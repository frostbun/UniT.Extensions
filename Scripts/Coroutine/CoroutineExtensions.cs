#if !UNIT_UNITASK
namespace UniT.Extensions
{
    using System;
    using System.Collections;

    public static class CoroutineExtensions
    {
        public static IEnumerator Then(this IEnumerator coroutine, Action callback)
        {
            yield return coroutine;
            callback();
        }

        public static IEnumerator Then(this IEnumerator coroutine, Func<IEnumerator> callback)
        {
            yield return coroutine;
            yield return callback();
        }

        public static IEnumerator Finally(this IEnumerator coroutine, Action handler)
        {
            try
            {
                yield return coroutine;
            }
            finally
            {
                handler();
            }
        }

        public static void Wait(this IEnumerator coroutine)
        {
            while (coroutine.MoveNext()) ;
        }
    }
}
#endif