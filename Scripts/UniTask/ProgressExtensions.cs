#if UNIT_EXTENSIONS_UNITASK
#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;

    public static class ProgressExtensions
    {
        public static IEnumerable<IProgress<float>?> CreateSubProgresses(this IProgress<float>? progress, int count)
        {
            var totalProgress = 0f;
            return IterTools.Repeat(CreateSubProgress, count);

            IProgress<float>? CreateSubProgress()
            {
                if (progress is null) return null;
                var subProgress = 0f;
                return Progress.CreateOnlyValueChanged<float>(value =>
                {
                    totalProgress += value - subProgress;
                    subProgress   =  value;
                    progress.Report(totalProgress / count);
                });
            }
        }
    }
}
#endif