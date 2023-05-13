using Voxelicious.Ast.Statement;

namespace Voxelicious.Ast.Expression
{

    public class MemberExpr : IExpression
    {
        public NodeType Kind => NodeType.MemberExpr;
        public IExpression Object { get; }
        public IExpression Member { get; }
        public bool IsComputed { get; }
        public string Operator { get; }

        public MemberExpr(IExpression obj, IExpression member, bool isComputed, string operatorCode)
        {
            this.Object = obj;
            this.Member = member;
            this.IsComputed = isComputed;
            this.Operator = operatorCode;
        }
    }
}