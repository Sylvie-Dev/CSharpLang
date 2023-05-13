
namespace Voxelicious.Ast.Expression
{

    public class IdentifierExpr : IExpression
    {
        public NodeType Kind => NodeType.Identifier;
        public string Token { get; }

        public IdentifierExpr(string token)
        {
            this.Token = token;
        }
    }
}