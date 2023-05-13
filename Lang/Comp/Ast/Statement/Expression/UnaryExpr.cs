
namespace Voxelicious.Ast.Expression
{

    public class UnaryExpr : IExpression
    {
        public NodeType Kind { get; } = NodeType.UnaryExpr;
        public IExpression Right { get; }
        public string Operator { get; }
        public bool PostFix { get; set; } = false;

        public UnaryExpr(IExpression right, string op, bool postfix = false)
        {
            this.Right = right;
            this.Operator = op;
            this.PostFix = postfix;
        }
    }
}