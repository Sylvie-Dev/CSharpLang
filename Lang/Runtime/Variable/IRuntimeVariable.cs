

using Voxelicious.Enviornment;

namespace Voxelicious.Runtime.Variable 
{
    public interface IRuntimeVariable
    {
        ValueType Type { get; }

        String GetTypeString() => ValueTypeExtensions.GetString(Type);

        IRuntimeVariable Add(IRuntimeVariable other, IEnviornment enviornment) => new NullValue();
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
        IRuntimeVariable Decrement(IEnviornment enviornment) => new NullValue();
    }

    public interface IRuntimeValue<T> : IRuntimeVariable
    {
        T Value { get; }

        String? GetValueString() => Value!.ToString();
    }

    public class DefaultArg
    {
        public ValueType ArgType { get; }
        public bool IsDefault { get; }
        public IRuntimeVariable DefaultValue { get; } = new NullValue();

        public DefaultArg(ValueType argType, IRuntimeVariable? defaultValue = null)
        {
            ArgType = argType;
            IsDefault = defaultValue != null;
            DefaultValue = defaultValue ?? new NullValue();
        }
    }

    public interface ICallable<T>
    {
        DefaultArg[] DefaultArgs { get; }

        T Call (List<IRuntimeVariable> args, IEnviornment enviornment);
    }
}