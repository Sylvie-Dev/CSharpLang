using System.Collections.Generic;

using Voxelicious.Ast;
using Voxelicious.Lexer;
using Voxelicious.Lexer.Token;
using Voxelicious.Util.List;
using Voxelicious.Ast.Statement;
using Voxelicious.Ast.Expression;
using Voxelicious.Ast.LiteralExpression;
using Voxelicious.Access;

namespace Voxelicious.Parser
{

    public class Parser : ParserBase
    {
        public override ProgramStatement ProduceAST(string src, ILexer lexer)
        {
            ProgramStatement program = new ProgramStatement();

            SetTokens(lexer.Tokenize(src));

            while (NotEof()) program.Body.Add(ParseStatement(false));

            return program;
        }

        public IStatement ParseStatement(bool statement)
        {
            switch (CurrentType())
            {
                case TokenType.And:
                case TokenType.Let:
                    return ParseVariableDeclaration(statement);

                case TokenType.For:
                    return ParseForStatement(statement);

                case TokenType.While:
                    return ParseWhileStatement(statement);

                case TokenType.If:
                    return ParseIfStatement(statement);

                case TokenType.Break:
                    return ParseBreakStatement(statement);

                case TokenType.Continue:
                    return ParseContinueStatement(statement);

                case TokenType.Return:
                    return ParseReturnStatement(statement);

                case TokenType.Public:
                case TokenType.Private:
                case TokenType.Object:
                    return ParseObjectDeclaration(statement);


                default:
                    return ParseExpression(statement);
            }
        }

        private IExpression ParseObjectDeclaration(bool statement)
        {
            AccessModifier accessModifier = AccessModifier.Public;
            bool constant = false;
            if (IsType(TokenType.Object)) Eat();
            else
            {
                accessModifier = AccessModifierExtensions.FromToken(Eat().Type);
                if (At().Type == TokenType.And)
                {
                    Eat();
                    constant = true;
                }
                Expect(TokenType.Object, "Expected 'obj' token following access modifier");

            }
            string identifier = Expect(TokenType.Identifier, "Expected an Identifier following access modifier/object declaration").Value;

            Expect(TokenType.Equals, "Expected '=' token following identifier");
            Expect(TokenType.RightArrow, "Expected '>' token following '=' token");
            Expect(TokenType.OpenBrace, "Expected '{' token following '=>' token");

            // Parse object body eg
            // foo: 1,
            // bar: 2,
            // baz: {
            //     foo: 1,
            // },
            // qux: 3;
            List<PropertyLiteralExpr> properties = new List<PropertyLiteralExpr>();
            TokenType lastTokenType = TokenType.OpenBrace;
            while (NotEof() && !IsType(TokenType.CloseBrace))
            {
                string propertyName = Expect(TokenType.Identifier, "Expected an Identifier following " + lastTokenType.ToString() + " token").Value;
                if (IsType(TokenType.Comma))
                {
                    Eat();
                    properties.Add(new PropertyLiteralExpr(propertyName, new EmptyExpr()));
                    continue;
                }
                else if (IsType(TokenType.CloseBrace))
                {
                    properties.Add(new PropertyLiteralExpr(propertyName, new EmptyExpr()));
                    break;
                }

                Expect(TokenType.Colon, "Expected ':' token following property name");

                if (IsType(TokenType.OpenBrace))
                {
                    properties.Add(new PropertyLiteralExpr(propertyName, ParseObjectLiteral(statement)));
                }
                else
                {
                    properties.Add(new PropertyLiteralExpr(propertyName, ParseExpression(statement)));
                }

                if (!(IsType(TokenType.CloseBrace))) Expect(TokenType.Comma, "Expected ',' token following property expression");


                lastTokenType = At().Type;
            }

            Expect(TokenType.CloseBrace, "Expected '}' token following property expression");
            if (!statement)
                AdvanceIf(TokenType.Semicolon);

            return new ObjectLiteral(identifier, new ObjectLiteralExpr(properties), accessModifier, constant);
        }

