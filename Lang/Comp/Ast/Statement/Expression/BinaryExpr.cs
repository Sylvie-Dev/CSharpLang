using Voxelicious.Ast.Statement;

namespace Voxelicious.Ast.Expression
{

    public class BinaryExpr : IExpression
    {
        public NodeType Kind => NodeType.BinaryExpr;
        public IExpression Left { get; }
        public IExpression Right { get; }
        public string Operator { get; }

        public BinaryExpr(IExpression left, IExpression right, string operatorCode)
        {
            this.Left = left;
            this.Right = right;
            this.Operator = operatorCode;
        }
    }
}