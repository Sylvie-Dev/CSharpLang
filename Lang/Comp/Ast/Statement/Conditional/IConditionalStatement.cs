
using Voxelicious.Ast.Expression;

namespace Voxelicious.Ast.Statement
{

    public interface IConditionalStatement : IStatement
    {
        IExpression Condition { get; }
    }
}