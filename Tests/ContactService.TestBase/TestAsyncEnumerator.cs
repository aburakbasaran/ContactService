using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactService.TestBase
{
    public class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;

        public TestAsyncEnumerator(IEnumerator<T> enumerator)
        {
            _enumerator = enumerator ?? throw new ArgumentNullException(nameof(enumerator));
        }

        public T Current => _enumerator.Current;

        public ValueTask DisposeAsync()
        {
            _enumerator.Dispose();
            return new();
        }

        public ValueTask<bool> MoveNextAsync()
        {
            return new(_enumerator.MoveNext());
        }
    }
}