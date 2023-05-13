
namespace Voxelicious.Ast.LiteralExpression
{
    public class BooleanLiteral : ILiteralExpr<bool>
    {
        public NodeType Kind => NodeType.BooleanLiteral;
        public bool Value { get; }

        public BooleanLiteral(bool value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}