        private IExpression ParseObjectLiteral(bool statement)
        {
            // { foo: 1, bar: 2, baz: { foo: 1, }, qux: 3 }
            Expect(TokenType.OpenBrace, "Expected '{' token following object declaration");

            List<PropertyLiteralExpr> properties = new List<PropertyLiteralExpr>();
            TokenType lastTokenType = TokenType.OpenBrace;
            while (NotEof() && !IsType(TokenType.CloseBrace))
            {
                string propertyName = Expect(TokenType.Identifier, "Expected an Identifier following " + lastTokenType.ToString() + " token").Value;
                if (IsType(TokenType.Comma))
                {
                    Eat();
                    properties.Add(new PropertyLiteralExpr(propertyName, new EmptyExpr()));
                    continue;
                }
                else if (IsType(TokenType.CloseBrace))
                {
                    properties.Add(new PropertyLiteralExpr(propertyName, new EmptyExpr()));
                    break;
                }

                Expect(TokenType.Colon, "Expected ':' token following property name");

                if (IsType(TokenType.OpenBrace))
                {
                    properties.Add(new PropertyLiteralExpr(propertyName, ParseObjectLiteral(statement)));
                }
                else
                {
                    properties.Add(new PropertyLiteralExpr(propertyName, ParseExpression(statement)));
                }

                if (!(IsType(TokenType.CloseBrace))) Expect(TokenType.Comma, "Expected ',' token following property expression");


                lastTokenType = At().Type;
            }

            Expect(TokenType.CloseBrace, "Expected '}' token following property expression");
            if (!statement)
                AdvanceIf(TokenType.Semicolon);
            return new ObjectLiteralExpr(properties);
        }

        private IStatement ParseVariableDeclaration(bool statement)
        {
            bool constant = IsType(TokenType.And);

            if (constant) EatMany(2);
            else Eat();

            AccessModifier accessModifier = AccessModifier.Public;

            if (IsType(TokenType.Public, TokenType.Private, TokenType.Protected))
            {
                accessModifier = AccessModifierExtensions.FromToken(Eat().Type);
            }

            string identifier = Expect(TokenType.Identifier, "Expected an Identifier following let/const keyword").Value;

            if (IsType(TokenType.Complex))
            {
                Eat();
                Expect("Expected '-' token following identifier", TokenType.BinaryOperator, "-");
                Expect(TokenType.RightArrow, "Expected '>' token following '=' token");
                Expect(TokenType.OpenBrace, "Expected '{' token following '=>' token");

                List<IStatement> statements = new List<IStatement>();

                while (NotEof() && !IsType(TokenType.CloseBrace))
                {
                    statements.Add(ParseStatement(statement));
                }

                if (!statements.Exists(x => x is ReturnStatement))
                    throw new System.Exception("[Parser Exception] -> Expected a return statement in complex statement");

                Expect(TokenType.CloseBrace, "Expected '}' token following complex statement");
                if (!statement) Expect(TokenType.Semicolon, "Expected ';' token following complex statement");

                return new VariableDeclaration(accessModifier, constant, identifier, null, new ComplexExpr(statements));
            }

            if (IsType(TokenType.Semicolon))
            {
                Eat();

                if (constant) throw new System.Exception("[Parser Exception] -> Expected an Identifier following const keyword");

                return new VariableDeclaration(accessModifier, false, identifier, null);
            }

            Expect(TokenType.Equals, "Expected '=' token following identifier");

            if (IsType(TokenType.OpenBracket))
            {
                return new VariableDeclaration(accessModifier, constant, identifier, ParseArrayLiteralExpr(statement));
            }

            IExpression expr = ParseExpression(false);

            if (!statement)
                Expect(TokenType.Semicolon, "Expected ';' token following expression");

            return new VariableDeclaration(accessModifier, constant, identifier, expr);
        }

