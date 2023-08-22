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

using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    private int player = 0; // 0 for empty, 1 for player 1, 2 for player 2
    private int x;
    private int y;
    private TicTacToeGame gameController;
    public Text cellText;

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
        if (IsEmpty())
        {
            player = playerNumber;
            UpdateCellText();
            gameController.MakeMove(this);
        }
    }

    public int GetPlayer()
    {
        return player;
    }

    public void SetTicTacToeGame(TicTacToeGame game)
    {
        gameController = game;
    }

    public void UpdateCellText()
    {
        cellText.text = player == 1 ? "X" : "O";
    }
}

This script is responsible for managing individual cells within the grid. It holds information about the cell's position, whether it's occupied, and which player has marked it. The SetPlayer method updates the cell's state and triggers the game logic to make a move. The UpdateCellText method updates the visual representation of the cell with "X" or "O" based on the player's mark.

Attach this script to your cell prefab and make sure to link the cellText field to a UI Text element that displays the "X" or "O" on the cell.



using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int gridSize = 3; // Change this to your desired grid size (between 3 and 8)
    public Button buttonPrefab;
    public Text resultText;
    private char[] grid;
    private Button[] buttons;

    private void Start()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        grid = new char[gridSize * gridSize];
        buttons = new Button[gridSize * gridSize];

        for (int i = 0; i < grid.Length; i++)
        {
            Button button = Instantiate(buttonPrefab, transform);
            int row = i / gridSize;
            int col = i % gridSize;
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(col * 50, -row * 50);
            int index = i; // Capture the index for the lambda
            button.onClick.AddListener(() => OnButtonClick(index, button));
            buttons[i] = button;
        }
    }

    private void OnButtonClick(int index, Button button)
    {
        if (grid[index] == '\0')
        {
            grid[index] = 'X'; // For simplicity, assume player X goes first
            button.GetComponentInChildren<Text>().text = "X";

            if (CheckWinCondition(index, 'X'))
            {
                resultText.text = "Player X wins!";
            }
            // Implement player O's turn here

            // Implement a draw condition here
        }
    }

    private bool CheckWinCondition(int moveIndex, char symbol)
    {
        int row = moveIndex / gridSize;
        int col = moveIndex % gridSize;

        // Check row
        bool rowWin = true;
        for (int c = 0; c < gridSize; c++)
        {
            if (grid[row * gridSize + c] != symbol)
            {
                rowWin = false;
                break;
            }
        }

        // Check column
        bool colWin = true;
        for (int r = 0; r < gridSize; r++)
        {
            if (grid[r * gridSize + col] != symbol)
            {
                colWin = false;
                break;
            }
        }

        // Check diagonals
        bool diagonalWin = true;
        if (row == col)
        {
            for (int i = 0; i < gridSize; i++)
            {
                if (grid[i * gridSize + i] != symbol)
                {
                    diagonalWin = false;
                    break;
                }
            }
        }

        bool antiDiagonalWin = true;
        if (row + col == gridSize - 1)
        {
            for (int i = 0; i < gridSize; i++)
            {
                if (grid[i * gridSize + (gridSize - 1 - i)] != symbol)
                {
                    antiDiagonalWin = false;
                    break;
                }
            }
        }

        return rowWin || colWin || diagonalWin || antiDiagonalWin;
    }
}
