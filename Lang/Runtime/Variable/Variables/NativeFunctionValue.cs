
using Voxelicious.Enviornment;

namespace Voxelicious.Runtime.Variable
{

    public class NativeFunctionValue : IRuntimeValue<ICallable<IRuntimeVariable>>
    {
        public ValueType Type { get; } = ValueType.NativeFunction;
        public ICallable<IRuntimeVariable> Value { get; set; }

        public NativeFunctionValue(ICallable<IRuntimeVariable> value)
        {
            this.Value = value;
        }
    }  
}