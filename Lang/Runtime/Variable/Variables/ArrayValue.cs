
namespace Voxelicious.Runtime.Variable
{
    public class ArrayValue : IRuntimeVariable
    {
        public ValueType Type => ValueType.Array;
        public List<IRuntimeVariable> Value { get; set; } = new List<IRuntimeVariable>();

        public ArrayValue() {}

        public override string ToString()
        {
            return $"[{string.Join(", ", Value)}]";
        }
    }
}