using Voxelicious.Runtime.Function;
using Voxelicious.Runtime.Variable;
using Voxelicious.Util.List;

namespace Voxelicious.Enviornment
{

    public interface IEnvTemplate
    {
        List<IVariable> Variables { get; set; }
    }

    public class ImplEnvTemplate
    {
        public List<IVariable> Variables { get; set; } = new List<IVariable>();

        public ImplEnvTemplate() {}

        public ImplEnvTemplate(List<IVariable> variables)
        {
            this.Variables = variables;
        }
    }

    public class DefaultScope : IEnvTemplate
    {
        public List<IVariable> Variables { get; set; } = new List<IVariable>();

        public DefaultScope()
        {
            // define Native method
            Variables.Add(new Variable(
                "print", Access.AccessModifier.Public, true, new NativeFunctionValue(
                    new NativePrintFunction()
                )
            ));
        }
    }
}