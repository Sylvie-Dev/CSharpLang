using Voxelicious.Runtime;
using System;
using Voxelicious.Access;
using Voxelicious.Runtime.Variable;


namespace Voxelicious.Enviornment
{
    public interface IEnviornment
    {
        IEnviornment? Parent { get; }
        Dictionary<string, IVariable> Variables { get; set; }


        void SetupScope(IEnvTemplate template)
        {
            foreach (IVariable variable in template.Variables)
            {
                Declare(variable.Address, variable.RuntimeVariable, variable.AccessModifier, variable.Constant);
            }
        }

        IVariable Declare(string varname, IRuntimeVariable variable, AccessModifier accessModifier = AccessModifier.Public, bool constant = false);
        IRuntimeVariable Assign(string varname, IRuntimeVariable val);
        bool IsAccessable(AccessModifier accessModifier, IEnviornment env);
        IVariable Lookup (string varname);
        IEnviornment Resolve(string varname);
    }
}