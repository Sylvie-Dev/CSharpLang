

using System.Text;

namespace Voxelicious.Ast.Statement
{

    public class ProgramStatement : IBlockStatement
    {
        public NodeType Kind { get; } = NodeType.Program;
        public List<IStatement> Body { get; }

        public ProgramStatement(List<IStatement> body)
        {
            this.Body = body;
        }

        public ProgramStatement()
        {
            this.Body = new List<IStatement>();
        }

    }
}