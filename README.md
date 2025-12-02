# SO
Projeto da aula de Sistemas Operacionais

# Alunos Integrantes
 * Bruno Barbosa, RA: 112944;
 * Luiz Augusto, 113363.

OBS.: um aluno integrante do grupo pediu transferência de instituição de ensino, portanto, restam somente dois alunos no grupo.

# Descrição do Projeto

# Simulador Didático de Sistema Operacional em C# (console)

Este projeto tem como objetivo implementar um simulador didático de sistema operacional utilizando os princípios de programação Orientada a Objetos. O simulador busca representar, de forma simplificada, componentes essenciais de um sistema operacional real, como processos, gerenciamento de memória, entrada e saída, e sistema de arquivos.

## Objetivo

Desenvolver um simulador que modele:
- Criação e gerenciamento de processos e threads.
- Gerenciamento de memória (paginação ou segmentação).
- Escalonamento de CPU com múltiplos algoritmos.
- Entrada e saída com dispositivos simulados.
- Sistema de arquivos com estrutura hierárquica.
- Coleta de métricas e geração de logs.

## Progresso Atual

**As funcionalidades implementadas incluem:**

- ✅ Programas instalados em disco com limite de capacidade (GB) e reserva de espaço para o próprio SO.
- ✅ Execução sob demanda: “Executar programa” carrega o executável na RAM, consumindo MemNec em MB e criando o processo com PCB (PID, estado, prioridade, contador lógico)
- ✅ Escalonador FCFS com fila de prontos: cada Tick escolhe o próximo processo pronto e simula execução, E/S e mudança de estado (Pronto, Executando, Bloqueado, Finalizado).
- ✅ Memória paginada: páginas lógicas por processo, molduras físicas, first fit e contagem de faltas de página, com mapa de molduras mostrado no Status.
- ✅ Entrada e saída: dispositivo de bloco (disco) com fila de pedidos, tempo de serviço em ticks e retorno de processos bloqueados ao final da E/S.
- ✅ Sistema de arquivos lógico: DiscoSim com criação/remoção de arquivos .exe, reserva de espaço para o SO e limite de uso.
- ✅ Interface de linha de comando: menu para instalar programa, executar programa (carregar na RAM), executar 1 ou 5 ticks, exibir status, encerrar processo e desinstalar programa.

**Arquitetura (Pastas e Classes Principais)**

- ✅ Core: KernelSim (motor de simulação/clock) e SistemaOperacionalSim (fachada de alto nível exposta ao Program.cs).
- ✅ Model: Process, Pcb, ThreadSim, Tcb, enums de estado.
- ✅ Scheduler: IEscalonador e EscalonadorFcfs.
- ✅ Memory: RAMSim (RAM física), GerenciadorMemoria (paginação), Pagina, Moldura, TabelaPaginas.
- ✅ IO: DispositivoBloco, PedidoEs.
- ✅ FS: DiscoSim, SistemaArquivos, Diretorio, ArquivoSim.
- ✅ Utils: Logger (planejado para centralizar logs).

### Funcionalidades ainda não implementadas - Limitações e trabalho futuro:
- ⏳ Threads estão modeladas (TCB/ThreadSim), mas o fluxo atual executa uma thread por processo.
- ⏳ Apenas o algoritmo FCFS está ativo; RR e prioridades estão planejados na pasta Scheduler.
- ⏳ Métricas avançadas (tempo de retorno, espera, throughput) ainda não são calculadas numericamente; o foco foi em clock e faltas de página.


## Como Executar

> Faça o download dos arquivos e junte todos em uma única pasta.
> Utilizando o programa Visual Studio 2022, com as configurações da linguagem C# em aplicativo de console devidamente instaladas e configuradas, abra todos os arquivos.
> Em seguida, aperte a tecla F5 do teclado, pois ela irá executar o código dentro do CMD.

**Menu**

1 – Instalar programa: informa nome e tamanho em GB; consome disco e registra MemNec calculada.​
2 - Listar Processos: mostra todos os programas instalados, indicando se estão apenas instalados ou carregados na RAM e seu estado atual.
3 – Executar programa: informa PID da lista, carrega na RAM se houver espaço e coloca na fila de prontos.​
4/5 – Executar 1 ou 5 ticks: avança o clock, simulando CPU, memória e E/S.​
6 – Status: mostra RAM usada, disco, lista de programas/processos e mapa de molduras.​
7 – Encerrar processo: encerra o processo em RAM, liberando memória (arquivo continua instalado).​
8 – Desinstalar programa: remove o .exe do disco e o item da lista, se não estiver em execução.
9 - Sair: Encerra a execução do projeto.
