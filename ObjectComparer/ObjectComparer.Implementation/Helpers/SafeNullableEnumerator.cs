using System.Collections;
using System.Collections.Generic;

namespace ObjectComparer.Implementation.Helpers
{
    public class SafeNullableEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;

        public SafeNullableEnumerator(IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                Completed = true;
                return;
            }

            _enumerator = enumerable.GetEnumerator();
        }

        public T Current { get; private set; }
        object IEnumerator.Current => Current;
        public bool Completed { get; private set; }

        public bool MoveNext()
        {
            if (Completed)
                return false;

            if (_enumerator.MoveNext())
            {
                Current = _enumerator.Current;
                return true;
            }

            Current = default(T);
            Completed = true;
            return false;
        }

        public void Reset()
        {
            if (_enumerator == null)
                return;

            _enumerator.Reset();
            Current = default(T);
            Completed = false;
        }

        public void Dispose()
        {
            _enumerator?.Dispose();
        }
    }
}
