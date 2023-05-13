
namespace Voxelicious.Object
{
    public class ObjectProperty
    {
        public string Name { get; }
        public object Value { get; }

        public ObjectProperty(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}