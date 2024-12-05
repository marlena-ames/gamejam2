using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // For scene management
using TMPro;
using UnityEngine.UI;
using System;

public class gameManager : MonoBehaviour
{
    static string k, k2 = "";
    static Image Pieces;
    static Color color = Color.white;
    public GameObject Checkerboard;
    public TMP_Text r, w;

    private static bool isFighting = false;

    void Update()
    {
        // Check if returning from FightingScene
        if (isFighting && !SceneManager.GetActiveScene().name.Equals("FightingScene"))
        {
            isFighting = false; // Reset fight flag
            AwardPoint(FightingManager.GetWinner()); // Award point to the fight winner
        }
    }

    public void Piecesclick(RectTransform t)
    {
        checkerboardInitializer.OutlineSingleInstance();
        Pieces = t.transform.Find("Pieces").GetComponent<Image>();

        if (color == Pieces.color)
        {
            string name = t.name;
            int i, j;
            i = Convert.ToInt32((name.Split("&"))[0]);
            j = Convert.ToInt32((name.Split("&"))[1]);
            int direction = (Pieces.color == Color.red) ? 1 : -1; // Red moves down, White moves up

            // Check diagonal left
            ValidateMove(i, j, direction, -1);

            // Check diagonal right
            ValidateMove(i, j, direction, 1);

            // Check capture moves
            ValidateCapture(i, j, direction, -1);
            ValidateCapture(i, j, direction, 1);

            k = i + " " + j; // Save the source position
        }
    }

    private void ValidateMove(int i, int j, int dir, int offset)
    {
        int targetI = i + dir;
        int targetJ = j + offset;

        if (IsInBounds(targetI, targetJ) && !checkerboardInitializer.g[targetI, targetJ].transform.Find("Pieces").GetComponent<Image>().enabled)
        {
            checkerboardInitializer.g[targetI, targetJ].transform.Find("Outline").GetComponent<Image>().enabled = true;
        }
    }

    private void ValidateCapture(int i, int j, int dir, int offset)
    {
        int midI = i + dir;
        int midJ = j + offset;
        int targetI = i + (2 * dir);
        int targetJ = j + (2 * offset);

        if (IsInBounds(midI, midJ) && IsInBounds(targetI, targetJ))
        {
            Image midPiece = checkerboardInitializer.g[midI, midJ].transform.Find("Pieces").GetComponent<Image>();
            Image targetPiece = checkerboardInitializer.g[targetI, targetJ].transform.Find("Pieces").GetComponent<Image>();

            if (midPiece.enabled && midPiece.color != Pieces.color && !targetPiece.enabled)
            {
                checkerboardInitializer.g[targetI, targetJ].transform.Find("Outline").GetComponent<Image>().enabled = true;
                k2 = midI + " " + midJ; // Save the capture position
            }
        }
    }

    private bool IsInBounds(int i, int j)
    {
        return i >= 0 && i < checkerboardInitializer.dim && j >= 0 && j < checkerboardInitializer.dim;
    }

    public void OutlineClick(RectTransform t)
    {
        string name = t.name;
        int destI = Convert.ToInt32((name.Split('&'))[0]);
        int destJ = Convert.ToInt32((name.Split('&'))[1]);

        // Check if the destination cell is a valid move (outlined)
        if (!checkerboardInitializer.g[destI, destJ].transform.Find("Outline").GetComponent<Image>().enabled)
        {
            Debug.LogWarning("Invalid move attempt!");
            return; // Abort if the move is invalid
        }

        // Move piece to the destination
        checkerboardInitializer.g[destI, destJ].transform.Find("Pieces").GetComponent<Image>().color = color;
        checkerboardInitializer.g[destI, destJ].transform.Find("Pieces").GetComponent<Image>().enabled = true;

        // Disable the piece at the source
        int srcI = Convert.ToInt32((k.Split(' '))[0]);
        int srcJ = Convert.ToInt32((k.Split(' '))[1]);
        checkerboardInitializer.g[srcI, srcJ].transform.Find("Pieces").GetComponent<Image>().enabled = false;

        // Handle capture logic
        if (!string.IsNullOrEmpty(k2))
        {
            int capI = Convert.ToInt32((k2.Split(' '))[0]);
            int capJ = Convert.ToInt32((k2.Split(' '))[1]);
            checkerboardInitializer.g[capI, capJ].transform.Find("Pieces").GetComponent<Image>().enabled = false;

            // Transition to FightingScene
            isFighting = true;
            FightingManager.SetFighters(color);
            SceneManager.LoadScene("FightingScene");
            return; // Exit after initiating fight
        }

        // Switch player turn
        color = (color == Color.white) ? Color.red : Color.white;
    }

    private void AwardPoint(Color winnerColor)
    {
        if (winnerColor == Color.white)
        {
            A2('w'); // White gets a point
        }
        else if (winnerColor == Color.red)
        {
            A2('r'); // Red gets a point
        }

        // Refresh score text
        r.text = checkerboardInitializer.Cmp.x.ToString();
        w.text = checkerboardInitializer.Cmp.y.ToString();
    }

    public void A2(char c)
    {
        if (c == 'w')
        {
            checkerboardInitializer.Cmp.x++;
        }
        else
        {
            checkerboardInitializer.Cmp.y++;
        }

        // These if statements can be changed depending on color
        if (checkerboardInitializer.Cmp.x >= 12)
        {
            Checkerboard.gameObject.SetActive(true);
            Checkerboard.transform.Find("w").GetComponent<Text>().text = "White Wins!";
        }

        if (checkerboardInitializer.Cmp.y >= 12)
        {
            Checkerboard.gameObject.SetActive(true);
            Checkerboard.transform.Find("w").GetComponent<Text>().text = "Red Wins!";
        }
    }
}
