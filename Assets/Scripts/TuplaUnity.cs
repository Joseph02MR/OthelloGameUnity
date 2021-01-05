using UnityEngine;

namespace EmptyProject1
{
    public class TuplaUnity 
    {
        public Tupla casillaOthello;
        public GameObject waypoint;
        public GameObject token;
    
        public TuplaUnity(Tupla a, GameObject casilla, GameObject ficha)
        {
            this.casillaOthello = a;
            this.waypoint = casilla;
            this.token = ficha;
        }
    }
}

