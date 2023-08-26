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

