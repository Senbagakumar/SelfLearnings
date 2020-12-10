using System;
using System.Linq;

namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string message = args.Length > 0 ? string.Join(" ", args) : "Hellow World";
            Send.main(message);
        }
    }
}
