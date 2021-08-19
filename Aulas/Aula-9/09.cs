using System;

//Operações de Bitwise
class Aula09
{
    static void Main()
    {
        int num = 0;
        int idade = 0;
        //<< dobra
        //< diminui pela metade

        Console.WriteLine("Informe sua idade: ");
        idade = int.Parse(Console.ReadLine());

        num = num>>10;
        Console.WriteLine(num);

        idade = idade +num;

        if (idade >= 18)
        {
            Console.WriteLine("Liberado");
        }
        else
        {
            Console.WriteLine("Precisa crescer garoto(a)");
        }
    }

}