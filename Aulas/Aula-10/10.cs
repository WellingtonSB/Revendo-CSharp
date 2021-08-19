using System;

//Enumeradores (enum)
class Aula10{
    enum DiaSemana { Domingo, Segunda, Terca, Quarta, Quinta, Sexta, Sabado };
    static void Main(){
        int diaDaSemana = (int) DiaSemana.Sabado;

        DiaSemana ds = (DiaSemana)3;
        Console.WriteLine(ds);
    }
}