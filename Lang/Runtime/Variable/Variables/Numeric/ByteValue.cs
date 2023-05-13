
namespace Voxelicious.Runtime.Variable
{
    public class ByteValue : NumberValue<byte>
    {
        public override ValueType Type => ValueType.Byte;
        public ByteValue(byte value) : base(value) { }
    }
}