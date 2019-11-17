using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration
{
    public class DesignQuestions
    {
        public static void ExecuteTicTacToe()
        {
            int val= 0;
            var tictactoe = new TicTacToe(3);
            val = tictactoe.Move(0, 0, 1); //1 - first player
            val = tictactoe.Move(0, 2, 2); //2 - second player
            val = tictactoe.Move(2, 2, 1); //1-  first player
            val = tictactoe.Move(1, 1, 2); //2-  second player
            val = tictactoe.Move(2, 0, 1); //1-  first player
            val = tictactoe.Move(1, 0, 2); //2 - second player
            val = tictactoe.Move(2, 1, 1); //1 - first player
            if (val == 1)
                Console.WriteLine("First Player Win");
            else
                if (val != 0)
                Console.WriteLine("Second Player Win");
            else
                Console.WriteLine("draw");
        }
        class TicTacToe
        {
            int[,] tic;
            int dc1, dc2, n;
            public TicTacToe(int n)
            {
                this.n = n;
                tic = new int[3, 3];
            }

            public int Move(int row, int col, int teamPlayer)
            {
                int val = teamPlayer == 1 ? 1 : -1;
                tic[row, col] += val;

                if (WinConditionForRow(teamPlayer, row) || WinConditionForColumn(teamPlayer, col) ||
                   WinConditionForFirstDiagonal(teamPlayer) || WinConditionForSecondDiagonal(teamPlayer))
                {
                    return teamPlayer;
                }
                else
                   return 0;
            }

            //Row Check
            public bool WinConditionForRow(int player, int row)
            {
                bool win = true;
                for (int i = 0; i < n; i++)
                {
                    if (tic[row, i] != player)
                    {
                        win = false;
                        break;
                    }                        
                }
                return win;
            }
            //Column check
            public bool WinConditionForColumn(int player, int column)
            {
                bool win = true;
                for (int i = 0; i < n; i++)
                {
                    if (tic[i, column] != player)
                    {
                        win = false;
                        break;
                    }
                }
                return win;
            }

            public bool WinConditionForFirstDiagonal(int player)
            {
                bool win = true;
                for(int i=0; i<n; i++)
                {
                    if(tic[i,i] != player)
                    {
                        win = false; break;
                    }
                }
                return win;
            }

            public bool WinConditionForSecondDiagonal(int player)
            {
                bool win = true;
                for (int i = 0; i < n; i++)
                {
                    if (tic[i, n-i-1] != player)
                    {
                        win = false; break;
                    }
                }
                return win;
            }

        }
    }
}
