

namespace Voxelicious.Ast.LiteralExpression
{

    public interface INumber<T>
    {
        T Value { get; }

        int GetAsInt();
        double GetAsDouble();
        float GetAsFloat();
        long GetAsLong();
        short GetAsShort();
        byte GetAsByte();
    }

    public class IntegerNumber : INumber<int>
    {
        public int Value { get; }

        public IntegerNumber(int value)
        {
            this.Value = value;
        }

        public int GetAsInt() => Value;
        public double GetAsDouble() => Value;
        public float GetAsFloat() => Value;
        public long GetAsLong() => Value;
        public short GetAsShort() => (short) Value;
        public byte GetAsByte() => (byte) Value;
    }

    public class LongNumber : INumber<long>
    {
        public long Value { get; }

        public LongNumber(long value)
        {
            this.Value = value;
        }

        public int GetAsInt() => (int) Value;
        public double GetAsDouble() => Value;
        public float GetAsFloat() => Value;
        public long GetAsLong() => Value;
        public short GetAsShort() => (short) Value;
        public byte GetAsByte() => (byte) Value;
    }

    public class ShortNumber : INumber<short>
    {
        public short Value { get; }

        public ShortNumber(short value)
        {
            this.Value = value;
        }

        public int GetAsInt() => Value;
        public double GetAsDouble() => Value;
        public float GetAsFloat() => Value;
        public long GetAsLong() => Value;
        public short GetAsShort() => Value;
        public byte GetAsByte() => (byte) Value;
    }

    public class FloatNumber : INumber<float>
    {
        public float Value { get; }

        public FloatNumber(float value)
        {
            this.Value = value;
        }

        public int GetAsInt() => (int) Value;
        public double GetAsDouble() => Value;
        public float GetAsFloat() => Value;
        public long GetAsLong() => (long) Value;
        public short GetAsShort() => (short) Value;
        public byte GetAsByte() => (byte) Value;
    }

    public class DoubleNumber : INumber<double>
    {
        public double Value { get; }

        public DoubleNumber(double value)
        {
            this.Value = value;
        }

        public int GetAsInt() => (int) Value;
        public double GetAsDouble() => Value;
        public float GetAsFloat() => (float) Value;
        public long GetAsLong() => (long) Value;
        public short GetAsShort() => (short) Value;
        public byte GetAsByte() => (byte) Value;
    }

    public class ByteNumber : INumber<byte>
    {
        public byte Value { get; }

        public ByteNumber(byte value)
        {
            this.Value = value;
        }

        public int GetAsInt() => Value;
        public double GetAsDouble() => Value;
        public float GetAsFloat() => Value;
        public long GetAsLong() => Value;
        public short GetAsShort() => Value;
        public byte GetAsByte() => Value;
    }
    
    
    
}