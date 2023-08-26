public void check()
{
    for (int i = 0; i < gridnum; i++)
    {
        for (int j = 0; j < gridnum; j++)
        {
            if (CheckWin(i, j))
            {
                wintxt.text = l[i * gridnum + j].GetComponentInChildren<Text>().text + " wins";
                MarkWinningButtons(i, j);
                win = true;
                return;
            }
        }
    }
}

private void MarkWinningButtons(int row, int col)
{
    string symbol = l[row * gridnum + col].GetComponentInChildren<Text>().text;

    // Mark horizontally
    for (int i = 0; i < matchnum; i++)
    {
        l[row * gridnum + col + i].GetComponent<Button>().interactable = false;
    }

    // Mark vertically
    for (int i = 0; i < matchnum; i++)
    {
        l[(row + i) * gridnum + col].GetComponent<Button>().interactable = false;
    }

    // Mark diagonally (top-left to bottom-right)
    for (int i = 0; i < matchnum; i++)
    {
        l[(row + i) * gridnum + col + i].GetComponent<Button>().interactable = false;
    }

    // Mark diagonally (top-right to bottom-left)
    for (int i = 0; i < matchnum; i++)
    {
        l[(row + i) * gridnum + col - i].GetComponent<Button>().interactable = false;
    }
}



#####

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class P_P : MonoBehaviour
{
    // ... (Existing code)

    // Define a list to store winning buttons
    private List<GameObject> winningButtons = new List<GameObject>();

    // ... (Existing code)

    public void check()
    {
        // ... (Existing code)

        // Loop through the grid to find winning buttons
        for (int i = 0; i < gridnum; i++)
        {
            for (int j = 0; j < gridnum; j++)
            {
                if (CheckWin(i, j))
                {
                    // ... (Existing code)

                    // Add the winning button to the list
                    winningButtons.Add(l[i * gridnum + j]);
                }
            }
        }

        // Highlight the winning buttons
        HighlightWinningButtons();

        // ... (Existing code)
    }

    // Add this method to highlight the winning buttons
    private void HighlightWinningButtons()
    {
        foreach (GameObject button in winningButtons)
        {
            Image buttonImage = button.GetComponent<Image>();
            buttonImage.color = Color.green;
        }
    }
}


