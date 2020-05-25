using System;

//https://leetcode.com/problems/design-tic-tac-toe/
public class TicTacToe
{
    int[] rows = new int[3];
    int[] cols = new int[3];
    int[][] board = new int[3][]
    {
        new int[3],
        new int[3],
        new int[3]
    };
    int turn = 0;
    int diag = 0;
    int antiDiag = 0;

    public void MakeMoves()
    {
        Console.WriteLine(Move(0, 0, 1));
        Console.WriteLine(Move(0, 2, 2));
        Console.WriteLine(Move(2, 2, 1));
        Console.WriteLine(Move(1, 1, 2));
        Console.WriteLine(Move(2, 0, 1));
        Console.WriteLine(Move(1, 0, 2));
        Console.WriteLine(Move(2, 1, 1));
    }

    public int Move(int row, int col, int player)
    {
        int toAdd = player == 1 ? 1 : -1;
        
        rows[row] += toAdd;
        cols[col] += toAdd;

        if (row == col)
        {
            diag+= toAdd;
        }

        if (row + col == 2)
        {
            antiDiag+= toAdd;
        }

       if (Math.Abs(rows[row]) == 3 || 
           Math.Abs(cols[col]) == 3 ||
           diag == 3 ||
           antiDiag == 3)
           {
               return player;
           }

        return 0;
    }
}