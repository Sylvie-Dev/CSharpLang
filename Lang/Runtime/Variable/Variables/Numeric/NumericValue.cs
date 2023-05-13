
using Voxelicious.Enviornment;

namespace Voxelicious.Runtime.Variable
{

    public abstract class NumberValue<T> : IRuntimeValue<T> where T : struct {
        public T Value { get; set; }
        public abstract ValueType Type { get; }

        public NumberValue(T value) => Value = value;
        
        public override string ToString() => Value!.ToString()!;

        /*        IRuntimeVariable Add(IRuntimeVariable other, IEnviornment enviornment) => new NullValue();
        IRuntimeVariable Subtract(IRuntimeVariable other, IEnviornment enviornment) => new NullValue();
        IRuntimeVariable Multiply(IRuntimeVariable other, IEnviornment enviornment) => new NullValue();
        IRuntimeVariable Divide(IRuntimeVariable other, IEnviornment enviornment) => new NullValue();
        IRuntimeVariable Modulo(IRuntimeVariable other, IEnviornment enviornment) => new NullValue();
        IRuntimeVariable Power(IRuntimeVariable other, IEnviornment enviornment) => new NullValue();
        IRuntimeVariable And(IRuntimeVariable other, IEnviornment enviornment) => new NullValue();
        IRuntimeVariable Or(IRuntimeVariable other, IEnviornment enviornment) => new NullValue();
        IRuntimeVariable Xor(IRuntimeVariable other, IEnviornment enviornment) => new NullValue();
        IRuntimeVariable Not(IEnviornment enviornment) => new NullValue();
        IRuntimeVariable ShiftLeft(IRuntimeVariable other, IEnviornment enviornment) => new NullValue();
        IRuntimeVariable ShiftRight(IRuntimeVariable other, IEnviornment enviornment) => new NullValue();
        IRuntimeVariable Equal(IRuntimeVariable other, IEnviornment enviornment) => new NullValue();
        IRuntimeVariable NotEqual(IRuntimeVariable other, IEnviornment enviornment) => new NullValue();
        IRuntimeVariable GreaterThan(IRuntimeVariable other, IEnviornment enviornment) => new NullValue();
        IRuntimeVariable LessThan(IRuntimeVariable other, IEnviornment enviornment) => new NullValue();
        IRuntimeVariable GreaterThanOrEqual(IRuntimeVariable other, IEnviornment enviornment) => new NullValue();
        IRuntimeVariable LessThanOrEqual(IRuntimeVariable other, IEnviornment enviornment) => new NullValue();
        IRuntimeVariable Negate(IEnviornment enviornment) => new NullValue();
        IRuntimeVariable Increment(IEnviornment enviornment) => new NullValue();
        IRuntimeVariable Decrement(IEnviornment enviornment) => new NullValue();*/

