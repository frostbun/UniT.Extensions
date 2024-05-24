#if UNIT_EXTENSIONS_UNITASK
#nullable enable
namespace UniT.Extensions
{
    using System;
    using Cysharp.Threading.Tasks;

    public static class UniTaskExtensions
    {
        public static async UniTask Catch<TException>(this UniTask task, Action<TException> handler) where TException : Exception
        {
            try
            {
                await task;
            }
            catch (TException e)
            {
                handler(e);
            }
        }

        public static async UniTask<T> Catch<T, TException>(this UniTask<T> task, Func<TException, T> handler) where TException : Exception
        {
            try
            {
                return await task;
            }
            catch (TException e)
            {
                return handler(e);
            }
        }

        public static UniTask Catch(this UniTask task, Action<Exception> handler)
        {
            return task.Catch<Exception>(handler);
        }

        public static UniTask<T> Catch<T>(this UniTask<T> task, Func<Exception, T> handler)
        {
            return task.Catch<T, Exception>(handler);
        }

        public static UniTask Catch(this UniTask task, Action handler)
        {
            return task.Catch(_ => handler());
        }

        public static UniTask<T> Catch<T>(this UniTask<T> task, Func<T> handler)
        {
            return task.Catch(_ => handler());
        }

        public static async UniTask Catch<TException>(this UniTask task, Func<TException, UniTask> handler) where TException : Exception
        {
            try
            {
                await task;
            }
            catch (TException e)
            {
                await handler(e);
            }
        }

        public static async UniTask<T> Catch<T, TException>(this UniTask<T> task, Func<TException, UniTask<T>> handler) where TException : Exception
        {
            try
            {
                return await task;
            }
            catch (TException e)
            {
                return await handler(e);
            }
        }

        public static UniTask Catch(this UniTask task, Func<Exception, UniTask> handler)
        {
            return task.Catch<Exception>(handler);
        }

        public static UniTask<T> Catch<T>(this UniTask<T> task, Func<Exception, UniTask<T>> handler)
        {
            return task.Catch<T, Exception>(handler);
        }

        public static UniTask Catch(this UniTask task, Func<UniTask> handler)
        {
            return task.Catch(_ => handler());
        }

        public static UniTask<T> Catch<T>(this UniTask<T> task, Func<UniTask<T>> handler)
        {
            return task.Catch(_ => handler());
        }

        public static async UniTask Finally(this UniTask task, Action handler)
        {
            try
            {
                await task;
            }
            finally
            {
                handler();
            }
        }

        public static async UniTask<T> Finally<T>(this UniTask<T> task, Action handler)
        {
            try
            {
                return await task;
            }
            finally
            {
                handler();
            }
        }

        public static async UniTask Finally(this UniTask task, Func<UniTask> handler)
        {
            try
            {
                await task;
            }
            finally
            {
                await handler();
            }
        }

        public static async UniTask<T> Finally<T>(this UniTask<T> task, Func<UniTask> handler)
        {
            try
            {
                return await task;
            }
            finally
            {
                await handler();
            }
        }
    }
}
#endif