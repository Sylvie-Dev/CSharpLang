
using Voxelicious.Runtime.Variable;

namespace Voxelicious.Runtime.Variable
{

    public class VoidValue : IRuntimeValue<string>
    {
        public static VoidValue Instance { get; } = new VoidValue();

        public string Value => "void";
        public ValueType Type => ValueType.Void;

    

        public override string ToString() => "void";

        private VoidValue() { }
    }
}