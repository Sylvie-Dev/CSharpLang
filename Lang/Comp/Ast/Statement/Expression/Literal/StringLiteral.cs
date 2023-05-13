
namespace Voxelicious.Ast.LiteralExpression
{

    public class StringLiteral : ILiteralExpr<string>
    {
        public NodeType Kind => NodeType.StringLiteral;
        public string Value { get; }

        public StringLiteral(string value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}