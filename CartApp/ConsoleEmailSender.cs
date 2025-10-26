using CartApp.Calculator;
using System;

public class Class1
{
    public class ConsoleEmailSender : IEmailSender
    {
        public void Send(string to, string subject, string body)
        {
            Console.WriteLine("\n=== Enviando Email ===");
            Console.WriteLine($"Para: {to}");
            Console.WriteLine($"Asunto: {subject}");
            Console.WriteLine("Contenido:");
            Console.WriteLine(body);
            Console.WriteLine("======================\n");
        }
    }
}
