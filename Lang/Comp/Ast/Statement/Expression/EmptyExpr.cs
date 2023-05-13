
namespace Voxelicious.Ast.Expression
{
    public class EmptyExpr : IExpression
    {
        public NodeType Kind => NodeType.EmptyExpr;
    }
}