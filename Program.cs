using System;
using System.Collections.Generic;

namespace JogoDaVelha
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IniciarJogoDaVelha();
            string desafiante = VerificaDesafiante();
            string nivel = "";

            string jogador1 = "Jogador 1";
            string jogador2 = "Máquina";

            char[,] tabuleiro = InicializaTabuleiro();
            char jogador = 'X';

            if (desafiante == "maquina")
            {
                nivel = VerificaNivel();
                if (nivel == "basico")
                {
                    CompetidorBasico(tabuleiro, jogador1, jogador2);
                }
                else
                {
                    CompetidorExpert(tabuleiro, jogador1, jogador2);
                }
            }
            else
            {
                Console.WriteLine("Entre com o nome do player 1:");
                jogador1 = Console.ReadLine();
                Console.WriteLine("Entre com o nome do player 2:");
                jogador2 = Console.ReadLine();

                Console.WriteLine("Estamos sorteando o primeiro player que deverá começar a jogar... Pressione qualquer tecla");
                Console.ReadLine();
                Console.Clear();

                int valor = GerarValor();
                MostraCabecalhoAtual(valor);

                string vencedor = null;

                do
                {
                    ImprimeTabuleiro(tabuleiro);

                    Console.WriteLine($"Vez de {jogador}");
                    Console.WriteLine("Escolha uma posição para jogar, no formato '1,2', caso queira representar a primeira linha, segunda coluna!");
                    string posicao = Console.ReadLine();
                    if (VerificaPosicao(posicao, tabuleiro, jogador))
                    {
                        vencedor = VerificaVencedor(tabuleiro, jogador, jogador1, jogador2);
                        if (vencedor != null)
                        {
                            Console.WriteLine($"O jogador {vencedor} venceu!");
                            break;
                        }
                        jogador = (jogador == 'X') ? 'O' : 'X';
                    }
                } while (vencedor == null);
            }
        }

        public static char[,] InicializaTabuleiro()
        {
            char[,] tabuleiro = new char[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    tabuleiro[i, j] = ' ';
                }
            }
            return tabuleiro;
        }

        public static bool VerificaPosicao(string posicao, char[,] tabuleiro, char jogador)
        {
            string[] partes = posicao.Split(',');
            int linha, coluna;
            if (partes.Length == 2)
            {
                if (int.TryParse(partes[0], out linha) && int.TryParse(partes[1], out coluna))
                {
                    if (linha >= 0 && linha < 3 && coluna >= 0 && coluna < 3)
                    {
                        if (tabuleiro[linha, coluna] == ' ')
                        {
                            tabuleiro[linha, coluna] = jogador;
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Essa posição já está ocupada. Escolha outra.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Posição inválida. Fora do tabuleiro.");
                    }
                }
                else
                {
                    Console.WriteLine("Formato inválido. As partes não são números inteiros.");
                }
            }
            else
            {
                Console.WriteLine("Formato inválido. Deve conter exatamente uma vírgula.");
            }
            return false;
        }

        public static void ImprimeTabuleiro(char[,] tabuleiro)
        {
            Console.Clear();
            Console.WriteLine("Tabuleiro:");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(" " + tabuleiro[i, j]);
                    if (j < 2)
                    {
                        Console.Write(" |");
                    }
                }
                Console.WriteLine();
                if (i < 2)
                {
                    Console.WriteLine("-----------");
                }
            }
        }

        public static string VerificaVencedor(char[,] tabuleiro, char jogador, string jogador1, string jogador2)
        {
            for (int i = 0; i < 3; i++)
            {
                if (tabuleiro[i, 0] == jogador && tabuleiro[i, 1] == jogador && tabuleiro[i, 2] == jogador)
                {
                    return (jogador == 'X') ? jogador1 : jogador2;
                }
                if (tabuleiro[0, i] == jogador && tabuleiro[1, i] == jogador && tabuleiro[2, i] == jogador)
                {
                    return (jogador == 'X') ? jogador1 : jogador2;
                }
            }
            if (tabuleiro[0, 0] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 2] == jogador)
            {
                return (jogador == 'X') ? jogador1 : jogador2;
            }
            if (tabuleiro[0, 2] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 0] == jogador)
            {
                return (jogador == 'X') ? jogador1 : jogador2;
            }
            return null;
        }

        public static string VerificaVencedor(char[,] tabuleiro, char jogador)
        {
            for (int i = 0; i < 3; i++)
            {
                if (tabuleiro[i, 0] == jogador && tabuleiro[i, 1] == jogador && tabuleiro[i, 2] == jogador)
                {
                    return "Jogador";
                }
                if (tabuleiro[0, i] == jogador && tabuleiro[1, i] == jogador && tabuleiro[2, i] == jogador)
                {
                    return "Jogador";
                }
            }

            if (tabuleiro[0, 0] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 2] == jogador)
            {
                return "Jogador";
            }
            if (tabuleiro[0, 2] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 0] == jogador)
            {
                return "Jogador";
            }

            return null;
        }

        public static bool TabuleiroCheio(char[,] tabuleiro)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tabuleiro[i, j] == ' ')
                    {
                        return false;
                    }
                }
            }
            Console.WriteLine("O jogo empatou!");
            return true;
        }

        public static void MostraCabecalhoAtual(int valor)
        {
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("-----------------                                           -----------------");
            Console.WriteLine("-----------------   O Player " + valor + " começa jogando   -----------------");
            Console.WriteLine("-----------------                                           -----------------");
            Console.WriteLine("---------------------------------------------------------------------");
        }

        public static void CompetidorBasico(char[,] tabuleiro, string jogador1, string jogador2)
        {
            char jogadorHumano = 'X';
            char jogadorMaquina = 'O';

            bool jogadorHumanoVenceu = false;
            bool jogadorMaquinaVenceu = false;

            int jogadas = 0;

            ImprimeTabuleiro(tabuleiro);

            while (true)
            {
                if (jogadas % 2 == 0)
                {
                    Console.WriteLine($"Vez de {jogador1} ({jogadorHumano})");
                    Console.WriteLine("Escolha uma posição para jogar, no formato '1,2', caso queira representar a primeira linha, segunda coluna!");

                    while (true)
                    {
                        string posicao = Console.ReadLine();
                        if (VerificaPosicao(posicao, tabuleiro, jogadorHumano))
                        {
                            ImprimeTabuleiro(tabuleiro);
                            jogadorHumanoVenceu = VerificaVencedor(tabuleiro, jogadorHumano) != null;
                            break; // Sai do loop de entrada quando uma jogada válida é feita.
                        }
                        else
                        {
                            // Jogada inválida, continue no mesmo turno.
                            Console.WriteLine("Jogada inválida. Tente novamente.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Vez de {jogador2} ({jogadorMaquina})");
                    System.Threading.Thread.Sleep(1000);

                    List<Tuple<int, int>> espacosLivres = new List<Tuple<int, int>>();
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (tabuleiro[i, j] == ' ')
                            {
                                espacosLivres.Add(Tuple.Create(i, j));
                            }
                        }
                    }

                    if (espacosLivres.Count > 0)
                    {
                        Random random = new Random();
                        int indice = random.Next(espacosLivres.Count);
                        Tuple<int, int> escolha = espacosLivres[indice];
                        int linha = escolha.Item1;
                        int coluna = escolha.Item2;
                        tabuleiro[linha, coluna] = jogadorMaquina;
                    }

                    ImprimeTabuleiro(tabuleiro);
                    jogadorMaquinaVenceu = VerificaVencedor(tabuleiro, jogadorMaquina) != null;
                }

                if (jogadorHumanoVenceu)
                {
                    Console.WriteLine($"O jogador {jogador1} ({jogadorHumano}) venceu!");
                    break;
                }
                else if (jogadorMaquinaVenceu)
                {
                    Console.WriteLine($"O jogador {jogador2} ({jogadorMaquina}) venceu!");
                    break;
                }

                jogadas++;

                if (jogadas == 9)
                {
                    Console.WriteLine("O jogo empatou!");
                    break;
                }
            }
        }

        public static void CompetidorExpert(char[,] tabuleiro, string jogador1, string jogador2)
        {
            char jogadorHumano = 'X';
            char jogadorMaquina = 'O';

            int jogadas = 0;
            List<Tuple<int, int>> jogadasMaquina = new List<Tuple<int, int>>();

            ImprimeTabuleiro(tabuleiro);

            while (true)
            {
                if (jogadas % 2 == 0)
                {
                    Console.WriteLine($"Vez de {jogador1} ({jogadorHumano})");
                    Console.WriteLine("Escolha uma posição para jogar, no formato '1,2', caso queira representar a primeira linha, segunda coluna!");

                    while (true)
                    {
                        string posicao = Console.ReadLine();
                        if (VerificaPosicao(posicao, tabuleiro, jogadorHumano))
                        {
                            ImprimeTabuleiro(tabuleiro);
                            if (VerificaVencedor(tabuleiro, jogadorHumano) != null)
                            {
                                Console.WriteLine($"O jogador {jogador1} ({jogadorHumano}) venceu!");
                                return;
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Jogada inválida. Tente novamente.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Vez da máquina ({jogador2} ({jogadorMaquina}))");
                    System.Threading.Thread.Sleep(1000);

                    Tuple<int, int> jogadaMaquina = FazerJogadaMaquina(tabuleiro, jogadorMaquina, jogadorHumano, jogadasMaquina);

                    jogadasMaquina.Add(jogadaMaquina);

                    ImprimeTabuleiro(tabuleiro);
                    if (VerificaVencedor(tabuleiro, jogadorMaquina) != null)
                    {
                        Console.WriteLine($"A máquina {jogador2} ({jogadorMaquina}) venceu!");
                        return;
                    }
                }

                jogadas++;

                if (jogadas == 9)
                {
                    Console.WriteLine("O jogo empatou!");
                    return;
                }
            }
        }

        public static Tuple<int, int> FazerJogadaMaquina(char[,] tabuleiro, char jogadorMaquina, char jogadorHumano, List<Tuple<int, int>> jogadasMaquina)
        {
            // Verifique as condições para ganhar ou bloquear uma jogada do jogador
            foreach (var possibilidade in PossibilidadesVitoria)
            {
                Tuple<int, int> jogadaVitoria = VerificaPossibilidadeVitoria(tabuleiro, jogadorMaquina, possibilidade);
                if (jogadaVitoria != null)
                {
                    return jogadaVitoria;
                }

                Tuple<int, int> jogadaBloqueio = VerificaPossibilidadeVitoria(tabuleiro, jogadorHumano, possibilidade);
                if (jogadaBloqueio != null)
                {
                    return jogadaBloqueio;
                }
            }

            // Se nenhuma jogada de vitória ou bloqueio estiver disponível, faça uma jogada inteligente
            Tuple<int, int> jogadaInteligente = EncontrarJogadaInteligente(tabuleiro, jogadorMaquina, jogadorHumano, jogadasMaquina);

            if (jogadaInteligente != null)
            {
                return jogadaInteligente;
            }

            // Se não for possível realizar nenhuma jogada inteligente, faça uma jogada aleatória
            Tuple<int, int> jogadaAleatoria = FazerJogadaAleatoria(tabuleiro);

            return jogadaAleatoria;
        }

        public static Tuple<int, int> FazerJogadaAleatoria(char[,] tabuleiro)
        {
            Random random = new Random();
            List<Tuple<int, int>> espacosLivres = new List<Tuple<int, int>>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tabuleiro[i, j] == ' ')
                    {
                        espacosLivres.Add(Tuple.Create(i, j));
                    }
                }
            }

            if (espacosLivres.Count > 0)
            {
                int indice = random.Next(espacosLivres.Count);
                return espacosLivres[indice];
            }

            return null;
        }

        public static void FazerJogadaAleatoria(char[,] tabuleiro, char jogadorMaquina)
        {
            Random random = new Random();
            List<Tuple<int, int>> espacosLivres = new List<Tuple<int, int>>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tabuleiro[i, j] == ' ')
                    {
                        espacosLivres.Add(Tuple.Create(i, j));
                    }
                }
            }

            if (espacosLivres.Count > 0)
            {
                int indice = random.Next(espacosLivres.Count);
                Tuple<int, int> escolha = espacosLivres[indice];
                int linha = escolha.Item1;
                int coluna = escolha.Item2;
                tabuleiro[linha, coluna] = jogadorMaquina;
            }
        }

        public static bool FazerJogadaVencedora(char[,] tabuleiro, char jogador)
        {
            for (int linha = 0; linha < 3; linha++)
            {
                for (int coluna = 0; coluna < 3; coluna++)
                {
                    if (tabuleiro[linha, coluna] == ' ')
                    {
                        // Simula a jogada do jogador na posição atual
                        tabuleiro[linha, coluna] = jogador;

                        // Se o jogador ganhar na próxima jogada, faz a jogada da máquina e vence o jogo
                        if (VerificaVencedor(tabuleiro, jogador) != null)
                        {
                            tabuleiro[linha, coluna] = jogador;
                            return true;
                        }

                        // Desfaz a jogada para continuar as verificações
                        tabuleiro[linha, coluna] = ' ';
                    }
                }
            }
            return false;
        }
        public static bool FazerJogadaInteligente(char[,] tabuleiro, char jogador, char jogadorAdversario)
        {
            int[] jogadaVencedora = EncontrarJogadaInteligente(tabuleiro, jogador, jogador);

            if (jogadaVencedora != null)
            {
                tabuleiro[jogadaVencedora[0], jogadaVencedora[1]] = jogador;
                return true;
            }

            int[] jogadaBloqueio = EncontrarJogadaInteligente(tabuleiro, jogadorAdversario, jogador);

            if (jogadaBloqueio != null)
            {
                tabuleiro[jogadaBloqueio[0], jogadaBloqueio[1]] = jogador;
                return true;
            }

            int[] jogadaInteligente = EncontrarJogadaInteligente(tabuleiro, jogador, jogadorAdversario);

            if (jogadaInteligente != null)
            {
                tabuleiro[jogadaInteligente[0], jogadaInteligente[1]] = jogador;
                return true;
            }

            return false;
        }

        public static int[] EncontrarJogadaInteligente(char[,] tabuleiro, char jogador, char jogadorAdversario)
        {
            for (int linha = 0; linha < 3; linha++)
            {
                for (int coluna = 0; coluna < 3; coluna++)
                {
                    if (tabuleiro[linha, coluna] == ' ')
                    {
                        tabuleiro[linha, coluna] = jogador;

                        if (VerificaVencedor(tabuleiro, jogador) != null)
                        {
                            tabuleiro[linha, coluna] = jogador;
                            return new int[] { linha, coluna };
                        }

                        tabuleiro[linha, coluna] = ' ';
                    }
                }
            }

            for (int linha = 0; linha < 3; linha++)
            {
                for (int coluna = 0; coluna < 3; coluna++)
                {
                    if (tabuleiro[linha, coluna] == ' ')
                    {
                        tabuleiro[linha, coluna] = jogadorAdversario;

                        if (VerificaVencedor(tabuleiro, jogadorAdversario) != null)
                        {
                            tabuleiro[linha, coluna] = jogador;
                            return new int[] { linha, coluna };
                        }
                        tabuleiro[linha, coluna] = ' ';
                    }
                }
            }

            return null;
        }

        public static int GerarValor()
        {
            Random random = new Random();
            int resultado = random.Next(1, 3);
            return resultado;
        }

        public static string VerificaNivel()
        {
            string resposta;
            bool start = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Qual o nível de desafio que você quer?");
                Console.WriteLine("1 - Básico");
                Console.WriteLine("2 - Expert");
                resposta = Console.ReadLine();
                if (resposta == "1" || resposta == "2")
                {
                    start = true;
                }
            } while (!start);
            return resposta == "1" ? "basico" : "expert";
        }

        public static string VerificaDesafiante()
        {
            bool start = false;
            string resposta = "";
            do
            {
                Console.Clear();
                Console.WriteLine("Você quer jogar com outro player ou contra a máquina?");
                Console.WriteLine("1 - Player vs Player");
                Console.WriteLine("2 - Player vs Máquina");
                resposta = Console.ReadLine();
                if (resposta == "1" || resposta == "2")
                {
                    start = true;
                }
            } while (!start);
            return resposta == "1" ? "player" : "maquina";
        }

        public static void IniciarJogoDaVelha()
        {
            string resposta;
            bool start = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Pronto para jogar o jogo da velha? s/n");
                resposta = Console.ReadLine();
                if (resposta.ToLower() == "s" || resposta.ToLower() == "sim")
                {
                    start = true;
                }
            } while (!start);
        }
    }
}
