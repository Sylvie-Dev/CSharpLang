
using Voxelicious.Ast.Expression;

namespace Voxelicious.Ast.Statement
{

    public class IfStatement : IBlockStatement, IConditionalStatement
    {
        public NodeType Kind => NodeType.IfStatement;
        public IExpression Condition { get; }
        public List<IStatement> Body { get; }
        public ElseIfStatement? ElseStatement { get; }

        public IfStatement(IExpression condition, List<IStatement> body, ElseIfStatement? elseIf = null)
        {
            this.Condition = condition;
            this.Body = body;
            this.ElseStatement = elseIf;
        }
    }
}