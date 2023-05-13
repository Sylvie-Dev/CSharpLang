using Voxelicious.Ast;
using Voxelicious.Enviornment;
using Voxelicious.Ast.Expression;
using Voxelicious.Ast.LiteralExpression;
using Voxelicious.Ast.Statement;
using Voxelicious.Runtime.Variable;
using Newtonsoft.Json;

namespace Voxelicious.Runtime
{

    public class Interpreter
    {

        public IRuntimeVariable EvaluateProgram(ProgramStatement program, IEnviornment env)
        {
            IRuntimeVariable lastEvaluated = new NullValue();

            foreach (IStatement stmt in program.Body)
            {
                lastEvaluated = Evaluate(stmt, env);
            }

            return lastEvaluated;
        }


        public IRuntimeVariable Evaluate(IStatement node, IEnviornment env)
        {
            switch (node.Kind)
            {
                case NodeType.IntegerLiteral:
                    return new IntegerValue(((IntegerLiteral) node).Value.Value);
            
                case NodeType.StringLiteral:
                    return new StringValue(((StringLiteral) node).Value);

                case NodeType.FloatLiteral:
                    return new FloatValue(((FloatLiteral) node).Value.Value);

                case NodeType.DoubleLiteral:
                    return new DoubleValue(((DoubleLiteral) node).Value.Value);

                case NodeType.LongLiteral:
                    return new LongValue(((LongLiteral) node).Value.Value);
                
                case NodeType.ShortLiteral:
                    return new ShortValue(((ShortLiteral) node).Value.Value);

                case NodeType.ByteLiteral:
                    return new ByteValue(((ByteLiteral) node).Value.Value);

                case NodeType.CharLiteral:
                    return new CharValue(((CharLiteral) node).Value);
                    
                case NodeType.BooleanLiteral:
                    return new BooleanValue(((BooleanLiteral) node).Value);

                case NodeType.Identifier:
                    return EvaluateIdentifierExpr((IdentifierExpr)node, env);

                case NodeType.AssignmentExpr:
                    return EvaluateAssignmentExpr((AssignmentExpr)node, env);

                case NodeType.PropertyLiteral:
                    return EvaluatePropertyExpr((PropertyLiteralExpr)node, env);

                case NodeType.UnaryExpr:
                    return EvaluateUnaryExpr((UnaryExpr)node, env);

                case NodeType.ObjectLiteral:
                    return EvaluateObjectExpr((ObjectLiteral)node, env);

                case NodeType.ObjectLiteralExpr:
                    return EvaluateObjectLiteralExpr((ObjectLiteralExpr)node, env);

                case NodeType.ArrayLiteralExpr:
                    return EvaluateArrayLiteralExpr((ArrayLiteralExpr)node, env);

                case NodeType.CallExpr:
                    return EvaluateCallExpr((CallExpr)node, env);

                case NodeType.BinaryExpr:
                    return EvaluateBinaryExpr((BinaryExpr)node, env);

                case NodeType.ComplexExpr:
                    return EvaluateComplexExpr((ComplexExpr)node, env);

                case NodeType.IfStatement:
                    return EvaluateIfStatement((IfStatement)node, env);


                case NodeType.ForStatement:
                    return EvaluateForStatement((ForStatement)node, env);

                case NodeType.WhileStatement:
                    return EvaluateWhileStatement((WhileStatement)node, env);

                case NodeType.Program:
                    return EvaluateProgram((ProgramStatement)node, env);

                case NodeType.ReturnStatement:
                    return EvaluateReturnStatement((ReturnStatement)node, env);

                case NodeType.VariableDeclaration:
                    return EvaluateVariableDeclaration((VariableDeclaration)node, env);

                case NodeType.EmptyExpr:
                    return new NullValue();

                case NodeType.EmptyStatement:
                    return new NullValue();


                default:
                    throw new Exception("[Interpreter] -> Unknown node type: " + JsonConvert.SerializeObject(node, Formatting.Indented) + "! has no evaluate method!");
            }
        }


