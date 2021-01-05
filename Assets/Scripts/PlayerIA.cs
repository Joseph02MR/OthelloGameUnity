using System;
using System.Collections.Generic;
using Random = System.Random;
using System.Diagnostics;

namespace EmptyProject1
{
    public class PlayerIA : Player
    {
        static Stopwatch time = new Stopwatch();
        static DateTime start = new DateTime();

        public static bool RealizarMovRandom(Player player, Othello game, bool turn)
        {
            List<Tupla> movimientos = Othello.Availabilty(game.Tablero, player.color);
            if (movimientos.Count > 0)
            {
                Random objeto = new Random();
                int index = objeto.Next(0, movimientos.Count - 1);
                Tupla casilla = new Tupla(movimientos[index].a, movimientos[index].b);
                game.Tablero = Othello.ColocarFicha(game.Tablero, casilla.a, casilla.b, player);
            }
            else
            {
                player.pass = true;
                UnityEngine.Debug.Log("Jugador pasa IA");
            }

            return turn ? false : true;
        }

        private static int GetHeuristicScore(int[,] Board, byte colorPlayer)
        {
            int score1, score2, EmptyS, score1W, score2W;
            score1 = score2 = EmptyS = score1W = score2W = 0;
            int[,] BoardW =
            {
                {1000, -10, 10, 10, 10, 10, -10, 1000}, {-10, -10, 10, 1, 1, 10, -10, -10},
                {10, 10, 10, 1, 1, 10, 10, 10}, {10, 1, 1, 1, 1, 1, 1, 10}, {10, 1, 1, 1, 1, 1, 1, 10}, 
                {10, 10, 10, 1, 1, 10, 10, 10}, {-10, -10, 10, 1, 1, 10, -10, -10}, {1000, -10, 10, 10, 10, 10, -10, 1000}
            };

            for (int i = 0; i < Othello.Dim; i++)
            {
                for (int j = 0; j < Othello.Dim; j++)
                {
                    if (Board[i, j] == 0)
                        EmptyS++;
                    else if (Board[i, j] == colorPlayer)
                    {
                        score1++;
                        score1W += BoardW[i, j];
                    }
                    else
                    {
                        score2++;
                        score2W += BoardW[i, j];
                    }
                }
            }
            if (EmptyS > 0)
                return score2W - score1W;
            else
                return score2 - score1;
        }

        private static int Minimax(Othello game, bool aux, int[,] Tablero, int depth, bool MaxPlayer)
        {
            Player player = aux ? game.Jugador1 : game.Jugador2;
            List<Tupla> movimientos = Othello.Availabilty(Tablero, player.color);

            if (depth == 0 || movimientos.Count == 0)
                return GetHeuristicScore(Tablero, player.color);

            int[,] neuTablero;
            int value;
            if (MaxPlayer)
            {
                value = -1000000;
                foreach (var x in movimientos)
                {
                    neuTablero = Othello.ColocarFicha(Tablero, x.a, x.b, player);
                    value = Math.Max(value, Minimax(game, !aux, neuTablero, depth-1, false));
                }
                return value;
            }
            else
            {
                value = 1000000;
                foreach (var x in movimientos)
                {
                    neuTablero = Othello.ColocarFicha(Tablero, x.a, x.b, player);
                    value = Math.Min(value, Minimax(game, !aux, neuTablero, depth-1, true));
                }
                return value;
            }
        }

        private static int AlphaBeta(Othello game, bool aux, int[,] Tablero, int depth, bool MaxPlayer,
            int alpha, int beta, int end)
        {
            Player player = aux ? game.Jugador1 : game.Jugador2;
            List<Tupla> movimientos = Othello.Availabilty(Tablero, player.color);

            if (depth == 0 || movimientos.Count == 0 ||time.ElapsedMilliseconds >= end)
                return GetHeuristicScore(Tablero, player.color);

            int[,] neuTablero;
            int value;
            if (MaxPlayer)
            {
                value = -1000000;
                foreach (var x in movimientos)
                {
                    neuTablero = Othello.ColocarFicha(Tablero, x.a, x.b, player);
                    value = Math.Max(value,
                        AlphaBeta(game, !aux, neuTablero, depth - 1, false, alpha, beta, end));
                    alpha = Math.Max(alpha, value);
                    if (alpha >= beta)
                        break;
                }
                return value;
            }
            else
            {
                value = 1000000;
                foreach (var x in movimientos)
                {
                    neuTablero = Othello.ColocarFicha(Tablero, x.a, x.b, player);
                    value = Math.Min(value,
                        AlphaBeta(game, !aux, neuTablero, depth - 1, true, alpha, beta, end));
                    beta = Math.Min(beta, value);
                    if (beta <= alpha)
                        break;
                }
                return value;
            }
        }

        public static bool RealizarMovMinimax(Othello game, bool turn, int[,] Tablero, int depth, bool poda)
        {
            Player player = turn ? game.Jugador1 : game.Jugador2;
            List<Tupla> movimientos = Othello.Availabilty(Tablero, player.color);
            if (movimientos.Count > 0)
            {
                int bestvalue = -10000000;
                int[,] neuTablero;
                int valueAux;
                Tupla bestMove = null;
                int endTimer = start.Millisecond + 4500;
                time.Start();
                foreach (var x in movimientos)
                {
                    neuTablero = Othello.ColocarFicha(Tablero, x.a, x.b, player);
                    if (!poda)
                        valueAux = Minimax(game, !turn, neuTablero, depth, true);
                    else
                        valueAux = AlphaBeta(game, !turn, neuTablero, depth, true, -1000000, 1000000, endTimer);

                    if (valueAux > bestvalue)
                    {
                        bestvalue = valueAux;
                        bestMove = x;
                    }
                }
                time.Reset();
                game.Tablero = Othello.ColocarFicha(game.Tablero, bestMove.a, bestMove.b, game.Jugador2);
            }
            else
            {
                player.pass = true;
                UnityEngine.Debug.Log("Jugador pasa IA");
            }
            return turn ? false : true;
        }
    }
}
