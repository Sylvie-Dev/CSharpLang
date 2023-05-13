using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;


namespace Voxelicious.Runtime.Variable
{

    public enum ValueType
    {
        Null,
        Integer,
        Long,
        Short,
        Byte,
        Float,
        Double,
        String,
        Boolean,
        Object,
        Array,
        Char,

        
        NativeFunction,

        Void
    }

    public static class ValueTypeExtensions
    {
        public static string GetString(ValueType type)
        {
            return type.ToString().ToLower();
        }
    }
}