        public IRuntimeVariable EvaluateVariableDeclaration(VariableDeclaration variableDeclaration, IEnviornment env)
        {

            IRuntimeVariable value = new NullValue();
            if (variableDeclaration.IsComplex)
            {


                IEnviornment complexEnv = new ProgramEnviorment(env);

                ComplexExpr complexExpression = variableDeclaration.ComplexExpression;
                value = Evaluate(complexExpression, complexEnv);

                if (value is null || value is EmptyExpr) value = new NullValue();
            }
            else
            {
                if (variableDeclaration.Value is null || variableDeclaration.Value is EmptyExpr) value = new NullValue();
                else value = Evaluate(variableDeclaration.Value, env);
            }

            env.Declare(variableDeclaration.Identifier, value, variableDeclaration.AccessModifier, variableDeclaration.IsConstant);

            return value;
        }

        public IRuntimeVariable EvaluateReturnStatement(ReturnStatement returnStatement, IEnviornment env)
        {
            if (returnStatement.Value is null || returnStatement.Value is EmptyExpr) return new NullValue();
            return Evaluate(returnStatement.Value, env);
        }

        public IRuntimeVariable EvaluateAssignmentExpr(AssignmentExpr assignment, IEnviornment env)
        {
            if (assignment.Assigne.Kind != NodeType.Identifier) throw new Exception("[Interpreter] -> Assignment expression must have an identifier as the assigne!");

            IdentifierExpr ident = (IdentifierExpr)assignment.Assigne;

            return env.Assign(ident.Token, Evaluate(assignment.Value, env));
        }

        public IRuntimeVariable EvaluateObjectExpr(ObjectLiteral obj, IEnviornment env)
        {
            IRuntimeVariable objVar = new ObjectValue();

            foreach (PropertyLiteralExpr prop in obj.Value.Value)
            {
                IRuntimeVariable var = EvaluatePropertyExpr(prop, env);

                if (var is null || var is EmptyExpr) var = new NullValue();

                ((ObjectValue)objVar).Value.Add(prop.Key, var);
            }

            return objVar;
        }

        public IRuntimeVariable EvaluateCallExpr(CallExpr obj, IEnviornment env)
        {
            List<IRuntimeVariable> args = new List<IRuntimeVariable>();
            obj.Args.ForEach((arg) => args.Add(Evaluate(arg, env)));

            if (obj.Caller.Kind != NodeType.Identifier) throw new Exception("[Interpreter] -> Caller must be an identifier!");

            IRuntimeVariable var = env.Lookup(((IdentifierExpr)obj.Caller).Token).RuntimeVariable;
            if (var.Type != Variable.ValueType.NativeFunction) throw new Exception("[Interpreter] -> Caller must be a function!");

            NativeFunctionValue func = (NativeFunctionValue)var;

            return func.Value.Call(args, env);
        }

        public IRuntimeVariable EvaluateBlockStatement(IBlockStatement block, IEnviornment env)
        {
            IRuntimeVariable lastEvaluated = new NullValue();

            foreach (IStatement stmt in block.Body)
            {
                lastEvaluated = Evaluate(stmt, env);
            }

            return lastEvaluated;
        }

        public IRuntimeVariable EvaluateIfStatement(IfStatement ifStatement, IEnviornment env)
        {
            if (ifStatement is ElseIfStatement) return EvaluateElseStatement((ElseIfStatement)ifStatement, env);

            IRuntimeVariable condition = Evaluate(ifStatement.Condition, env);

            if (condition is null || condition is EmptyExpr) condition = new NullValue();

            if (condition.Type != Variable.ValueType.Boolean) throw new Exception("[Interpreter] -> If statement condition must be a boolean! got: " + condition.Type);

            if (((BooleanValue)condition).Value)
            {
                return EvaluateBlockStatement(ifStatement, env);
            }
            else
            {
                if (ifStatement.ElseStatement is null) return new NullValue();
                return Evaluate(ifStatement.ElseStatement, env);
            }
        }

        public IRuntimeVariable EvaluateElseStatement(ElseIfStatement elseStatement, IEnviornment env)
        {
            if (elseStatement.HasCondition)
            {
                IRuntimeVariable condition = Evaluate(elseStatement.Condition, env);

                if (condition is null || condition is EmptyExpr) condition = new NullValue();

                if (((BooleanValue)condition).Value)
                {
                    return EvaluateBlockStatement(elseStatement, env);
                }
                else
                {
                    if (elseStatement.ElseStatement is null) return new NullValue();
                    return Evaluate(elseStatement.ElseStatement, env);
                }
            }
            else return EvaluateBlockStatement(elseStatement, env);
        }

