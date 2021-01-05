using System;
using System.Diagnostics;
using UnityEngine;

    public class TextManager : MonoBehaviour
    {
        bool turno;

        void Update()
        {
            turno = kekMain.turno;
       
            if (gameObject.name == "Score_J1")
            {
                GetComponent<TextMesh>().text = kekMain.Juego.Score.a.ToString();
            }
            else if (gameObject.name == "Score_J2")
            {
                GetComponent<TextMesh>().text = kekMain.Juego.Score.b.ToString();
            }

            if(kekMain.final)
            {
                if(gameObject.name == "Texto_Winner")
                    GetComponent<TextMesh>().text = "Ganador: " + kekMain.Juego.GetWinner();    
            }

            if(gameObject.name == "Turno_J1")
                GetComponent<TextMesh>().text = turno ? ">>" : "";   

            if(gameObject.name == "Turno_J2")
                GetComponent<TextMesh>().text = !turno ? ">>" : "";   



             
        }
    }

