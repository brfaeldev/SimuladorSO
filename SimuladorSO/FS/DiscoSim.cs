using System;
using System.Collections.Generic;

namespace SimuladorSO.FS
{
    public class DiscoSim
    {
        public int CapacidadeGB { get; private set; }
        private int usadoGB = 0;

        private List<string> arquivos = new List<string>();

        public DiscoSim(int capacidadeGB)
        {
            CapacidadeGB = capacidadeGB;
        }

        //  Tamanho em GB
        public bool CriarArquivo(string nome, int tamanhoGb)
        {
            if (usadoGB + tamanhoGb <= CapacidadeGB)
            {
                arquivos.Add(nome);
                usadoGB += tamanhoGb;
                Console.WriteLine($"[DISCO] Arquivo '{nome}' criado. Uso: {usadoGB}GB / {CapacidadeGB}GB");
                return true;
            }

            Console.WriteLine("[DISCO] Espaço insuficiente para criar arquivo.");
            return false;
        }

        public void RemoverArquivo(string nome, int tamanhoGb)
        {
            if (arquivos.Remove(nome))
            {
                usadoGB = Math.Max(0, usadoGB - tamanhoGb);
                Console.WriteLine($"[DISCO] Arquivo '{nome}' removido. Uso: {usadoGB}GB / {CapacidadeGB}GB");
            }
        }

        public void ListarArquivos()
        {
            Console.WriteLine("[DISCO] Arquivos:");
            if (arquivos.Count == 0)
            {
                Console.WriteLine(" (nenhum arquivo)");
                return;
            }

            foreach (var a in arquivos)
            {
                Console.WriteLine(" - " + a);
            }
            Console.WriteLine($"[DISCO] Usado: {usadoGB}GB / {CapacidadeGB}GB");
        }
    }
}
