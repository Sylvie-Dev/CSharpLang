using Voxelicious.Access;


namespace Voxelicious.Ast.LiteralExpression
{
    public class ObjectLiteral : ILiteralExpr<ObjectLiteralExpr>
    {
        public bool IsConstant { get; set; } = false;
        public string Identifier { get; set; }
        public AccessModifier AccessModifier { get; set; } = AccessModifier.Public;
        public ObjectLiteralExpr Value { get; set; }
        public NodeType Kind => NodeType.ObjectLiteral;

        public ObjectLiteral(string identifier, ObjectLiteralExpr value, AccessModifier accessModifier = AccessModifier.Public, bool isConstant = false)
        {
            this.Identifier = identifier;
            this.Value = value;
            this.AccessModifier = accessModifier;
            this.IsConstant = isConstant;
        }
        
    }
}