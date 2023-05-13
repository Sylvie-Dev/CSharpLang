using Voxelicious.Enviornment;
using Voxelicious.Runtime.Variable;

namespace Voxelicious.Runtime.Function
{
    public class NativePrintFunction : ICallable<IRuntimeVariable>
    {
        public DefaultArg[] DefaultArgs => new DefaultArg[] { 
        };

        public IRuntimeVariable Call(List<IRuntimeVariable> args, IEnviornment enviornment)
        {
            
            for (int i = 0; i < args.Count; i++)
            {
                Console.Write(args[i].ToString());
            }

            return VoidValue.Instance;
        }
    }
}