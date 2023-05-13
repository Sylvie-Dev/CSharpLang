using Voxelicious.Access;
using Voxelicious.Ast.Expression;


namespace Voxelicious.Ast.Statement
{
    public class VariableDeclaration : IStatement
    {
        public NodeType Kind { get; } = NodeType.VariableDeclaration;
        public AccessModifier AccessModifier { get; }
        public bool IsConstant { get; }
        public bool IsComplex { get; } = false;
        public string Identifier { get; }
        public IExpression? Value { get; }
        public ComplexExpr ComplexExpression { get; } = new ComplexExpr();

        public VariableDeclaration(AccessModifier accessModifier, bool constant, string identifier, IExpression? expression, ComplexExpr? complexExpression = null)
        {
            this.AccessModifier = accessModifier;
            this.IsConstant = constant;
            this.Identifier = identifier;
            this.IsComplex = complexExpression != null;
            if (expression == null) this.Value = new EmptyExpr();
            else this.Value = expression;
            if (complexExpression != null) this.ComplexExpression = complexExpression;
        }
    }
}