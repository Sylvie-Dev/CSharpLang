
namespace Voxelicious.Runtime.Variable
{
    public class IntegerValue : NumberValue<int>
    {
        public override ValueType Type => ValueType.Integer;

        public IntegerValue(int value) : base(value) { }
    }
}