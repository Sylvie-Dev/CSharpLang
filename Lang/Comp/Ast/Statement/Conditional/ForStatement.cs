using Voxelicious.Ast.Expression;

namespace Voxelicious.Ast.Statement
{

    public class ForStatement : IConditionalStatement, IUpdateableStatement, IBlockStatement
    {
        public NodeType Kind { get; } = NodeType.ForStatement;
        public IExpression Condition { get; }
        public List<IStatement> Body { get; }
        public IStatement Initializer { get; }
        public IStatement Updater { get; }

        public ForStatement(IStatement initializer, IExpression condition, IStatement updater, List<IStatement> body)
        {
            this.Initializer = initializer;
            this.Condition = condition;
            this.Updater = updater;
            this.Body = body;
        }
    }
}