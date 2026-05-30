#nullable enable
namespace UniT.Extensions.Editor
{
    using UnityEditor;

    [CustomPropertyDrawer(typeof(SerializableReadOnlyList<>), useForChildren: true)]
    internal sealed class SerializableReadOnlyListDrawer : NestedPropertyDrawer
    {
        protected override string PropertyName => "values";
    }
}