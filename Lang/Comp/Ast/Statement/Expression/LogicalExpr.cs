

namespace Voxelicious.Ast.Expression
{

    public class LogicalExpr : IExpression
    {
        public NodeType Kind => NodeType.LogicalExpr;
        public IExpression Left { get; }
        public IExpression Right { get; }
        public string Operator { get; }

        public LogicalExpr(IExpression left, IExpression right, string op)
        {
            this.Left = left;
            this.Right = right;
            this.Operator = op;
        }
    }
}