
namespace Voxelicious.Ast.LiteralExpression
{

    public class IntegerLiteral : NumericLiteralExpr<int>
    {
        public NodeType Kind => NodeType.IntegerLiteral;
        public INumber<int> Value { get; }

        public IntegerLiteral(int value)
        {
            this.Value = new IntegerNumber(value);
        }

        public IntegerLiteral(INumber<int> value)
        {
            this.Value = value;
        }
    }
}