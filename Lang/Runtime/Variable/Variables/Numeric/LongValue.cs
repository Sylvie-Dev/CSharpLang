
namespace Voxelicious.Runtime.Variable
{
    public class LongValue : NumberValue<long>
    {
        public override ValueType Type => ValueType.Long;
        public LongValue(long value) : base(value) { }
    }
}