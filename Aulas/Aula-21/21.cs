using System;

//Argumento out

class Aula21
{
    static void Main(){
        int dividendo,divisor,quociente;
        int resto;

        dividendo = 10;
        divisor = 3;
        quociente = div(dividendo,divisor, out resto);

        Console.WriteLine("Quociente: "+ quociente+" Resto: "+ resto);


    }

    //permite mais de um retorno  no mesmo metodo
    static int div(int dividendo, int divisor, out int resto){
        int quociente;
        quociente = dividendo/divisor;
        resto = dividendo%divisor;

        return quociente;
    }
}