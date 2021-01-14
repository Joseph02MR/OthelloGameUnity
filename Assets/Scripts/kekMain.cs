using EmptyProject1;
using UnityEngine;
using System.Threading;

public class kekMain : MonoBehaviour
{
    public GameObject[] Waypoints = new GameObject[64];
    public GameObject[] Tokens = new GameObject[64];
    public static Othello Juego;
    public static bool turno;
    public static bool final;
    public static bool flag;
    // Start is called before the first frame update
    void Start()
    {
        final = false;
        Juego = new Othello();
        
        Juego.CrearJuego(MenuBehaviour.Modo);
        Juego.NuevoJuego(MenuBehaviour.ColorJugador);

        if (MenuBehaviour.ColorJugador) turno = true;
        else turno = false;
            
        EnlazarTablero();
        ActualizarTablero();
    }
    // Update is called once per frame
    private void Update()
    {
        Juego.GetScore();
        if (flag || (Juego.Jugador1.pass && Juego.Jugador2.pass))
        {
            if (Waypoint.aux != null)
            {
                CrearFichaAux(Waypoint.aux);
                Waypoint.aux = null;
            }
            else
            {
                final = Juego.EvaluarFinal();
                flag = false;
            }
        }

        if (Juego.Jugador2.GetType() == typeof(PlayerIA))
        {
            if (!turno)
            {
                if(MenuBehaviour.Dificultad)
                {
                    Thread.Sleep(250);
                    turno = PlayerIA.RealizarMovRandom(Juego.Jugador2, kekMain.Juego, turno);
                }
                else
                    turno = PlayerIA.RealizarMovMinimax(Juego, turno, Juego.Tablero, 4, true);

                ActualizarTablero();
            }
        }
    }

    void EnlazarTablero()
    {
        int k = 0;
        for (int i = 0; i < Othello.Dim; i++)
        {
            for (int j = 0; j < Othello.Dim; j++)
            {
                Juego.ArregloEnlace[k] = new TuplaUnity(new Tupla(i, j), 
                    Waypoints[k], Tokens[k]);
                k++;
            }
        }
    }
    void CrearFicha(GameObject casilla, GameObject token, int color)
    {
        if (color == 1) { token.GetComponent<SpriteRenderer>().color = Color.black ; }
        else if (color == 2) { token.GetComponent<SpriteRenderer>().color = Color.white ; }
        
        if (!token.GetComponent<SpriteRenderer>().isVisible)
            token.GetComponent<SpriteRenderer>().enabled = true;
        
        casilla.GetComponent<BoxCollider2D>().enabled = false;
    }
    
    void CrearFichaAux(Tupla coordenadas)  
    {
        if (turno)
            Juego.Tablero = Othello.ColocarFicha(Juego.Tablero, coordenadas.a, coordenadas.b, Juego.Jugador1);
        else
            Juego.Tablero = Othello.ColocarFicha(Juego.Tablero, coordenadas.a, coordenadas.b, Juego.Jugador2);
        ActualizarTablero();
    }
    void ActualizarTablero() 
    {
        foreach (var x in Juego.ArregloEnlace)
        {
            if (Juego.Tablero[x.casillaOthello.a,x.casillaOthello.b] == 1)
            {
                CrearFicha(x.waypoint, x.token, 1);
            }
            else if (Juego.Tablero[x.casillaOthello.a,x.casillaOthello.b] == 2)
            {
                CrearFicha(x.waypoint, x.token, 2);
            }
        }
    }

}
