using System.Text;
using Voxelicious.Ast.Statement;

namespace Voxelicious.Ast.Expression
{

    public class CallExpr : IExpression
    {
        public NodeType Kind => NodeType.CallExpr;
        public List<IExpression> Args { get; }
        public IExpression Caller { get; }

        public CallExpr(IExpression caller, List<IExpression> args)
        {
            this.Caller = caller;
            this.Args = args;
        }

        public CallExpr(IExpression caller)
        {
            this.Caller = caller;
            this.Args = new List<IExpression>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Caller.ToString());
            sb.Append("(");
            for (int i = 0; i < this.Args.Count; i++)
            {
                sb.Append(this.Args[i].ToString());
                if (i != this.Args.Count - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.Append(")");
            return sb.ToString();
        }
    }
}