        public IRuntimeVariable Add(IRuntimeVariable other, IEnviornment enviornment)
        {
            if (other is IRuntimeValue<T> otherValue)
            {
                T result = (dynamic) Value + (dynamic) otherValue.Value;
                return new ImplNumberValue<T>(result);
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public IRuntimeVariable Subtract(IRuntimeVariable other, IEnviornment enviornment)
        {
            if (other is IRuntimeValue<T> otherValue)
            {
                T result = (dynamic) Value - (dynamic) otherValue.Value;
                return new ImplNumberValue<T>(result);
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public IRuntimeVariable Multiply(IRuntimeVariable other, IEnviornment enviornment)
        {
            if (other is IRuntimeValue<T> otherValue)
            {
                T result = (dynamic) Value * (dynamic) otherValue.Value;
                return new ImplNumberValue<T>(result);
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public IRuntimeVariable Divide(IRuntimeVariable other, IEnviornment enviornment)
        {
            if (other is IRuntimeValue<T> otherValue)
            {
                T result = (dynamic) Value / (dynamic) otherValue.Value;
                return new ImplNumberValue<T>(result);
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public IRuntimeVariable Modulo(IRuntimeVariable other, IEnviornment enviornment)
        {
            if (other is IRuntimeValue<T> otherValue)
            {
                T result = (dynamic) Value % (dynamic) otherValue.Value;
                return new ImplNumberValue<T>(result);
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public IRuntimeVariable Power(IRuntimeVariable other, IEnviornment enviornment)
        {
            if (other is IRuntimeValue<T> otherValue)
            {
                T result = (dynamic) Value ^ (dynamic) otherValue.Value;
                return new ImplNumberValue<T>(result);
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public IRuntimeVariable And(IRuntimeVariable other, IEnviornment enviornment)
        {
            if (other is IRuntimeValue<T> otherValue)
            {
                T result = (dynamic) Value & (dynamic) otherValue.Value;
                return new ImplNumberValue<T>(result);
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public IRuntimeVariable Or(IRuntimeVariable other, IEnviornment enviornment)
        {
            if (other is IRuntimeValue<T> otherValue)
            {
                T result = (dynamic) Value | (dynamic) otherValue.Value;
                return new ImplNumberValue<T>(result);
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public IRuntimeVariable Xor(IRuntimeVariable other, IEnviornment enviornment)
        {
            if (other is IRuntimeValue<T> otherValue)
            {
                T result = (dynamic) Value ^ (dynamic) otherValue.Value;
                return new ImplNumberValue<T>(result);
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public IRuntimeVariable Not(IEnviornment enviornment)
        {
            if (Value is bool)
            {
                bool result = !(dynamic) Value;
                return new BooleanValue(result);
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public IRuntimeVariable ShiftLeft(IRuntimeVariable other, IEnviornment enviornment)
        {
            if (other is IRuntimeValue<T> otherValue)
            {
                T result = (dynamic) Value << (dynamic) otherValue.Value;
                return new ImplNumberValue<T>(result);
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public IRuntimeVariable ShiftRight(IRuntimeVariable other, IEnviornment enviornment)
        {
            if (other is IRuntimeValue<T> otherValue)
            {
                T result = (dynamic) Value >> (dynamic) otherValue.Value;
                return new ImplNumberValue<T>(result);
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public IRuntimeVariable Equal(IRuntimeVariable other, IEnviornment enviornment)
        {
            if (other is IRuntimeValue<T> otherValue)
            {
                bool result = (dynamic) Value == (dynamic) otherValue.Value;
                return new BooleanValue(result);
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public IRuntimeVariable NotEqual(IRuntimeVariable other, IEnviornment enviornment)
        {
            if (other is IRuntimeValue<T> otherValue)
            {
                bool result = (dynamic) Value != (dynamic) otherValue.Value;
                return new BooleanValue(result);
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public IRuntimeVariable GreaterThan(IRuntimeVariable other, IEnviornment enviornment)
        {
            if (other is IRuntimeValue<T> otherValue)
            {
                bool result = (dynamic) Value > (dynamic) otherValue.Value;
                return new BooleanValue(result);
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public IRuntimeVariable LessThan(IRuntimeVariable other, IEnviornment enviornment)
        {
            if (other is IRuntimeValue<T> otherValue)
            {
                bool result = (dynamic) Value < (dynamic) otherValue.Value;
                return new BooleanValue(result);
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public IRuntimeVariable GreaterThanOrEqual(IRuntimeVariable other, IEnviornment enviornment)
        {
            if (other is IRuntimeValue<T> otherValue)
            {
                bool result = (dynamic) Value >= (dynamic) otherValue.Value;
                return new BooleanValue(result);
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public IRuntimeVariable LessThanOrEqual(IRuntimeVariable other, IEnviornment enviornment)
        {
            if (other is IRuntimeValue<T> otherValue)
            {
                bool result = (dynamic) Value <= (dynamic) otherValue.Value;
                return new BooleanValue(result);
            }
            else
            {
                throw new InvalidCastException();
            }
        }
    }

    public class ImplNumberValue<T> : NumberValue<T> where T : struct
    {
        public ImplNumberValue(T value) : base(value) { }
        public override ValueType Type => ValueType.Byte;
    }
}