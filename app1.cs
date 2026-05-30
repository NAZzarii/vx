using System;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Task t1 = new Task(() => Console.WriteLine(DateTime.Now));
        t1.Start();

        Task t2 = Task.Factory.StartNew(() => Console.WriteLine(DateTime.Now));

        Task t3 = Task.Run(() => Console.WriteLine(DateTime.Now));

        Task.WaitAll(t1, t2, t3);
        Console.WriteLine();


        Task task2 = Task.Run(() =>
        {
            for (int i = 2; i <= 1000; i++)
            {
                bool isPrime = true;
                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime) Console.Write(i + " ");
            }
            Console.WriteLine();
        });
        task2.Wait();
        Console.WriteLine();


        int start = 0;
        int end = 1000;
        Task<int> task3 = Task.Run(() =>
        {
            int count = 0;
            for (int i = Math.Max(2, start); i <= end; i++)
            {
                bool isPrime = true;
                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime) count++;
            }
            return count;
        });
        task3.Wait();
        Console.WriteLine(task3.Result);
        Console.WriteLine();


        int[] arr = { 12, 4, 7, 2, 9, 15, 3 };
        Task[] tasks = new Task[4];

        tasks[0] = Task.Run(() =>
        {
            int min = arr[0];
            foreach (int x in arr)
            {
                if (x < min) min = x;
            }
            Console.WriteLine(min);
        });

        tasks[1] = Task.Run(() =>
        {
            int max = arr[0];
            foreach (int x in arr)
            {
                if (x > max) max = x;
            }
            Console.WriteLine(max);
        });

        tasks[2] = Task.Run(() =>
        {
            double sum = 0;
            foreach (int x in arr)
            {
                sum += x;
            }
            Console.WriteLine(sum / arr.Length);
        });

        tasks[3] = Task.Run(() =>
        {
            int sum = 0;
            foreach (int x in arr)
            {
                sum += x;
            }
            Console.WriteLine(sum);
        });

        Task.WaitAll(tasks);
        Console.WriteLine();


        int[] arr5 = { 7, 2, 5, 2, 9, 7, 3 };
        int search = 5;

        Task<int[]> step1 = Task.Run(() =>
        {
            return arr5.Distinct().ToArray();
        });

        Task<int[]> step2 = step1.ContinueWith(t =>
        {
            int[] res = t.Result;
            Array.Sort(res);
            return res;
        });

        Task step3 = step2.ContinueWith(t =>
        {
            int[] res = t.Result;
            int index = Array.BinarySearch(res, search);
            Console.WriteLine(index);
        });

        step3.Wait();
    }
}
