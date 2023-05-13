
using Voxelicious.Ast.Expression;

namespace Voxelicious.Ast.Statement
{

    public class ElseIfStatement : IfStatement
    {
        new public NodeType Kind => NodeType.ElseStatement;
        public bool HasCondition { get; }

        public ElseIfStatement(List<IStatement> body, IExpression condition, ElseIfStatement? elseIf = null)
            : base(condition, body, elseIf)
        {
            if (condition is EmptyExpr)
            {
                HasCondition = false;
            }
            else
            {
                HasCondition = true;
            }
        }
    }
}