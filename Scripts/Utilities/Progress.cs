#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;

    public sealed class Progress : IProgress<float>
    {
        private readonly Action<float> onUpdate;

        public Progress(Action<float> onUpdate)
        {
            this.onUpdate = onUpdate;
        }

        private float lastValue = 0;

        void IProgress<float>.Report(float value)
        {
            if (value is < 0 or > 1) throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be between 0 and 1");
            if (value < this.lastValue) throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be greater than the last value");
            this.onUpdate(this.lastValue = value);
        }
    }

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
                return new Progress(value =>
                {
                    totalProgress += value - subProgress;
                    subProgress   =  value;
                    progress.Report(totalProgress / count);
                });
            }
        }
    }
}