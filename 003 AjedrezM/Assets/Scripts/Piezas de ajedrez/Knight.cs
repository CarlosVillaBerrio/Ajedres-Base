using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Chessman
{
    BoardManager Tablero;

    private void Start()
    {
        Tablero = FindObjectOfType<BoardManager>();        
    }

    public override bool[,] PossibleMove()
   {
        bool[,] r = new bool[Tablero.tamañoTablero, Tablero.tamañoTablero];

        // Up Left
        KnightMove(CurrentX - 1, CurrentY + 2, ref r);

        // Up Right
        KnightMove(CurrentX + 1, CurrentY + 2, ref r);

        // Right Up
        KnightMove(CurrentX + 2, CurrentY + 1, ref r);

        // Right Down
        KnightMove(CurrentX + 2, CurrentY - 1, ref r);

        // Down Left
        KnightMove(CurrentX - 1, CurrentY - 2, ref r);

        // Down Right
        KnightMove(CurrentX + 1, CurrentY - 2, ref r);

        // Left Up
        KnightMove(CurrentX - 2, CurrentY + 1, ref r);

        // Left Down
        KnightMove(CurrentX - 2, CurrentY - 1, ref r);

        return r;
   }

    public void KnightMove(int x, int y, ref bool[,] r)
    {
        Chessman c;
        if(x >= 0 && x < Tablero.tamañoTablero && y >= 0 && y < Tablero.tamañoTablero)
        {
            c = BoardManager.Instance.Chessmans[x, y];
            if (c == null)
                r[x, y] = true;
            else if (isWhite != c.isWhite)
                r[x, y] = true;
        }
    }
}
