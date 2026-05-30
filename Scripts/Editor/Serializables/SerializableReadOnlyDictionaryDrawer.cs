#nullable enable
namespace UniT.Extensions.Editor
{
    using UnityEditor;

    [CustomPropertyDrawer(typeof(SerializableReadOnlyDictionary<,>), useForChildren: true)]
    internal sealed class SerializableReadOnlyDictionaryDrawer : NestedPropertyDrawer
    {
        protected override string PropertyName => "values";
    }
}