using System;
using System.Collections;
using System.Collections.Generic;

namespace p05
{
    static class Program
    {
        static void Main(string[] args)
        {
            var lst = new PrimeList();
            int n = 1;
            foreach (int p in lst)
            {
                Console.WriteLine("p{0}: {1}", n++, p);
            }
        }
    }

    sealed class PrimeList : IEnumerable<int>
    {
        public static IEnumerator<int> Numbers()
        {
            for (int n = 2; ; ++n)
                yield return n;
        }

        public static IEnumerator<int> FilterPrime(IEnumerator<int> seq)
        {
            if (seq.MoveNext())
            {
                int p = seq.Current;
                yield return p;

                IEnumerator<int> seq2 = FilterPrime(seq);
                while (seq2.MoveNext())
                {
                    if (seq2.Current % p != 0)
                        yield return seq2.Current;
                }
            }
        }

        public static IEnumerator<int> Primes()
        {
            return FilterPrime(Numbers());
        }

        public IEnumerator<int> GetEnumerator()
        {
            return Primes();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
