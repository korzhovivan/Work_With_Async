using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dining_Philosofers
{
    public sealed class Filosof
    {
        static public void Eat(object firstStic, object secondStic, int FilosofNum, int FirstSticNum, int secondSticNum)
        {
            lock (firstStic)
            {
                Console.WriteLine("Filosof " + FilosofNum + "; s1: " + FirstSticNum);
                lock (secondStic)
                {
                    Console.WriteLine("Filosof " + FilosofNum + "; s2: " + secondSticNum);
                    Console.WriteLine("Filosof " + FilosofNum + "; eating ");
                }
                Console.WriteLine("Filosof " + FilosofNum + "; release s2: " + secondSticNum);
            }
            Console.WriteLine("Filosof " + FilosofNum + "; release s1: " + FirstSticNum);
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            var chopSticks = new Dictionary<int, object>(5);

            for (int i = 0; i < 5; i++)
            {
                chopSticks.Add(i, new object());
            }

            Task[] tasks = new Task[5];

            tasks[0] = new Task(() => Filosof.Eat(chopSticks[0], chopSticks[4], 0, 0, 4));


            for (int i = 1; i < 5; i++)
            {
                int ix = i;
                tasks[i] = new Task(() => Filosof.Eat(chopSticks[ix - 1], chopSticks[ix], ix, ix, ix + 1));
            }


            Parallel.ForEach(tasks, t => t.Start());
            Task.WaitAll(tasks);

            Console.ReadKey();
        }
    }
}
