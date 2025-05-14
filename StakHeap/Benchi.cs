using BenchmarkDotNet.Attributes;

namespace StakHeap
{
   [ MemoryDiagnoser]
    public class Benchi
    {

        public static string _dateAsText = "13 05 2025";

        [Benchmark]
        public (int day, int month, int year) DateWithString()
        {
            var dayAsText = _dateAsText.Substring(0, 2);
            var monthAsText = _dateAsText.Substring(3, 2);
            var yearAsText = _dateAsText.Substring(6, 4);

            int day = int.Parse(dayAsText);
            int month = int.Parse(monthAsText);
            int year = int.Parse(yearAsText);

            return (day, month, year);
        }

        [Benchmark]
        public (int day, int month, int year) DateWithStringSpan()
        {
            ReadOnlySpan<char> dateSpan = _dateAsText;

            var dayAsText = dateSpan.Slice(0, 2);
            var monthAsText = dateSpan.Slice(3, 2);
            var yearAsText = dateSpan.Slice(6, 4);

            int day = int.Parse(dayAsText);
            int month = int.Parse(monthAsText);
            int year = int.Parse(yearAsText);

            return (day, month, year);
        }


    }
}
