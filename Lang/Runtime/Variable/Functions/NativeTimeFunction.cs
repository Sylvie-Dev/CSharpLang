using Voxelicious.Enviornment;
using Voxelicious.Runtime.Variable;

namespace Voxelicious.Runtime.Function
{
    public class NativeTimeFunction : ICallable<IRuntimeVariable>
    {
        public DefaultArg[] DefaultArgs => new DefaultArg[] { 
        };

        public IRuntimeVariable Call(List<IRuntimeVariable> args, IEnviornment enviornment)
        {
            return new LongValue(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
        }
    }
}