using System;

//SWITCH case
class Aula13
{
    static void Main()
    {
        double valorIda = 0;
        double valorVolta = 0;
        double valorPassagem = 0;
        char escolha;
        Console.WriteLine("Escolha o seu destino de viagem: ");

        Console.WriteLine("Qual o estado da ida?");
        Console.WriteLine("[a]SP\n[b]RJ\n[c]MG\n[d]BH");
        escolha = char.Parse(Console.ReadLine());

        Console.WriteLine("Qual o estado da volta?");
        Console.WriteLine("[a]SP\n[b]RJ\n[c]MG\n[d]BH");
        escolha = char.Parse(Console.ReadLine());
        switch (escolha)
        {
            case 'a':
                valorIda = 10;
                break;
            case 'b':
                valorIda = 20;
                break;
            case 'c':
                valorIda = 30;
                break;
            case 'd':
                valorIda = 40;
                break;
            default:
                valorIda = 0;
                break;
        }

        switch (escolha)
        {
            case 'a':
                valorVolta = 10;
                break;
            case 'b':
                valorVolta = 20;
                break;
            case 'c':
                valorVolta = 30;
                break;
            case 'd':
                valorVolta = 40;
                break;
            default:
                valorVolta = 0;
                break;
        }

        valorPassagem = valorIda + valorVolta;
        Console.WriteLine("Valor da sua passagem: " + valorPassagem);

    }
}