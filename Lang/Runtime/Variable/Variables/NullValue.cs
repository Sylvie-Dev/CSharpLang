
namespace Voxelicious.Runtime.Variable
{

    public class NullValue : IRuntimeValue<string>
    {
        public string Value => "null";

        public ValueType Type => ValueType.Null;

        public override string ToString() => "null";
    }
}