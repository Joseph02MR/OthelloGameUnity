using UnityEngine;
using UnityEngine.SceneManagement;

namespace EmptyProject1
{
    public class ButtonBehaviour : MonoBehaviour
    {
        public void ResetButton()
        {
            SceneManager.LoadScene("OthelloGame");
        }

        public void PassButton()
        {
            Player player = kekMain.turno ? kekMain.Juego.Jugador1 : kekMain.Juego.Jugador2;
            player.pass = true;
            Debug.Log("Jugador pasa ");
            
            kekMain.turno = kekMain.turno ? false : true;
        }
    }
}
