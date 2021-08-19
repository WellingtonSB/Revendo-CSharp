using System;

//Operadores e Operações
class Aula05
{
    static void Main()
    {
        int resultado = (10 + 2) * 3;
        bool resp = 10 < 5; //boolean
        bool passou = false;

        /* 
        & = AND
        | = OR */

        if (!resp)
        {
            if (resultado == 36 & resultado > 30)
            {
                resultado +=2;
                passou = true;
                Console.WriteLine("AND,value: " + resultado);
                if (passou== true | resultado>36)
                {
                    Console.Write("OR, value: " + resultado);
                }
            }
        }
    }
}