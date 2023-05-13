
namespace Voxelicious.Ast.LiteralExpression
{
    public class CharLiteral : ILiteralExpr<char>
    {
        public NodeType Kind => NodeType.CharLiteral;
        public char Value { get; }

        public CharLiteral(char value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}