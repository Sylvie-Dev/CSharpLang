
namespace Voxelicious.Ast.LiteralExpression
{

    public class ShortLiteral : NumericLiteralExpr<short>
    {
        public NodeType Kind => NodeType.ShortLiteral;
        public INumber<short> Value { get; }

        public ShortLiteral(short value)
        {
            this.Value = new ShortNumber(value);
        }

        public ShortLiteral(INumber<short> value)
        {
            this.Value = value;
        }
    }

}