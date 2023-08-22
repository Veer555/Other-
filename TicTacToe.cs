using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TicTacToe : MonoBehaviour
{
    public Button[] buttons;
    public Text winText;
    public GameObject gameOverPanel;

    private string playerSide = "X";
    private int[,] board = new int[3, 3];

    void Start ()
    {
        int randomInt = Random.Range(0, 2);

        if ( randomInt == 0 )
        {
            playerSide = "X";
            Debug.Log(playerSide);
        }
        else
        {
            playerSide = "O";
            Debug.Log(playerSide);
            int randomIndex = Random.Range(0, 8);
            ButtonClicked(randomIndex);
            Debug.Log("RandomIndex" + randomIndex);
        }

        // Set up button click events
        for ( int i = 0; i < buttons.Length; i++ )
        {
            int index = i;
            buttons[index].onClick.AddListener(() => ButtonClicked(index));
        }
    }

    public void ButtonClicked ( int index )
    {
        if ( board[index / 3, index % 3] == 0 )
        {
            buttons[index].GetComponentInChildren<Text>().text = playerSide;
            buttons[index].interactable = false;
            Debug.Log("playerSide" + playerSide);

            // Update board array
            board[index / 3, index % 3] = playerSide == "X" ? 1 : -1;

            // Check for a win or tie
            int winner = CheckForWinner();
            if ( winner != 0 )
            {
                EndGame(winner);
                return;
            }
            else if ( IsBoardFull() )
            {
                EndGame(0);
                return;
            }
            
            playerSide = ( playerSide == "X" ) ? "O" : "X";

            if ( playerSide == "O" )
            {
                int bestMoveIndex = Minimax(board, false).index;
                ButtonClicked(bestMoveIndex);
            }            
        }
    }
      
    private void SwitchPlayerTurn ()
    {
        int winner = CheckForWinner();
        if ( winner != 0 )
        {
            EndGame(winner);
            return;
        }
        else if ( IsBoardFull() )
        {
            EndGame(0);
            return;
        }
        else
        {
            playerSide = ( playerSide == "X" ) ? "O" : "X";

            if ( playerSide == "O" )
            {
                int bestMoveIndex = Minimax(board, false).index;
                ButtonClicked(bestMoveIndex);
            }
        }
        
    }

    // (Move) Structure
    struct Move
    {
        public int score;
        public int index;
    }

    Move Minimax ( int[,] currentBoard, bool maximizingPlayer )
    {
        int winner = CheckForWinner(currentBoard);
        if ( winner != 0 )
        {
            return new Move { score = winner };
        }
        else if ( IsBoardFull(currentBoard) )
        {
            return new Move { score = 0 };
        }

        Move bestMove = new Move();
        bestMove.index = -1;
        bestMove.score = maximizingPlayer ? int.MinValue : int.MaxValue;

        for ( int i = 0; i < 9; i++ )
        {
            if ( currentBoard[i / 3, i % 3] == 0 )
            {
                int[,] newBoard = (int[,])currentBoard.Clone();
                newBoard[i / 3, i % 3] = maximizingPlayer ? 1 : -1;

                Move newMove = Minimax(newBoard, !maximizingPlayer);

                if ( maximizingPlayer )
                {
                    if ( newMove.score > bestMove.score )
                    {
                        bestMove.score = newMove.score;
                        bestMove.index = i;
                    }
                }
                else
                {
                    if ( newMove.score < bestMove.score )
                    {
                        bestMove.score = newMove.score;
                        bestMove.index = i;
                    }
                }
            }
        }

        return bestMove;
    }

    int CheckForWinner ()
    {
        return CheckForWinner(board);
    }

    int CheckForWinner ( int[,] currentBoard )
    {
        // rows
        for ( int i = 0; i < 3; i++ )
        {
            int sum = currentBoard[i, 0] + currentBoard[i, 1] + currentBoard[i, 2];
            if ( sum == 3 || sum == -3 )
            {
                return sum / 3;
            }
        }

        // columns
        for ( int i = 0; i < 3; i++ )
        {
            int sum = currentBoard[0, i] + currentBoard[1, i] + currentBoard[2, i];
            if ( sum == 3 || sum == -3 )
            {
                return sum / 3;
            }
        }

        // diagonals
        int diagonalSum1 = currentBoard[0, 0] + currentBoard[1, 1] + currentBoard[2, 2];
        if ( diagonalSum1 == 3 || diagonalSum1 == -3 )
        {
            return diagonalSum1 / 3;
        }

        int diagonalSum2 = currentBoard[0, 2] + currentBoard[1, 1] + currentBoard[2, 0];
        if ( diagonalSum2 == 3 || diagonalSum2 == -3 )
        {
            return diagonalSum2 / 3;
        }

        // No winner
        return 0;
    }

    bool IsBoardFull ()
    {
        return IsBoardFull(board);
    }

    bool IsBoardFull ( int[,] currentBoard )
    {
        for ( int i = 0; i < 9; i++ )
        {
            if ( currentBoard[i / 3, i % 3] == 0 )
            {
                return false;
            }
        }
        return true;
    }

    void EndGame ( int winner )
    {
        gameOverPanel.SetActive(true);
        if ( winner == 1 )
        {
            winText.text = "X Wins!";
        }
        else if ( winner == -1 )
        {
            winText.text = "O Wins!";
        }
        else
        {
            winText.text = "Tie!";
        }
    }

    public void ResetGameButton ()
    {
        for ( int i = 0; i < buttons.Length; i++ )
        {
            buttons[i].GetComponentInChildren<Text>().text = "";
            buttons[i].interactable = true;
            // Clear board array
            board[i / 3, i % 3] = 0;
        }

        gameOverPanel.SetActive(false);

        playerSide = "X";
        playerSide = ( Random.Range(0, 2) == 0 ) ? "X" : "O";
        Debug.Log("Reset player side" + playerSide);

        // If it's the AI's turn, make a move
        if ( playerSide == "O" )
        {
            int randomIndex = Random.Range(0, 8);
            ButtonClicked(randomIndex);
            Debug.Log("RandomIndex" + randomIndex);
        }
    }        
}