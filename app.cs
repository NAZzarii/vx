using System;
using System.IO;
using System.Threading;

class Statistics
{
    public int Words;
    public int Lines;
    public int Punctuation;

    public readonly object syncObj = new object();
}

class Program
{
    static void Main()
    {
        Console.Write("Шлях до папки: ");
        string path = Console.ReadLine();

        if (!Directory.Exists(path))
        {
            Console.WriteLine("Папка не знайдена");
            return;
        }

        string[] files = Directory.GetFiles(path, "*.txt");
        Statistics stats = new Statistics();
        Thread[] threads = new Thread[files.Length];

        for (int i = 0; i < files.Length; i++)
        {
            string file = files[i];
            threads[i] = new Thread(() => Analyze(file, stats));
            threads[i].Start();
        }

        foreach (Thread t in threads)
        {
            t.Join();
        }

        Console.WriteLine($"Слів: {stats.Words}");
        Console.WriteLine($"Рядків: {stats.Lines}");
        Console.WriteLine($"Розділових знаків: {stats.Punctuation}");
    }

    static void Analyze(string file, Statistics stats)
    {
        string[] lines = File.ReadAllLines(file);
        int localLines = lines.Length;
        int localWords = 0;
        int localPunctuation = 0;

        string marks = ".,;:–—‒…!?\"'«»(){}[]<>/";

        foreach (string line in lines)
        {
            string[] words = line.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            localWords += words.Length;

            foreach (char c in line)
            {
                if (marks.Contains(c))
                {
                    localPunctuation++;
                }
            }
        }

        Interlocked.Add(ref stats.Lines, localLines);
        Interlocked.Add(ref stats.Words, localWords);

        Monitor.Enter(stats.syncObj);
        try
        {
            stats.Punctuation += localPunctuation;
        }
        finally
        {
            Monitor.Exit(stats.syncObj);
        }
    }
}
