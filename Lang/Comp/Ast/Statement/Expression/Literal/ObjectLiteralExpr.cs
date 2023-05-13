using Voxelicious.Ast.Expression;
using Voxelicious.Ast.Statement;
using Voxelicious.Access;


namespace Voxelicious.Ast.LiteralExpression
{

    public class ObjectLiteralExpr : ILiteralExpr<List<PropertyLiteralExpr>>
    {

        public List<PropertyLiteralExpr> Value { get; }
        public NodeType Kind => NodeType.ObjectLiteralExpr;

        public ObjectLiteralExpr(List<PropertyLiteralExpr> value)
        {
            this.Value = value;
        }

        public ObjectLiteralExpr()
        {
            this.Value = new List<PropertyLiteralExpr>();
        }
    }
}