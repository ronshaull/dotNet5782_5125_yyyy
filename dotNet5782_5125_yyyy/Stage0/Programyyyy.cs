namespace Stage0
{
    partial class Program
    {
        private static void Main(string[] args)
        {
            welcome5125();
            Console.ReadKey();
        }
        static partial void welcomeyyyy();
        private static void welcome5125()
        {
            string name = "";
            Console.Write("Enter your name: ");
            name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first application!", name);
        }
    }
}