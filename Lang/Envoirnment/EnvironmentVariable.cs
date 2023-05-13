using Voxelicious.Runtime;
using System;
using Voxelicious.Access;
using Voxelicious.Runtime.Variable;


namespace Voxelicious.Enviornment
{

    public interface IVariable
    {
        string Address { get; }
        AccessModifier AccessModifier { get; }
        bool Constant { get; }
        IRuntimeVariable RuntimeVariable { get; }
    }
    public class Variable : IVariable
    {
        public IRuntimeVariable RuntimeVariable { get; set; } = new NullValue();
        public AccessModifier AccessModifier { get; set; }
        public bool Constant { get; set; }
        public string Address { get; }

        public Variable(string address, AccessModifier accessModifier, bool constant, IRuntimeVariable? variable)
        {
            this.Address = address;
            this.AccessModifier = accessModifier;
            this.Constant = constant;
            if (variable != null) this.RuntimeVariable = variable;
        }
    }

}