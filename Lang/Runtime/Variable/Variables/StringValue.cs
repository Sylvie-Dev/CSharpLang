
namespace Voxelicious.Runtime.Variable
{
    public class StringValue : IRuntimeValue<string>
    {
        public ValueType Type => ValueType.String;
        public string Value { get; set; }

        public StringValue(string value)
        {
            this.Value = value;
        }

        public override string ToString() => Value!.ToString()!;
    }
}