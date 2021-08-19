using System;

class Aula08
{
    static void Main()
    {
        string user;
        double senha;

        Console.WriteLine("Digite um usuario: ");
        user = Console.ReadLine();

        Console.WriteLine("Digite uma senha: ");
        senha = int.Parse(Console.ReadLine());

        if (user != "")
        {
            Console.WriteLine("Usuario: {0}\nSenha: {1}", user, senha);
        }

    }
}

