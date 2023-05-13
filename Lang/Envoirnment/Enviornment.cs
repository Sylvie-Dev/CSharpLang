using Voxelicious.Runtime;
using System;
using Voxelicious.Access;
using Voxelicious.Runtime.Variable;

namespace Voxelicious.Enviornment
{
    public class ProgramEnviorment : IEnviornment
    {
        public IEnviornment? Parent { get; set; }
        public Dictionary<string, IVariable> Variables { get; set; }
        public bool IsRoot { get { return Parent == null; } }
        public bool IsGlobal { get { return Parent == null; } }

        public ProgramEnviorment(IEnviornment? parent = null)
        {
            this.Parent = parent;
            this.Variables = new Dictionary<string, IVariable>();

            if (IsGlobal)
            {
                SetupScope(new DefaultScope());
            }
        }

        public IVariable Declare(string varname, IRuntimeVariable variable, AccessModifier accessModifier, bool constant)
        {
            if (Variables.ContainsKey(varname)) throw new Exception($"[Enviornment] -> {varname} already exists!");
            Variable var = new Variable(varname, accessModifier, constant, variable);
            Variables[varname] = var;
            return var;
        }

        public IRuntimeVariable Assign(string varname, IRuntimeVariable val)
        {
            IEnviornment env = Resolve(varname);
            IVariable var = env.Variables[varname];
            if (var.Constant) throw new Exception($"[Enviornment] -> {varname} is constant and cannot be reassigned!");
            if (!IsAccessable(var.AccessModifier, env)) throw new Exception($"[Enviornment] -> {varname} is not accessable from this scope!");
            return val;
        }

        public bool IsAccessable(AccessModifier accessModifier, IEnviornment env)
        {
            if (accessModifier == AccessModifier.Public) return true;
            if (accessModifier == AccessModifier.Private) return env == this;
            if (accessModifier == AccessModifier.Protected) return env == this;
            return false;
        }

        public IVariable Lookup (string varname)
        {
            IEnviornment env = Resolve(varname);
            if (!IsAccessable(env.Variables[varname].AccessModifier, env)) throw new Exception($"[Enviornment] -> {varname} is not accessable from this scope!");
            return env.Variables[varname];
        }

        public IEnviornment Resolve(string varname)
        {
            if (Variables.ContainsKey(varname)) return this;
            if (Parent != null) return Parent.Resolve(varname);
            throw new Exception($"[Enviornment] -> {varname} does not exist!");
        }

        public void SetupScope(IEnvTemplate template)
        {
            foreach (IVariable variable in template.Variables)
            {
                Declare(variable.Address, variable.RuntimeVariable, variable.AccessModifier, variable.Constant);
            }
        }
    }
}