        public IRuntimeVariable EvaluateForStatement(ForStatement forStatement, IEnviornment env)
        {
            IRuntimeVariable lastEvaluated = new NullValue();

            IEnviornment forEnv = new ProgramEnviorment(env);

            if (forStatement.Initializer is null || forStatement.Initializer is EmptyStatement || forStatement.Initializer is EmptyExpr) return new NullValue();
            
            if (forStatement.Initializer is VariableDeclaration)
            {
                EvaluateVariableDeclaration((VariableDeclaration)forStatement.Initializer, forEnv);
            }
            else if (forStatement.Initializer is AssignmentExpr)
            {
                EvaluateAssignmentExpr((AssignmentExpr)forStatement.Initializer, forEnv);
            }
            else throw new Exception("[Interpreter] -> For statement initializer must be a variable declaration or an assignment expression!");


            IExpression condition = forStatement.Condition;

            if (forStatement.Condition is null || forStatement.Condition is EmptyExpr) return new NullValue();

             IStatement updater = forStatement.Updater;

            if (forStatement.Updater is null || forStatement.Updater is EmptyStatement || forStatement.Updater is EmptyExpr) return new NullValue();

            Console.WriteLine(JsonConvert.SerializeObject(forStatement.Updater, Formatting.Indented));
            if (forStatement.Updater is AssignmentExpr)
            {
                EvaluateAssignmentExpr((AssignmentExpr)forStatement.Updater, forEnv);
            }
            else throw new Exception("[Interpreter] -> For statement updater must be an assignment expression!");


            return lastEvaluated;
        }

        public IRuntimeVariable EvaluateWhileStatement(WhileStatement whileStatement, IEnviornment env)
        {
            IRuntimeVariable lastEvaluated = new NullValue();

            return lastEvaluated;
        }
        public IRuntimeVariable EvaluateObjectLiteralExpr(ObjectLiteralExpr obj, IEnviornment env)
        {
            IRuntimeVariable objVar = new ObjectValue();

            foreach (PropertyLiteralExpr prop in obj.Value)
            {
                IRuntimeVariable var = EvaluatePropertyExpr(prop, env);

                if (var is null || var is EmptyExpr) var = new NullValue();

                ((ObjectValue)objVar).Value.Add(prop.Key, var);
            }

            return objVar;
        }

        public IRuntimeVariable EvaluateArrayLiteralExpr(ArrayLiteralExpr array, IEnviornment env)
        {
            IRuntimeVariable arrayVar = new ArrayValue();

            foreach (IArrayElement expr in array.Value.Elements)
            {
                IRuntimeVariable var = Evaluate(expr.Value, env);

                if (var is null || var is EmptyExpr) var = new NullValue();

                ArrayValue arr = (ArrayValue) arrayVar;
                arr.Value.Add(var);
            }

            return arrayVar;
        }
        
        public IRuntimeVariable EvaluateComplexExpr(ComplexExpr complexExpr, IEnviornment env)
        {
            IRuntimeVariable lastEvaluated = new NullValue();

            foreach (IStatement stmt in complexExpr.Body)
            {
                lastEvaluated = Evaluate(stmt, env);
            }

            return lastEvaluated;
        }

        public IRuntimeVariable EvaluateIdentifierExpr(IdentifierExpr ident, IEnviornment env)
        {
            return env.Lookup(ident.Token).RuntimeVariable;
        }

        public IRuntimeVariable EvaluatePropertyExpr(PropertyLiteralExpr prop, IEnviornment env)
        {
            IRuntimeVariable var = new NullValue();
            if (prop.Value == null || prop.Value is EmptyExpr)
            {
                var = new NullValue();
            }
            else
            {
                var = Evaluate(prop.Value, env);
            }

            if (var is null || var is EmptyExpr) var = new NullValue();

            return var;
        }

