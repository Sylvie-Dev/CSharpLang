using System.Collections;

namespace Voxelicious.Util.List
{

    public class ShiftList<T> : List<T>
    {
        public ShiftList() : base() {}
        public ShiftList(params T[] collection) {
            foreach (T item in collection) Add(item);
        }
        public ShiftList(IEnumerable<T> collection) : base(collection) {}
        public ShiftList(int capacity) : base(capacity) {}

        public T[] ShiftMany(int length) {
            if (length > Count) throw new System.Exception("Cannot shift more than the length of the list");
            T[] shifted = new T[length];
            for (int i = 0; i < length; i++) {
                shifted[i] = this[i];
            }
            RemoveRange(0, length);
            return shifted;
        }

        public T Shift() => ShiftMany(1)[0];
    }
}