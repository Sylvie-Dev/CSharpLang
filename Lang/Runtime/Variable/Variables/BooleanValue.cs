
namespace Voxelicious.Runtime.Variable
{
    public class BooleanValue : IRuntimeValue<bool>
    {
        public ValueType Type => ValueType.Boolean;
        public bool Value { get; set; }

        public BooleanValue(bool value)
        {
            this.Value = value;
        }

        public override string ToString() => Value.ToString();
    }
}