        private ArrayLiteralExpr ParseArrayLiteralExpr(bool statement)
        {
            Expect(TokenType.OpenBracket, "Expected '[' token following array declaration");

            IArrayElement[] expressions = ParseArrayLiteral(statement);

            Expect(TokenType.CloseBracket, "Expected ']' token following array declaration");
            if (!statement)
                Expect(TokenType.Semicolon, "Expected ';' token following array declaration");

            ImplArray arrayImpl = new ImplArray(expressions);

            return new ArrayLiteralExpr(arrayImpl);
        }

        private IArrayElement[] ParseArrayLiteral(bool statement)
        {
            List<IArrayElement> expressions = new List<IArrayElement>();

            while (NotEof() && !IsType(TokenType.CloseBracket))
            {
                expressions.Add(new ImplArrayElement(ParseExpression(statement)));

                if (IsType(TokenType.Comma)) Eat();
            }


            return expressions.ToArray();
        }

        private IStatement ParseReturnStatement(bool statement)
        {
            Eat();

            IExpression expr = ParseExpression(statement);

            if (!statement)
                Expect(TokenType.Semicolon, "Expected ';' token following expression");

            return new ReturnStatement(expr);
        }

        private IStatement ParseForStatement(bool statement)
        {
            Expect(TokenType.For, "Expected 'for' keyword");
            Expect(TokenType.OpenParen, "Expected '(' token following 'for' keyword");

            IStatement initializer = new EmptyStatement();
            IExpression condition = new EmptyExpr();
            IExpression iterator = new EmptyExpr();

            if (!IsType(TokenType.Semicolon))
            {
                initializer = ParseStatement(statement);
            }

            if (initializer is EmptyStatement) Expect(TokenType.Semicolon, "Expected ';' token following initializer");


            if (!IsType(TokenType.Semicolon))
            {
                condition = ParseExpression(true);
            }

            Expect(TokenType.Semicolon, "Expected ';' token following condition");

            while (!IsType(TokenType.CloseParen))
            {
                iterator = ParseExpression(true);
            }

            Expect(TokenType.CloseParen, "Expected ')' token following iterator");

            if (IsType(TokenType.Semicolon))
            {
                Eat();
                return new ForStatement(initializer, condition, iterator, new List<IStatement>());
            }

            Expect(TokenType.OpenBrace, "Expected '{' token following for statement");

            List<IStatement> statements = new List<IStatement>();

            while (NotEof() && !IsType(TokenType.CloseBrace))
            {
                statements.Add(ParseStatement(statement));
            }

            Expect(TokenType.CloseBrace, "Expected '}' token following for statement");
            if (!statement)
                AdvanceIf(TokenType.Semicolon);

            return new ForStatement(initializer, condition, iterator, statements);
        }

        private IStatement ParseIfStatement(bool statement)
        {
            Expect(TokenType.If, "Expected 'if' keyword");
            Expect(TokenType.OpenParen, "Expected '(' token following 'if' keyword");

            IExpression condition = ParseExpression(true);

            Expect(TokenType.CloseParen, "Expected ')' token following condition");
            Expect(TokenType.OpenBrace, "Expected '{' token following if statement");

            List<IStatement> statements = new List<IStatement>();
            while (NotEof() && !IsType(TokenType.CloseBrace))
            {
                statements.Add(ParseStatement(statement));
            }

            Expect(TokenType.CloseBrace, "Expected '}' token following if statement");
            if (!statement)
                AdvanceIf(TokenType.Semicolon);

            ElseIfStatement? elseIfStatement = ParseElseIfStatement(statement);

            return new IfStatement(condition, statements, elseIfStatement);
        }

