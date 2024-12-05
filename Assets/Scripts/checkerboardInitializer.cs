using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class checkerboardInitializer : MonoBehaviour
{
    public static int dim = 8;
    public GameObject checkboardPrefab;
    public static GameObject[,] g = new GameObject[dim, dim];
    public static Vector2 Cmp;

    private void Start()
    {
        // Get parent (Panel1) size and tile size
        Vector2 parentSize = transform.Find("Panel1").GetComponent<RectTransform>().sizeDelta;
        Vector2 tileSize = checkboardPrefab.GetComponent<RectTransform>().sizeDelta;

        // Calculate the total board size
        Vector2 boardSize = new Vector2(dim * tileSize.x, dim * tileSize.y);

        // Calculate offsets to center the board within Panel1
        float startX = -(boardSize.x / 2) + (tileSize.x / 2);
        float startY = (boardSize.y / 2) - (tileSize.y / 2);

        Color[] colors = new Color[] { Color.white, Color.black };
        Image drt = checkboardPrefab.GetComponent<Image>();
        Image Ci = checkboardPrefab.transform.Find("Pieces").GetComponent<Image>();

        // Loop to create the board tiles
        for (int i = 0; i < dim; i++)
        {
            // Alternate row colors
            if (i % 2 == 0)
            {
                colors[0] = Color.black;
                colors[1] = Color.white;
            }
            else
            {
                colors[0] = Color.white;
                colors[1] = Color.black;
            }

            for (int j = 0; j < dim; j++)
            {
                // Set the tile color
                drt.color = colors[(j % 2 == 0) ? 0 : 1];

                // Configure piece visibility and color
                if (i == (dim / 2) - 1 || i == (dim / 2) || drt.color == Color.white)
                {
                    Ci.enabled = false;
                }
                else
                {
                    Ci.enabled = true;
                }

                if (i < (dim / 2))
                {
                    Ci.color = Color.red;
                }
                else
                {
                    Ci.color = Color.white;
                }

                // Configure "k2" visibility
                if (drt.color == Color.white)
                {
                    checkboardPrefab.transform.Find("k2").GetComponent<Image>().enabled = false;
                }
                else
                {
                    checkboardPrefab.transform.Find("k2").GetComponent<Image>().enabled = true;
                }

                // Instantiate and position the tile
                g[i, j] = Instantiate(checkboardPrefab);
                g[i, j].transform.SetParent(transform.Find("Panel1"));
                g[i, j].transform.localPosition = new Vector3(
                    startX + (j * tileSize.x), // X position
                    startY - (i * tileSize.y), // Y position
                    0 // Z position
                );
                g[i, j].transform.name = $"{i}&{j}";
            }
        }
    }
    public static void OutlineSingleInstance()
    {
        // 8 is the size of the board could switch it to dim in other script
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                g[i, j].transform.Find("Outline").gameObject.GetComponent<Image>().enabled = false;
            }
        }
    }
}
