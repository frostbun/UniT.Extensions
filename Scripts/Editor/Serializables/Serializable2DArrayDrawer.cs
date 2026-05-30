#nullable enable
namespace UniT.Extensions.Editor
{
    using UnityEditor;

    [CustomPropertyDrawer(typeof(Serializable2DArray<>), useForChildren: true)]
    internal sealed class Serializable2DArrayDrawer : NestedPropertyDrawer
    {
        protected override string PropertyName => "columns";
    }

    [CustomPropertyDrawer(typeof(Serializable2DArray<>.Column), useForChildren: true)]
    internal sealed class Serializable2DArrayColumnDrawer : NestedPropertyDrawer
    {
        protected override string PropertyName => "Cells".ToBackingFieldName();
    }
}