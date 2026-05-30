#nullable enable
namespace UniT.Extensions
{
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public static class JsonExtensions
    {
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, JsonSettings);
        }

        private static readonly JsonSerializerSettings JsonSettings = new()
        {
            Culture               = CultureInfo.InvariantCulture,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting            = Formatting.Indented,
            Converters = new JsonConverter[]
            {
                new StringEnumConverter(),
            },
        };
    }
}