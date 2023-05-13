

namespace Voxelicious.Ast.Expression
{
    public class ComparisonExpr : IExpression
    {
        public NodeType Kind => NodeType.ComparisonExpr;
        public IExpression Left { get; }
        public IExpression Right { get; }
        public string Operator { get; }

        public ComparisonExpr(IExpression left, IExpression right, string op)
        {
            this.Left = left;
            this.Right = right;
            this.Operator = op;
        }
    }
}