        private ElseIfStatement? ParseElseIfStatement(bool statement)
        {
            if (!IsType(TokenType.Else)) return null;

            Eat();

            if (IsType(TokenType.If))
            {
                Eat();
                Expect(TokenType.OpenParen, "Expected '(' token following 'else if' keyword");

                IExpression condition = ParseExpression(true);

                Expect(TokenType.CloseParen, "Expected ')' token following condition");

                Expect(TokenType.OpenBrace, "Expected '{' token following else if statement");

                List<IStatement> statements = new List<IStatement>();

                while (NotEof() && !IsType(TokenType.CloseBrace))
                {
                    statements.Add(ParseStatement(statement));
                }

                Expect(TokenType.CloseBrace, "Expected '}' token following else if statement");
                if (!statement)
                    AdvanceIf(TokenType.Semicolon);

                ElseIfStatement? elseIf = ParseElseIfStatement(statement);

                return new ElseIfStatement(statements, condition, elseIf);
            }


            Expect(TokenType.OpenBrace, "Expected '{' token following else statement");

            List<IStatement> elseStatements = new List<IStatement>();

            while (NotEof() && !IsType(TokenType.CloseBrace))
            {
                elseStatements.Add(ParseStatement(statement));
            }

            Expect(TokenType.CloseBrace, "Expected '}' token following else statement");
            if (!statement)
                AdvanceIf(TokenType.Semicolon);

            return new ElseIfStatement(elseStatements, new EmptyExpr(), null);
        }

        private IStatement ParseWhileStatement(bool statement)
        {
            Expect(TokenType.While, "Expected 'while' keyword");
            Expect(TokenType.OpenParen, "Expected '(' token following 'while' keyword");

            IExpression condition = ParseExpression(statement);

            Expect(TokenType.CloseParen, "Expected ')' token following condition");

            Expect(TokenType.OpenBrace, "Expected '{' token following while statement");

            List<IStatement> statements = new List<IStatement>();

            while (NotEof() && !IsType(TokenType.CloseBrace))
            {
                statements.Add(ParseStatement(statement));
            }

            Expect(TokenType.CloseBrace, "Expected '}' token following while statement");

            return new WhileStatement(condition, statements);
        }

        private IStatement ParseBreakStatement(bool statement)
        {
            Eat();
            if (!statement)
                Expect(TokenType.Semicolon, "Expected ';' token following break statement");

            return new BreakStatement();
        }

        private IStatement ParseContinueStatement(bool statement)
        {
            Eat();
            if (!statement)
                Expect(TokenType.Semicolon, "Expected ';' token following continue statement");

            return new ContinueStatement();
        }


        public IExpression ParseExpression(bool statment)
        {
            return ParseAssignmentExpression(statment);
        }
        private IExpression ParseAssignmentExpression(bool statement)
        {
            IExpression left = ParseLogicalExpr(statement);
            if (IsType(TokenType.Equals))
            {
                Eat();
                if (IsType(TokenType.RightArrow))
                {
                    Eat();
                    Expect(TokenType.OpenBrace, "Expected '{' token following '=>' token");

                    List<IStatement> statements = new List<IStatement>();

                    while (NotEof() && !IsType(TokenType.CloseBrace))
                    {
                        statements.Add(ParseStatement(statement));
                    }

                    if (!statements.Exists(x => x is ReturnStatement))
                        throw new System.Exception("[Parser Exception] -> Expected a return statement in complex statement");

                    Expect(TokenType.CloseBrace, "Expected '}' token following complex statement");
                    Expect(TokenType.Semicolon, "Expected ';' token following complex statement");

                    return new AssignmentExpr(left, null, new ComplexExpr(statements));
                }

                IExpression right = ParseExpression(statement);

                if (!statement)
                    Expect(TokenType.Semicolon, "Expected ';' token following expression");


                return new AssignmentExpr(left, right);
            }

            return left;
        }

        private IExpression ParseLogicalExpr(bool statment)
        {
            IExpression left = ParseEqualityExpr(statment);


            // IsValue("&&", "||") wont work because it takes in a single char
            LangToken current = At();
            string operatorCode = current.Value;
            if (IsType(TokenType.And, TokenType.Or))
            {
                Eat();
                if (IsType(TokenType.And, TokenType.Or))
                {
                    if (operatorCode != CurrentValue())
                        throw new System.Exception("[Parser Exception] -> Expected a logical operator");
                    operatorCode += Eat().Value;
                }

                IExpression right = ParseEqualityExpr(statment);

                return new LogicalExpr(left, right, operatorCode);
            }

            return left;
        }

