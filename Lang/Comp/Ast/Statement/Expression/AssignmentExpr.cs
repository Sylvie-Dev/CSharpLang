
using Voxelicious.Ast.Statement;

namespace Voxelicious.Ast.Expression
{

    public class AssignmentExpr : IExpression
    {
        public NodeType Kind => NodeType.AssignmentExpr;
        public IExpression Assigne { get; }
        public IExpression Value { get; } = new EmptyExpr();
        public bool IsComplex { get; } = false;
        public ComplexExpr ComplexExpression { get; } = new ComplexExpr();

        public AssignmentExpr(IExpression assigne, IExpression? value, ComplexExpr? complexExpression = null)
        {
            this.Assigne = assigne;
            if (value != null) this.Value = value;
            this.IsComplex = complexExpression != null;
            if (complexExpression != null) this.ComplexExpression = complexExpression;
        }
    }
}