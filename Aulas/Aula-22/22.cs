using System;

//Argumento params 

class Aula21
{
    static void Main(){
        soma(10,30,40,50,60,70,380,8);
    }

    //me permite passar mais de um valor, atravez de um array.
    
    static void soma(params int[]n){
        int res=0;

        if(n.Length< 1){
            Console.WriteLine("Não existe valores a serem somados");
        }else if(n.Length<2){
            Console.WriteLine("Valores insuficientes para soma");
        }else{
            //tomar cuidado com a forma em que farei a soma de cada valor
            for(int i=0; i<n.Length;i++){
                res += n[i];
            }
        }
        Console.WriteLine("A soma dos valores: {0}",res);
        
    }
}