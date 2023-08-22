# Other-
Node code
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToeGame : MonoBehaviour
{
    public int gridSize = 3;
    public GameObject cellPrefab;
    public Text resultText;
    private GameObject[,] cells;
    private int currentPlayer = 1;
    private bool isGameOver = false;

    private void Start()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        cells = new GameObject[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 position = new Vector3(x, 0, y);
                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity);
                cell.GetComponent<Cell>().SetCoordinates(x, y);
                cell.GetComponent<Cell>().SetTicTacToeGame(this);
                cells[x, y] = cell;
            }
        }
    }

    public void MakeMove(Cell cell)
    {
        if (isGameOver || !cell.IsEmpty())
            return;

        cell.SetPlayer(currentPlayer);

        if (CheckForWin())
        {
            resultText.text = "Player " + currentPlayer + " wins!";
            isGameOver = true;
        }
        else if (IsBoardFull())
        {
            resultText.text = "It's a draw!";
            isGameOver = true;
        }
        else
        {
            currentPlayer = 3 - currentPlayer; // Toggle between players 1 and 2
        }
    }

    private bool CheckForWin()
    {
        // Check rows, columns, and diagonals for a winning combination
        for (int i = 0; i < gridSize; i++)
        {
            if (cells[i, 0].GetComponent<Cell>().GetPlayer() == currentPlayer &&
                cells[i, 1].GetComponent<Cell>().GetPlayer() == currentPlayer &&
                cells[i, 2].GetComponent<Cell>().GetPlayer() == currentPlayer)
            {
                return true;
            }

            if (cells[0, i].GetComponent<Cell>().GetPlayer() == currentPlayer &&
                cells[1, i].GetComponent<Cell>().GetPlayer() == currentPlayer &&
                cells[2, i].GetComponent<Cell>().GetPlayer() == currentPlayer)
            {
                return true;
            }
        }

        if (cells[0, 0].GetComponent<Cell>().GetPlayer() == currentPlayer &&
            cells[1, 1].GetComponent<Cell>().GetPlayer() == currentPlayer &&
            cells[2, 2].GetComponent<Cell>().GetPlayer() == currentPlayer)
        {
            return true;
        }

        if (cells[0, 2].GetComponent<Cell>().GetPlayer() == currentPlayer &&
            cells[1, 1].GetComponent<Cell>().GetPlayer() == currentPlayer &&
            cells[2, 0].GetComponent<Cell>().GetPlayer() == currentPlayer)
        {
            return true;
        }

        return false;
    }

    private bool IsBoardFull()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                if (cells[x, y].GetComponent<Cell>().IsEmpty())
                {
                    return false;
                }
            }
        }
        return true;
    }
}

Note
This code should give you a functional dynamic grid Tic Tac Toe game in Unity. Remember to attach the Cell script to your cell prefab, and also create a UI Text element in your canvas and link it to the resultText field in the TicTacToeGame script


You'll also need to create a Cell script and attach it to your cell prefab. Here's a basic outline for the Cell script:


using UnityEngine;

public class Cell : MonoBehaviour
{
    private int player = 0; // 0 for empty, 1 for player 1, 2 for player 2
    private int x;
    private int y;

    public void SetCoordinates(int xCoord, int yCoord)
    {
        x = xCoord;
        y = yCoord;
    }

    public bool IsEmpty()
    {
        return player == 0;
    }

    public void SetPlayer(int playerNumber)
    {
        player = playerNumber;
        // Update the visual representation of the cell based on the player number
    }

    // Add any other methods you need here
}

Remember that this is a basic example to get you started. You will need to expand and enhance the code to include win condition checks, end-game handling, player feedback, and more.
