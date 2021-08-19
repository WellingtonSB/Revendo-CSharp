using System;

//padrão .NET
namespace Aula_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Hello Word...");
            //Quebra a linha (printLn)
            Console.WriteLine("Hello World!");
            if(args.GetLength(0)>0){
                Console.Write(args.GetValue(0));
            }
        }
    }
}
