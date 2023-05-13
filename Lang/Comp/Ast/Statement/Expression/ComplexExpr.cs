
using Voxelicious.Ast.Statement;

namespace Voxelicious.Ast.Expression
{

    public class ComplexExpr : IExpression
    {
        public NodeType Kind => NodeType.ComplexExpr;
        public List<IStatement> Body { get; } = new List<IStatement>();

        public ComplexExpr(List<IStatement> body)
        {
            this.Body = body;

            if (!body.Any())
                throw new Exception("Complex expression cannot be empty");
                
            if (body.Last().Kind != NodeType.ReturnStatement)
                throw new Exception("Complex expression must end with a return statement");
        }

        public ComplexExpr()
        {
        }
    }
}