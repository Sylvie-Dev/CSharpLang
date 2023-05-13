
namespace Voxelicious.Runtime.Variable
{
    public class ShortValue : NumberValue<short>
    {
        public override ValueType Type => ValueType.Short;
        public ShortValue(short value) : base(value) { }
    }
}