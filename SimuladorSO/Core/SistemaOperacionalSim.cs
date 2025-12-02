using System;
using System.Collections.Generic;
using SimuladorSO.Core;
using SimuladorSO.FS;
using SimuladorSO.Memory;
using SimuladorSO.Model;

namespace SimuladorSO.Core
{
    public class SistemaOperacionalSim
    {
        public KernelSim Kernel { get; private set; }
        public RAMSim RamFisica { get; private set; }
        public DiscoSim DiscoFisico { get; private set; }
        public SistemaArquivos SistemaArquivos { get; private set; }

        public SistemaOperacionalSim()
        {
            Kernel = new KernelSim();
            RamFisica = new RAMSim(8192);   // total físico
            DiscoFisico = new DiscoSim(32); // total físico
            SistemaArquivos = new SistemaArquivos();

            // Reservar recursos para o próprio SO
            RamFisica.Alocar(1024);          // 1 GB de RAM para o SO
            DiscoFisico.CriarArquivo("SO.sistema", 5); // 5 GB no disco para o SO
        }


        public void InstalarPrograma(string nome, int tamanhoGb)
        {
            if (!DiscoFisico.CriarArquivo(nome + ".exe", tamanhoGb))
            {
                Console.WriteLine("[SO] Disco cheio, não foi possível instalar.");
                return;
            }

            int memoriaMb = tamanhoGb * 512;

            var programa = new Process
            {
                Nome = nome,
                MemoriaNecessaria = memoriaMb,
                TamanhoGbNoDisco = tamanhoGb,
                CarregadoNaRam = false,
                Pcb = null // ainda não tem processo criado
            };

            // páginas de exemplo pré-carregadas
            programa.PaginasParaAcessar.Enqueue(0);
            programa.PaginasParaAcessar.Enqueue(1);
            programa.PaginasParaAcessar.Enqueue(0);

            // lista de programas instalados: usa Kernel.Processos só como catálogo
            Kernel.Processos.Add(programa);

            Console.WriteLine($"[SO] Programa {nome} instalado (PID={Kernel.Processos.Count}, Tamanho={tamanhoGb}GB, MemNec={memoriaMb}MB).");
        }



        public void ListarProcessos()
        {
            Console.WriteLine("[SO] Programas/Processos:");
            if (Kernel.Processos.Count == 0)
            {
                Console.WriteLine(" (nenhum programa instalado)");
                return;
            }

            for (int i = 0; i < Kernel.Processos.Count; i++)
            {
                var p = Kernel.Processos[i];

                int pidMostrar;
                string estado;

                if (p.Pcb == null)
                {
                    pidMostrar = i + 1; // índice como ID enquanto não tiver PCB
                    estado = "Instalado (não carregado)";
                }
                else
                {
                    pidMostrar = p.Pcb.Pid;
                    estado = p.Pcb.Estado.ToString();
                }

                Console.WriteLine($" - PID={pidMostrar}, Nome={p.Nome}, MemNec={p.MemoriaNecessaria}MB, Estado={estado}");
            }
        }



        public void ExecutarTick()
        {
            Kernel.Tick();
        }

        public void Status()
        {
            Console.WriteLine("\n=== STATUS DO SISTEMA ===");
            RamFisica.Status();
            DiscoFisico.ListarArquivos();
            ListarProcessos();
            Kernel.MostrarEstadoMemoria();
            Console.WriteLine("=========================\n");
        }

        public void EncerrarProcesso(int pid)
        {
            int index = pid - 1;
            if (index < 0 || index >= Kernel.Processos.Count)
            {
                Console.WriteLine("[SO] Processo não encontrado.");
                return;
            }

            var processo = Kernel.Processos[index];
            if (processo.Pcb == null)
            {
                Console.WriteLine("[SO] Processo ainda não foi carregado na RAM.");
                return;
            }

            processo.Pcb.Estado = EstadoProcesso.Finalizado;
            processo.CarregadoNaRam = false;
            RamFisica.Liberar(processo.MemoriaNecessaria);

            Console.WriteLine($"[SO] Processo PID={pid} encerrado. RAM liberada (arquivo permanece no disco).");
        }


        public void CarregarProcessoNaRam(int pid)
        {
            int index = pid - 1;

            if (index < 0 || index >= Kernel.Processos.Count)
            {
                Console.WriteLine("[SO] Programa não encontrado.");
                return;
            }

            var processo = Kernel.Processos[index];

            if (processo.CarregadoNaRam)
            {
                Console.WriteLine("[SO] Programa já está carregado na RAM.");
                return;
            }

            if (!RamFisica.Alocar(processo.MemoriaNecessaria))
            {
                Console.WriteLine($"[SO] RAM insuficiente para executar o programa PID={pid} (Necessário {processo.MemoriaNecessaria}MB).");
                return;
            }

            if (processo.Pcb == null)
            {
                processo.Pcb = new Pcb
                {
                    Pid = pid,
                    Estado = EstadoProcesso.Pronto,
                    Prioridade = 1
                };
            }
            else
            {
                processo.Pcb.Estado = EstadoProcesso.Pronto;
            }

            processo.CarregadoNaRam = true;
            Kernel.AdicionarProcesso(processo);

            Console.WriteLine($"[SO] Programa PID={pid} carregado na RAM e colocado na fila de prontos.");
        }



        public void DesinstalarPrograma(int pid)
        {
            int index = pid - 1;
            if (index < 0 || index >= Kernel.Processos.Count)
            {
                Console.WriteLine("[SO] Programa não encontrado para desinstalar.");
                return;
            }

            var programa = Kernel.Processos[index];

            if (programa.CarregadoNaRam)
            {
                Console.WriteLine("[SO] Não é possível desinstalar enquanto o programa está em execução. Encerre o processo primeiro.");
                return;
            }

            DiscoFisico.RemoverArquivo(programa.Nome + ".exe", programa.TamanhoGbNoDisco);
            Kernel.Processos.RemoveAt(index);

            Console.WriteLine($"[SO] Programa {programa.Nome} (PID={pid}) desinstalado e removido da lista.");
        }


    }
}