        private IExpression ParseEqualityExpr(bool statement)
        {
            IExpression left = ParseRelationalExpr(statement);


            LangToken current = At();
            string operatorCode = current.Value;
            if (IsType(TokenType.Equals, TokenType.Exclamation))
            {
                Eat();
                if (IsType(TokenType.Equals, TokenType.Exclamation))
                {
                    operatorCode += Eat().Value;

                    IExpression right = ParseRelationalExpr(statement);

                    return new ComparisonExpr(left, right, operatorCode);
                }
                
                return left;
            }


            return left;
        }

        private IExpression ParseRelationalExpr(bool statement)
        {
            IExpression left = ParseAdditiveExpression(statement);

            string operatorCode = CurrentValue();
            while (IsType(TokenType.LeftArrow, TokenType.RightArrow, TokenType.Exclamation))
            {
                Eat();
                if (IsType(TokenType.LeftArrow, TokenType.RightArrow, TokenType.Equals, TokenType.Exclamation))
                {
                    operatorCode += Eat().Value;
                }


                IExpression right = ParseAdditiveExpression(statement);

                return new ComparisonExpr(left, right, operatorCode);
            }

            return left;
        }

        private IExpression ParseAdditiveExpression(bool statement)
        {
            IExpression left = ParseMultiplicativeExpression(statement);

            while (IsValue("+", "-"))
            {
                string operatorCode = Eat().Value;

                IExpression right = ParseMultiplicativeExpression(statement);

                left = new BinaryExpr(left, right, operatorCode);
            }

            return left;
        }

        private IExpression ParseMultiplicativeExpression(bool statement)
        {
            IExpression left = ParseCallMemberExpr(statement);

            while (IsValue("/", "*", "%"))
            {
                string operatorCode = Eat().Value;

                IExpression right = ParseCallMemberExpr(statement);

                left = new BinaryExpr(left, right, operatorCode);
            }

            return left;
        }

        private IExpression ParseCallMemberExpr(bool statement)
        {
            IExpression member = ParseMemberExpr(statement);

            if (IsType(TokenType.OpenParen))
            {
                return ParseCallExpr(member, statement);
            }

            return member;
        }

        private IExpression ParseCallExpr(IExpression caller, bool statement)
        {
            IExpression callExpr = new CallExpr(
                caller,
                ParseArgsExpr(statement)
            );

            if (IsType(TokenType.OpenParen))
            {
                callExpr = ParseCallExpr(callExpr, statement);
            }

            return callExpr;
        }

        private List<IExpression> ParseArgsExpr(bool statement)
        {
            Expect(TokenType.OpenParen, "Expected '(' token following function identifier");

            List<IExpression> args = new List<IExpression>();

            if (!IsType(TokenType.CloseParen))
            {
                args = ParseArguementsList(statement);
            }

            Expect(TokenType.CloseParen, "Expected ')' token following function arguements");
            if (!statement)
                Expect(TokenType.Semicolon, "Expected ';' token following function call");

            return args;
        }

        private List<IExpression> ParseArguementsList(bool statement)
        {
            List<IExpression> args = new List<IExpression>();

            args.Add(ParseAssignmentExpression(statement));

            while (NotEof() && IsType(TokenType.Comma))
            {
                Eat();

                args.Add(ParseAssignmentExpression(statement));
            }

            return args;
        }

