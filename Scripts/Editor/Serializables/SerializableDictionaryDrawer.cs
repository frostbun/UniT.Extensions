#nullable enable
namespace UniT.Extensions.Editor
{
    using UnityEditor;

    [CustomPropertyDrawer(typeof(SerializableDictionary<,>), useForChildren: true)]
    internal sealed class SerializableDictionaryDrawer : NestedPropertyDrawer
    {
        protected override string PropertyName => "values";
    }
}