
namespace Voxelicious.Ast.LiteralExpression
{

    public class FloatLiteral : NumericLiteralExpr<float>
    {
        public NodeType Kind => NodeType.FloatLiteral;
        public INumber<float> Value { get; }

        public FloatLiteral(float value)
        {
            this.Value = new FloatNumber(value);
        }

        public FloatLiteral(INumber<float> value)
        {
            this.Value = value;
        }
    }
}