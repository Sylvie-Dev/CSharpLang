using Voxelicious.Ast.Expression;

namespace Voxelicious.Ast.LiteralExpression
{
    public interface ILiteralExpr<T> : IExpression
    {
        T Value { get; }
    }
}