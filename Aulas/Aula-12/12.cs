using System;
     //If & Else
class Aula12{
    static void Main(){
   
        int n1,n2,n3,n4,media = 0;
        string resultado ;

        Console.WriteLine("Informe a nota de Historia:");
        n1 = int.Parse(Console.ReadLine());
    
        Console.WriteLine("Informe a nota de Geografia: ");
        n2 = int.Parse(Console.ReadLine());
                
        Console.WriteLine("Informe a nota de Matematica: ");
        n3 = int.Parse(Console.ReadLine());
                
        Console.WriteLine("Informe a nota de Portugues: ");
        n4 = int.Parse(Console.ReadLine());

        media = (n1+n2+n3+n4)/4;

        if(media>=60){
            resultado = "Aprovado!";
        }else if(media>40 & media<=50){
                resultado = "de Exame, verifique com seu professor";
        }else{
            resultado = "Reprovado!";
        }

        Console.WriteLine("Voce esta: {0}\nMedia Final: {1}",resultado,media);
    }
}