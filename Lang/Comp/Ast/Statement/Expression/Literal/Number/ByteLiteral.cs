
namespace Voxelicious.Ast.LiteralExpression
{

    public class ByteLiteral : NumericLiteralExpr<byte>
    {
        public NodeType Kind => NodeType.ByteLiteral;
        public INumber<byte> Value { get; }

        public ByteLiteral(byte value)
        {
            this.Value = new ByteNumber(value);
        }

        public ByteLiteral(INumber<byte> value)
        {
            this.Value = value;
        }
    }
}