using System;

//Array/Vetor
class Aula15{
    static void Main(){
        int n1,n2,n3,n4,n5;
        int[] array = new int[5];

        for(int a=0; a<array.Length;a++){
            Console.WriteLine("Digite um numero: ");
            array[a] = int.Parse(Console.ReadLine());
        }
    
        for(int b=0; b<array.Length;b++){
            Console.WriteLine("Valor: "+array[b]);
        }
    }
}