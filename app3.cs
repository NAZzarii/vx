using System;
using System.Linq;

class Worker : IComparable<Worker>
{
    private string name;
    private int age;
    private decimal salary;
    private DateTime hireDate;

    public string Name
    {
        get => name;
        set => name = !string.IsNullOrWhiteSpace(value) ? value : throw new Exception("Прізвище не може бути пустим");
    }

    public int Age
    {
        get => age;
        set => age = value > 0 ? value : throw new Exception("Вік має бути додатнім");
    }

    public decimal Salary
    {
        get => salary;
        set => salary = value >= 0 ? value : throw new Exception("ЗП не може бути від'ємною");
    }

    public DateTime HireDate
    {
        get => hireDate;
        set => hireDate = value <= DateTime.Now ? value : throw new Exception("Дата не може бути в майбутньому");
    }

    public int CompareTo(Worker other) => string.Compare(this.Name, other.Name);
}

class Calculator
{
    public double Add(double a, double b) => a + b;
    public double Sub(double a, double b) => a - b;
    public double Mul(double a, double b) => a * b;
    public double Div(double a, double b) => b != 0 ? a / b : throw new DivideByZeroException("Ділення на нуль неможливе");
}

class Program
{
    static void Main()
    {
        Worker[] workers = new Worker[5];
        for (int i = 0; i < 5; i++)
        {
            workers[i] = new Worker();
            try
            {
                Console.Write("Прізвище: ");
                workers[i].Name = Console.ReadLine();
                Console.Write("Вік: ");
                workers[i].Age = int.Parse(Console.ReadLine());
                Console.Write("ЗП: ");
                workers[i].Salary = decimal.Parse(Console.ReadLine());
                Console.Write("Дата (рррр-мм-дд): ");
                workers[i].HireDate = DateTime.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
                i--;
            }
        }

        Array.Sort(workers);
        Console.Write("Введіть стаж для пошуку: ");
        int minYears = int.Parse(Console.ReadLine());
        foreach (var w in workers)
        {
            if (DateTime.Now.Year - w.HireDate.Year > minYears)
                Console.WriteLine(w.Name);
        }

        Calculator calc = new Calculator();
        try
        {
            Console.Write("Число 1: ");
            double a = double.Parse(Console.ReadLine());
            Console.Write("Операція (+ - * /): ");
            string op = Console.ReadLine();
            Console.Write("Число 2: ");
            double b = double.Parse(Console.ReadLine());

            double res = op switch
            {
                "+" => calc.Add(a, b),
                "-" => calc.Sub(a, b),
                "*" => calc.Mul(a, b),
                "/" => calc.Div(a, b),
                _ => throw new Exception("Невідома операція")
            };
            Console.WriteLine("Результат: " + res);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка калькулятора: " + ex.Message);
        }
    }
}
