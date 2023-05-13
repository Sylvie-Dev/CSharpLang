
namespace Voxelicious.Ast.Statement
{

    public interface IBlockStatement : IStatement
    {
        new NodeType Kind => NodeType.BlockStatement;
        List<IStatement> Body { get; }
    }
}