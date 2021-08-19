using System;

//Conversões de tipos (typecast) 
class Aula11{
    static void Main(){
        int n1 = 10;
        float n2 =n1; //convesão implicita(Segura)

        //float n3 = 10.2f;
        //int n4=n3; convesão explicita(não é segura) == ERRO!
        float n3 = 10.2f;
        int n4 = (int)n3;//conversao por typecast

        Console.WriteLine(n2);
        Console.WriteLine(n4);

    }
}