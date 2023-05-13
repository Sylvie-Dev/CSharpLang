
namespace Voxelicious.Ast.LiteralExpression
{

    public class NullLiteral : ILiteralExpr<string>
    {
        public NodeType Kind => NodeType.NullLiteral;
        public string Value { get; } = "null";

        public NullLiteral() {}

        public override string ToString()
        {
            return Value;
        }
    }
}