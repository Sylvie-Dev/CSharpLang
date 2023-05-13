
namespace Voxelicious.Runtime.Variable
{
    public class FloatValue : NumberValue<float>
    {
        public override ValueType Type => ValueType.Float;
        public FloatValue(float value) : base(value) { }
    }
}