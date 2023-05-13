
namespace Voxelicious.Runtime.Variable
{
    public class CharValue : IRuntimeValue<char>
    {
        public char Value { get; set; }
        public ValueType Type => ValueType.Char;

        public CharValue(char value) => Value = value;

        public override string ToString() => Value.ToString()!;
    }
}