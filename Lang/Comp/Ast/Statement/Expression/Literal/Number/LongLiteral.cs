
namespace Voxelicious.Ast.LiteralExpression
{

    public class LongLiteral : NumericLiteralExpr<long>
    {
        public NodeType Kind => NodeType.LongLiteral;
        public INumber<long> Value { get; }

        public LongLiteral(long value)
        {
            this.Value = new LongNumber(value);
        }

        public LongLiteral(INumber<long> value)
        {
            this.Value = value;
        }

    }
}