        public IRuntimeVariable EvaluateUnaryExpr(UnaryExpr unary, IEnviornment env)
        {

            IRuntimeVariable var = Evaluate(unary.Right, env);

            if (var.Type == Variable.ValueType.Integer)
            {
                if (unary.Operator == "-") return new IntegerValue(-((IntegerValue)var).Value);
                else if (unary.Operator == "+") return new IntegerValue(((IntegerValue)var).Value);
                else if (unary.Operator == "~") return new IntegerValue(~((IntegerValue)var).Value);
                else if (unary.Operator == "++") return new IntegerValue(((IntegerValue)var).Value + 1);
                else if (unary.Operator == "--") return new IntegerValue(((IntegerValue)var).Value - 1);
                else throw new Exception("[Interpreter] -> Invalid unary operator for integer: " + unary.Operator);
            }
            else if (var.Type == Variable.ValueType.Long)
            {
                if (unary.Operator == "-") return new LongValue(-((LongValue)var).Value);
                else if (unary.Operator == "+") return new LongValue(((LongValue)var).Value);
                else if (unary.Operator == "~") return new LongValue(~((LongValue)var).Value);
                else if (unary.Operator == "++") return new LongValue(((LongValue)var).Value + 1);
                else if (unary.Operator == "--") return new LongValue(((LongValue)var).Value - 1);
                else throw new Exception("[Interpreter] -> Invalid unary operator for long: " + unary.Operator);
            }
            else if (var.Type == Variable.ValueType.Float)
            {
                if (unary.Operator == "-") return new FloatValue(-((FloatValue)var).Value);
                else if (unary.Operator == "+") return new FloatValue(((FloatValue)var).Value);
                else if (unary.Operator == "++") return new FloatValue(((FloatValue)var).Value + 1);
                else if (unary.Operator == "--") return new FloatValue(((FloatValue)var).Value - 1);
                else throw new Exception("[Interpreter] -> Invalid unary operator for float: " + unary.Operator);
            }
            else if (var.Type == Variable.ValueType.Double)
            {
                if (unary.Operator == "-") return new DoubleValue(-((DoubleValue)var).Value);
                else if (unary.Operator == "+") return new DoubleValue(((DoubleValue)var).Value);
                else if (unary.Operator == "++") return new DoubleValue(((DoubleValue)var).Value + 1);
                else if (unary.Operator == "--") return new DoubleValue(((DoubleValue)var).Value - 1);
                else throw new Exception("[Interpreter] -> Invalid unary operator for double: " + unary.Operator);
            }
            else if (var.Type == Variable.ValueType.Boolean)
            {
                if (unary.Operator == "!") return new BooleanValue(!((BooleanValue)var).Value);
                else throw new Exception("[Interpreter] -> Invalid unary operator for boolean!");
            }
            else 
            {
                throw new Exception("[Interpreter] -> Invalid unary operator for type: " + var.Type);
            }
        }

        public IRuntimeVariable EvaluateBinaryExpr(BinaryExpr binop, IEnviornment env)
        {
            IRuntimeVariable left = Evaluate(binop.Left, env);
            IRuntimeVariable right = Evaluate(binop.Right, env);
            
            if (left is null || left is EmptyExpr) left = new NullValue();

            if (right is null || right is EmptyExpr) right = new NullValue();

            if (left.Type == Variable.ValueType.String && right.Type == Variable.ValueType.String) return EvaluateBinaryStringExpr((StringValue) left, (StringValue) right, binop, env);

            IRuntimeVariable result = new NullValue();

            switch(binop.Operator)
            {
                case "/":
                    result = left.Divide(right, env);
                    break;
                case "*":
                    result = left.Multiply(right, env);
                    break;
                case "%":
                    result = left.Modulo(right, env);
                    break;
                case "+": 
                    result = left.Add(right, env);
                    break;
                case "-":
                    result = left.Subtract(right, env);
                    break;
                case "<<":
                    result = left.ShiftLeft(right, env);
                    break;
                case ">>":
                    result = left.ShiftRight(right, env);
                    break;
                case "&":
                    result = left.And(right, env);
                    break;
                case "^":
                    result = left.Xor(right, env);
                    break;
                case "|":
                    result = left.Or(right, env);
                    break;
            }
            return result;
        }

        public IRuntimeVariable EvaluateBinaryStringExpr(StringValue left, StringValue right, BinaryExpr binop, IEnviornment env)
        {
            IRuntimeVariable result = new NullValue();

            switch(binop.Operator)
            {
                case "+":
                    result = new StringValue(left.Value + right.Value);
                    break;
            }
            return result;
        }
    }
}