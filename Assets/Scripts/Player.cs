
namespace EmptyProject1
{
    public class Player
    {
        public byte color;
        public int score;
        public bool pass = false;

        public int DefScore(int[,] Tablero)
        {
            int aux = 0;
            for (int i = 0; i < Othello.Dim; i++)
            {
                for (int j = 0; j < Othello.Dim; j++)
                {
                    if (Tablero[i, j] == color) aux++;
                }
            }
            score = aux;
            return score;
        }

        //get_posible_Moves
        //EncontrarLineas
    }
        
}