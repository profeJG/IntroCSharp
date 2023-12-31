internal class Program
{
    
    private static void Main(string[] args)
    {
        Console.WriteLine("Hola caracola!");
        
        var counter = 0;
        var max = args.Length is not 0 ? Convert.ToInt32(args[0]) : -1;
        while (max is -1 || counter < max)
        {
            Console.WriteLine($"Contador: {++counter}");
            // await Task.Delay(TimeSpan.FromMilliseconds(1_000));
        }
    }
}