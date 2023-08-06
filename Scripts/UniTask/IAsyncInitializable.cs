#if UNIT_EXTENSIONS_UNITASK
namespace UniT.Extensions
{
    using Cysharp.Threading.Tasks;

    public interface IAsyncInitializable
    {
        public UniTask InitializeAsync();
    }
}
#endif