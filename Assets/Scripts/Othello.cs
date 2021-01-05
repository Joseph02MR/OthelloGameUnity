using System.Collections.Generic;
using UnityEngine; //quitar todos los debugs para quitar el namespace

namespace EmptyProject1
{
    public class Othello
    {
        public TuplaUnity[] ArregloEnlace;
        public int[,] Tablero;
        public Player Jugador1;
        public Player Jugador2;
        public Tupla Score;
        public const int Dim = 8; //dimension del tablero

        public void CrearJuego(bool prueba)
        {
            ArregloEnlace = new TuplaUnity[64];
            Tablero = new int[Dim, Dim];
            Score = new Tupla(0, 0);
            Jugador1 = new Player();

            if (prueba)
                Jugador2 = new Player();
            else
                Jugador2 = new PlayerIA();
        }

        public void GetScore()
        {
            Jugador1.DefScore(Tablero);
            Jugador2.DefScore(Tablero);
            Score.ActualizarTupla(Jugador1.score, Jugador2.score);
        }

        public string GetWinner()
        {
            if (Score.a == Score.b)
                return "Empate";
            else
                return Score.a > Score.b ? "Jugador 1" : "Jugador 2";
        }

        public void NuevoJuego(bool colorJugador)
        {
            //crearTablero()
            Tablero[3, 3] = Tablero[4, 4] = 1;
            Tablero[4, 3] = Tablero[3, 4] = 2;
            
            Jugador1.color = (byte) (colorJugador ? 1 : 2);
            Jugador2.color = Jugador1.color == 1 ? (byte) 2 : (byte) 1;
        }

        public bool EvaluarFinal()
        {
            int aux = 0;
            foreach(var x in Tablero)
                if(x == 0) aux ++; 
            return Availabilty(Tablero, Jugador1.color).Count == 0 && Availabilty(Tablero, Jugador2.color).Count == 0 || aux == 0;
        }


        public static int[,] ColocarFicha(int [,] Tablero, int r, int c, /*byte*/Player player) //renglon, columna Ficha nueva
        {
            Tupla aux = new Tupla(r, c);
            List<Tupla> movimientos = Availabilty(Tablero, player.color);
            if (movimientos.Count == 0)
            {
                Debug.Log("Sin movimientos disponibles");
                return Tablero;
            }
            else
            {
                player.pass = false;
                int[,] neuTablero = new int[Dim, Dim];
                for (int i = 0; i < Dim; i++)
                {
                    for (int j = 0; j < Dim; j++)
                    {
                        neuTablero[i, j] = Tablero[i, j];
                    }
                }

                bool kek = movimientos.Exists(x => (x.a == aux.a && x.b == aux.b));
                if (!kek)
                    Debug.Log("Casilla inválida"); //mensaje de casilla inválida
                else
                {
                    List<List<Tupla>> Lineas = EncontrarLineas(Tablero, r, c, player.color);
                    neuTablero[r, c] = player.color;

                    foreach (var Linea in Lineas)
                    {
                        foreach (var tupla in Linea)
                        {
                            neuTablero[tupla.a, tupla.b] = player.color;
                        }
                    }

                    kekMain.turno = kekMain.turno ? false : true;
                    //turno = turno == 1 ? (byte) 2 : (byte) 1;
                }

                return neuTablero;
            }
        }

        public static List<Tupla> Availabilty(int [,] Tablero, byte player)
        {
            List<Tupla> Resultado = new List<Tupla>();
            List<List<Tupla>> Lineas;

            for (int i = 0; i < Othello.Dim; i++)
            {
                for (int j = 0; j < Othello.Dim; j++)
                {
                    if (Tablero[i, j] == 0)
                    {
                        Lineas = EncontrarLineas(Tablero, i, j, player);
                        if (Lineas.Count != 0)
                            Resultado.Add(new Tupla(i, j));
                    }
                }
            }
            return Resultado;
        }

        public static List<List<Tupla>> EncontrarLineas(int [,] Tablero, int r, int c, byte player)
        {
            List<List<Tupla>> Lineas = new List<List<Tupla>>();

            Tupla[] Coord =
            {
                new Tupla(0, 1), new Tupla(1, 1), new Tupla(1, 0), new Tupla(1, -1), new Tupla(0, -1),
                new Tupla(-1, -1), new Tupla(-1, 0), new Tupla(-1, 1)
            };
            foreach (var C in Coord)
            {
                int u = r;
                int v = c;

                List<Tupla> Linea = new List<Tupla>();
                u += C.a;
                v += C.b;
                bool found = false;

                while (u >= 0 && u < Othello.Dim && v >= 0 && v < Othello.Dim)
                {
                    if (Tablero[u, v] == 0)
                        break;
                    else if (Tablero[u, v] == player)
                    {
                        found = true;
                        break;
                    }
                    else Linea.Add(new Tupla(u, v));

                    u += C.a;
                    v += C.b;
                }
                if (found && Linea.Count != 0)
                    Lineas.Add(Linea);
            }

            return Lineas;
        }
    }
}


