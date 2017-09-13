using System;
using System.Threading.Tasks;

namespace Shared
{
    public class ExitApp
    {
        public void ExitAppProcess(SharedData shared, out bool stop)
        {
            stop = false;
            Console.WriteLine("Enter x to stop program: ");

            while (!stop)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo cki = Console.ReadKey();

                    if (cki.Key == ConsoleKey.X)
                    {
                        Console.WriteLine("App will close shortly");
                        stop = true;
                        break;
                    }
                }
                Task.Delay(1000);
            }
        }

    }
}
