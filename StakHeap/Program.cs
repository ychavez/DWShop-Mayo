using BenchmarkDotNet.Running;

namespace StakHeap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchi>();
        }
    }
}
