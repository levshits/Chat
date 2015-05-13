using System;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Run();
            Console.ReadLine();
        
        }
    }
}
