using Voxelicious.Ast.Expression;
using Voxelicious.Ast.Statement;

namespace Voxelicious.Ast.LiteralExpression
{

    public class PropertyLiteralExpr : ILiteralExpr<IExpression>
    {
        public string Key { get; set; }
        public IExpression Value { get; } = new EmptyExpr();
        public NodeType Kind => NodeType.PropertyLiteral;

        public PropertyLiteralExpr(string key, IExpression? value)
        {
            this.Key = key;
            if (value != null) this.Value = value;
        }
    }
}