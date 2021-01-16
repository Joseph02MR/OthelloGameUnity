using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    public static bool Modo;
    public static bool ColorJugador;
    public static bool Dificultad = true;
    
    public void SetModePVP()
    {
        Modo = true;
    }
    public void SetModePVIA()
    {
        Modo = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("OthelloGame");
    }

    public void SetColorBlack()
    {
        ColorJugador = true;
    }
    public void SetColorWhite()
    {
        ColorJugador = false;
    }

    public void SetDifEasy()
    {
        Dificultad = true;
    }

    public void SetDifNormal()
    {
        Dificultad = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
