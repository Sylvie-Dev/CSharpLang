
namespace Voxelicious.Ast.LiteralExpression
{

    public class DoubleLiteral : NumericLiteralExpr<double>
    {
        public NodeType Kind => NodeType.DoubleLiteral;
        public INumber<double> Value { get; }

        public DoubleLiteral(double value)
        {
            this.Value = new DoubleNumber(value);
        }

        public DoubleLiteral(INumber<double> value)
        {
            this.Value = value;
        }

    }
}