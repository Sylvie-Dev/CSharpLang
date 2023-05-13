
using Voxelicious.Ast.Expression;

namespace Voxelicious.Ast.Statement
{
    public class ReturnStatement : IStatement
    {
        public NodeType Kind { get; } = NodeType.ReturnStatement;
        public IExpression? Value { get; }

        public ReturnStatement(IExpression? expression)
        {
            this.Value = expression;
        }
    }
}