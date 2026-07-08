class TypesPractice
{
    private static void Main()
    {
        var p1 = new Point
        {
            X = 1,
            Y = 2
        };

        var p2 = p1;
        p2.X = 99;
        
        var m1 = new Money(100, "RUB");
        var m2 = m1 with {Amount = 200};
        var m3 = new Money(100, "RUB");
        
        Console.WriteLine(p1.X); // Результат останется 1, так как мы не меняли этот параметр у p1, меняли только у p2, но это другой объект, который просто являлся копией p1
        Console.WriteLine(m1 == m3); // Вернёт true, так как у типа record, оператор == переопледелен на сравнение значений внутри объектов, а не ссылок
        Console.WriteLine(ReferenceEquals(m1, m3)); //Вернёт false, так как сравниванием непосредственно указывают ли ссылки на один объект в памяти
        
        ResetWithoutRef(p2);
        
        Console.WriteLine($"X:{p2.X}  Y:{p2.Y}");
        
        ResetWithRef(ref p2);
        
        Console.WriteLine($"X:{p2.X}  Y:{p2.Y}");
    }
    
    private static void ResetWithoutRef(Point pt)
    {
        pt.X = 0;
        pt.Y = 0;
    }

    private static void ResetWithRef(ref Point pt)
    {
        pt.X = 0;
        pt.Y = 0;
    }
    
}

public struct Point
{
    public int X;
    public int Y;
}

public record Money(decimal Amount, string Currency);