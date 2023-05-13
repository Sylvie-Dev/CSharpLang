
namespace Voxelicious.Ast.Statement
{

    public interface IUpdateableStatement : IStatement
    {
        IStatement Initializer { get; }
        IStatement Updater { get; }
    }
}