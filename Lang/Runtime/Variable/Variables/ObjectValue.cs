using Newtonsoft.Json;
using Voxelicious.Ast.LiteralExpression;


namespace Voxelicious.Runtime.Variable
{
    public class ObjectValue : IRuntimeValue<Dictionary<string, IRuntimeVariable>>
    {
        public ValueType Type => ValueType.Object;
        public Dictionary<string, IRuntimeVariable> Value { get; set; }

        public ObjectValue(Dictionary<string, IRuntimeVariable> value)
        {
            this.Value = value;
        }

        public ObjectValue()
        {
            this.Value = new Dictionary<string, IRuntimeVariable>();
        }

        public override string ToString() => JsonConvert.SerializeObject(Value, Formatting.Indented);
    }
}