
using Voxelicious.Ast.Expression;

namespace Voxelicious.Ast.LiteralExpression
{

    public class ArrayLiteralExpr : ILiteralExpr<IArray>
    {
        public NodeType Kind => NodeType.ArrayLiteralExpr;
        public IArray Value { get; }

        public ArrayLiteralExpr(IArray value)
        {
            this.Value = value;
        }
    }

    public interface IArray
    {
        IArrayElement[] Elements { get; }
    }

    public interface IArrayElement
    {
        IExpression Value { get; }
    }

    public class ImplArrayElement : IArrayElement
    {
        public IExpression Value { get; }

        public ImplArrayElement(IExpression value)
        {
            this.Value = value;
        }
    }

    public class ImplArray : IArray
    {
        public IArrayElement[] Elements { get; }

        public ImplArray(IArrayElement[] elements)
        {
            this.Elements = elements;
        }
    }
}