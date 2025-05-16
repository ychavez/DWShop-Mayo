namespace TaskAsync
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var numeros = Enumerable.Range(0, 30_000).ToList();


            var semaphore = new SemaphoreSlim(10000);

            List<Task> cobros = numeros.Select(async c =>
            {
                await semaphore.WaitAsync();
                try
                {
                    await Cobrar(c);
                }
                finally
                {

                    semaphore.Release();
                }
            }).ToList();


            var saludarTask = Saludar();


            await Task.WhenAll(cobros);
            await saludarTask;
        }

        private static async Task Saludar()
        {
            await Task.Delay(1000);
            Console.WriteLine("Hola desde el saludo");

        }


        private static async Task<bool> Cobrar(int numero)
        {
            await Task.Delay(2000);
            Random random = new Random();
            if (random.Next(2) == 0)
            {
                Console.WriteLine($"Cobro exitoso {numero}");
            }
            else
            {
                Console.WriteLine($"Cobro no exitoso {numero}");
            }

            return random.Next(2) == 0;
        }

    }
}
