using System;
using System.Collections.Generic;

namespace SimuladorSO.FS
{
    public class SistemaArquivos
    {
        public Diretorio Raiz { get; private set; }

        public SistemaArquivos()
        {
            Raiz = new Diretorio("root");
        }

        // caminho tipo "/": raiz, ou "/docs", "/docs/trabalhos"
        private Diretorio EncontrarDiretorio(string caminho)
        {
            if (string.IsNullOrEmpty(caminho) || caminho == "/")
            {
                return Raiz;
            }

            string[] partes = caminho.Split('/', StringSplitOptions.RemoveEmptyEntries);
            Diretorio atual = Raiz;

            foreach (var nomeDir in partes)
            {
                var proximo = atual.Subdiretorios.Find(d => d.Nome == nomeDir);
                if (proximo == null)
                {
                    // se não existe, cria (para simplificar)
                    proximo = new Diretorio(nomeDir);
                    atual.Subdiretorios.Add(proximo);
                }
                atual = proximo;
            }

            return atual;
        }

        public void CriarArquivo(string caminho, string nome, string conteudo)
        {
            var dir = EncontrarDiretorio(caminho);

            var arquivo = new ArquivoSim
            {
                Nome = nome,
                Conteudo = conteudo,
                Tamanho = conteudo.Length
            };

            dir.Arquivos.Add(arquivo);
            Console.WriteLine($"Arquivo '{nome}' criado em '{caminho}'.");
        }

        public void ApagarArquivo(string caminho, string nome)
        {
            var dir = EncontrarDiretorio(caminho);
            var arquivo = dir.Arquivos.Find(a => a.Nome == nome);

            if (arquivo != null)
            {
                dir.Arquivos.Remove(arquivo);
                Console.WriteLine($"Arquivo '{nome}' apagado de '{caminho}'.");
            }
            else
            {
                Console.WriteLine($"Arquivo '{nome}' não encontrado em '{caminho}'.");
            }
        }

        public void Listar(string caminho)
        {
            var dir = EncontrarDiretorio(caminho);

            Console.WriteLine($"Conteúdo de '{caminho}':");

            foreach (var d in dir.Subdiretorios)
            {
                Console.WriteLine($"[DIR]  {d.Nome}");
            }

            foreach (var a in dir.Arquivos)
            {
                Console.WriteLine($"[ARQ]  {a.Nome}  ({a.Tamanho} bytes)");
            }
        }
    }
}
