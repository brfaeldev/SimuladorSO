using System;
using SimuladorSO.Core;

namespace SimuladorSO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var so = new SistemaOperacionalSim();
            bool rodando = true;

            while (rodando)
            {
                Console.WriteLine("\n=== MENU ===");
                Console.WriteLine("1 - Instalar programa");
                Console.WriteLine("2 - Listar processos");
                Console.WriteLine("3 - Executar programa (carregar na RAM)");
                Console.WriteLine("4 - Executar 1 tick");
                Console.WriteLine("5 - Executar 5 ticks");
                Console.WriteLine("6 - Status do sistema");
                Console.WriteLine("7 - Encerrar processo");
                Console.WriteLine("8 - Desinstalar programa");
                Console.WriteLine("9 - Sair");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.Write("Nome do programa: ");
                        string nome = Console.ReadLine();
                        Console.Write("Tamanho no disco (GB): ");
                        int mem = int.Parse(Console.ReadLine() ?? "0");
                        so.InstalarPrograma(nome, mem);
                        break;

                    case "2":
                        so.ListarProcessos();
                        break;

                    case "3":
                        Console.Write("PID do programa a executar: ");
                        int pidCarregar = int.Parse(Console.ReadLine() ?? "0");
                        so.CarregarProcessoNaRam(pidCarregar);
                        break;

                    case "4":
                        so.ExecutarTick();
                        break;

                    case "5":
                        for (int i = 0; i < 5; i++)
                        {
                            so.ExecutarTick();
                        }
                        break;

                    case "6":
                        so.Status();
                        break;

                    case "7":
                        Console.Write("PID do processo a encerrar: ");
                        int pidEncerrar = int.Parse(Console.ReadLine() ?? "0");
                        so.EncerrarProcesso(pidEncerrar);
                        break;

                    case "8":
                        Console.Write("PID do programa a desinstalar: ");
                        int pidDesinstalar = int.Parse(Console.ReadLine() ?? "0");
                        so.DesinstalarPrograma(pidDesinstalar);
                        break;

                    case "9":
                        rodando = false;
                        break;

                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
        }
    }
}
