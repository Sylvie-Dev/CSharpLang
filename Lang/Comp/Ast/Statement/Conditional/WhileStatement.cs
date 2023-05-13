
using Voxelicious.Ast.Expression;

namespace Voxelicious.Ast.Statement
{
    public class WhileStatement : IConditionalStatement, IBlockStatement
    {
        public NodeType Kind { get; } = NodeType.WhileStatement;
        public IExpression Condition { get; }
        public List<IStatement> Body { get; }

        public WhileStatement(IExpression condition, List<IStatement> body)
        {
            this.Condition = condition;
            this.Body = body;
        }
    }
}