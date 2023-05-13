
namespace Voxelicious.Runtime.Variable
{
    public class DoubleValue : NumberValue<double>
    {
        public override ValueType Type => ValueType.Double;
        public DoubleValue(double value) : base(value) { }
    }
}