        private IExpression ParseMemberExpr(bool statement)
        {
            IExpression member = ParsePrimaryExpression(statement);

            while (IsType(TokenType.Dot, TokenType.OpenBracket))
            {
                LangToken operatorCode = Eat();

                IExpression property;
                bool computed;

                if (operatorCode.Type == TokenType.Dot)
                {
                    computed = false;
                    property = ParsePrimaryExpression(statement);

                    if (property.Kind != NodeType.Identifier)
                        throw new System.Exception("[Parser Exception] -> Expected an Identifier following '.' token");

                    if (!statement)
                        AdvanceIf(TokenType.Semicolon);
                }
                else
                {
                    computed = true;
                    property = ParseExpression(statement);

                    Expect(TokenType.CloseBracket, "Expected ']' token following expression");

                    if (!statement)
                        AdvanceIf(TokenType.Semicolon);
                }

                member = new MemberExpr(member, property, computed, operatorCode.Value);
            }

            return member;
        }

        private IExpression ParseUnaryExpression(bool statement)
        {
            if (IsValue("!", "-", "+", "~"))
            {
                string operatorCode = Eat().Value;
                if (IsValue("+", "-"))
                {
                    if (CurrentValue() != operatorCode) throw new System.Exception("[Parser Exception] -> Unexpected token: " + At() + " ?? " + operatorCode + " ?? " + CurrentValue());
                    operatorCode += Eat().Value;
                }

                return new UnaryExpr(ParseUnaryExpression(statement), operatorCode);
            }

            return ParseCallMemberExpr(statement);
        }

        private IExpression ParsePrimaryExpression(bool statement)
        {
            TokenType type = CurrentType();


            if (IsType(TokenType.Identifier)) return new IdentifierExpr(Eat().Value);
            else if (IsType(TokenType.String)) return new StringLiteral(ParseString(Eat().Value));
            else if (IsType(TokenType.Float)) return new FloatLiteral(ParseFloat(Eat().Value));
            else if (IsType(TokenType.Double)) return new DoubleLiteral(ParseDouble(Eat().Value));
            else if (IsType(TokenType.Long)) return new LongLiteral(ParseLong(Eat().Value));
            else if (IsType(TokenType.Short)) return new ShortLiteral(ParseShort(Eat().Value));
            else if (IsType(TokenType.Integer)) return new IntegerLiteral(ParseInt(Eat().Value));
            else if (IsType(TokenType.Byte)) return new ByteLiteral(ParseByte(Eat().Value));
            else if (IsType(TokenType.Boolean)) return new BooleanLiteral(ParseBool(Eat().Value));
            else if (IsType(TokenType.Null)) return new NullLiteral();
            else if (IsType(TokenType.Char)) return new CharLiteral(ParseChar(Eat().Value));
            else if (IsType(TokenType.OpenParen))
            {
                Eat();
                IExpression expr = ParseExpression(statement);
                Expect(TokenType.CloseParen, "Expected ')' token following expression");
                return expr;
            }
            else if (IsType(TokenType.Exclamation, TokenType.Tilde, TokenType.BinaryOperator) && IsValue("+", "-", "!", "~"))
            {
                return ParseUnaryExpression(statement);
            }
            else
            {
                throw new System.Exception("[Parser Exception] -> Unexpected token: " + At());
            }

        }

        public INumber<int> ParseInt(string str) => new IntegerNumber(int.Parse(str));
        public INumber<long> ParseLong(string str) => new LongNumber(long.Parse(str.Replace("L", "").Replace("l", "")));
        public INumber<short> ParseShort(string str) => new ShortNumber(short.Parse(str));

        public char ParseChar(string str) => char.Parse(str);
        public string ParseString(string str) => str.Replace("\"", "");
        public bool ParseBool(string str) => bool.Parse(str);
        public INumber<float> ParseFloat(string str) => new FloatNumber(float.Parse(str.Replace("F", "").Replace("f", "")));
        public INumber<double> ParseDouble(string str) => new DoubleNumber(double.Parse(str.Replace("D", "").Replace("d", "")));
        public INumber<byte> ParseByte(string str) => new ByteNumber(byte.Parse(str));
        public bool ParseBoolean(string str) => bool.Parse(str);
    }


}