using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class P_P : MonoBehaviour
{
    public GameObject btn;
    public Transform btnholder;
    public Text wintxt, playertxt;

    private int gridnum = 3, matchnum = 3;
    private List<GameObject> l = new List<GameObject>();
    private bool win = false;
    private play p;

    private void Start()
    {
        p = FindObjectOfType<play>();
        gridnum = p.gridnum;
        matchnum = p.matchnum;

        SetCellSize();

        for (int i = 0; i < gridnum * gridnum; i++)
        {
            GameObject g = Instantiate(btn, btnholder);
            Button button = g.GetComponent<Button>();
            Text buttonText = g.GetComponentInChildren<Text>();
            buttonText.text = "";
            button.onClick.AddListener(() => click(g));
            l.Add(g);
        }
    }

    private void SetCellSize()
    {
        float cellSize = 91f;
        switch (gridnum)
        {
            case 4:
                cellSize = 68f;
                break;
            case 5:
                cellSize = 54f;
                break;
            case 6:
                cellSize = 45f;
                break;
        }

        btnholder.GetComponent<GridLayoutGroup>().cellSize = new Vector2(cellSize, cellSize);
    }

    public void click(GameObject g)
    {
        if (win) return;

        Text buttonText = g.GetComponentInChildren<Text>();
        if (buttonText.text == "")
        {
            buttonText.text = (l.Count % 2 == 0) ? "O" : "X";
            g.GetComponent<Button>().interactable = false;
            playertxt.text = (l.Count % 2 == 0) ? "Player B" : "Player A";
            check();
        }
    }

    public void restart()
    {
        SceneManager.LoadScene("play");
    }

    private void check()
    {
        // Implement your win condition checks here based on gridnum and matchnum
    }

    // Other win condition checking methods go here...

    private void Win(string winner)
    {
        wintxt.text = winner + " win";
        win = true;
    }
}
