using System;

//Operadores e Operações
class Aula06
{
    static void Main(){
        int n1,n2,n3;
        n1=2;
        n2=3;
        n3=4;

        Console.WriteLine("n1=\t{0}\nn2=\t{1}\nn3=\t{2}",n1,n2,n3);

        double valorCompra = 4.30;
        double valorVenda;
        double lucro= 0.1;
        string produto= "Pastel";

        valorVenda = valorCompra+(valorCompra*lucro);
        Console.WriteLine("Produto..........:{0,15}",produto);
        Console.WriteLine("Valor Compra.....:{0,15:c}",valorCompra);
        Console.WriteLine("Lucro............:{0,15:p}",lucro);
        Console.WriteLine("Valor Venda .....:{0,15:c}",valorVenda);
     }
}