using UnityEngine;
using EmptyProject1;


public class Waypoint : MonoBehaviour
{
    public static Tupla aux;

    private void OnMouseDown()
    {
        kekMain.flag = true;
        int busqueda = 0;
        while (this.gameObject != kekMain.Juego.ArregloEnlace[busqueda].waypoint)
            busqueda++;
        aux = kekMain.Juego.ArregloEnlace[busqueda].casillaOthello;
    }

    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 1);
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}