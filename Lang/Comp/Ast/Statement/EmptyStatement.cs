
namespace Voxelicious.Ast.Statement
{

    public class EmptyStatement : IStatement
    {
        public NodeType Kind => NodeType.EmptyStatement;
    }
}