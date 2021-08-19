using System;

//Passagem por valor e por referência

class Aula20
{
    static void Main()
    {
        int num= 10;
        dobrar(num);
        Console.WriteLine(num);

        dobrar2(ref num);
        Console.WriteLine(num);
    }

    //Passagem por valor
    static void dobrar(int valor){
        valor*=2;
    }

    //Passagem por referência
    static void dobrar2(ref int valor1){
        valor1*=2;
    }

}