using System;

//Loop FOREACH / Estruturas de iteração

class Aula18 {
    static void Main(){
        int[] vetor = new int [3]{10,20,30};

        for(int a = 0; a <=vetor.Length-1; a++){
            Console.WriteLine(vetor[a]);
            Console.Write("\n");
        }

        //recomendado apenas para leitura de valores
        foreach(int b in vetor){
             Console.WriteLine(b);
        }
    }
}