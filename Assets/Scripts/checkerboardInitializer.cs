using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class checkerboardInitializer : MonoBehaviour
{
    public static int dim = 8;
    public GameObject checkerboard;
    public Sprite lightTileSprite; // Sprite for light tiles
    public Sprite darkTileSprite; // Sprite for dark tiles
    public Sprite redPieceSprite; // Sprite for red pieces
    public Sprite whitePieceSprite; // Sprite for white pieces
    public static GameObject[,] g = new GameObject[dim, dim];
    public static Vector2 Cmp;

    private void Start()
    {
        Vector2 cs = transform.gameObject.GetComponent<RectTransform>().sizeDelta, size = checkerboard.GetComponent<RectTransform>().sizeDelta;
        cs.x /= 2;
        cs.y /= 2;
        float left = (cs.x - size.x) * -1, top = (cs.y - size.y);
        Color[] colors = new Color[] { Color.white, Color.black };
        Image drt = checkerboard.GetComponent<Image>(), Ci = checkerboard.transform.Find("Pieces").GetComponent<Image>();

        // Below is the loops for touching every panel on the board black and white one by one (nested loop)
        for (int i = 0; i < dim; i++)
        {
            if (i % 2 == 0)
            {
                { colors[0] = Color.black; colors[1] = Color.white; }
            }
            else
            {
                { colors[0] = Color.white; colors[1] = Color.black; }
            }

            for(int j = 0; j < dim; j++)
            {
                drt.color = colors[(((j % 2) == 0) ? 0 : 1)];
                if (i == (dim / 2) - 1 || i == (dim / 2) || drt.color == Color.white)
                {
                    Ci.enabled = false;
                }
                else
                {
                    Ci.enabled = true;
                }

                //Both of these conditions apply, if you want to edit piece colors change below
                if (i < (dim / 2))
                {
                    Ci.color = Color.red;
                }
                else
                {
                    Ci.color = Color.white;
                }
                
                if (drt.color == Color.white)
                {
                    checkerboard.transform.Find("k2").GetComponent<Image>().enabled = false;
                }
                else
                {
                    checkerboard.transform.Find("k2").GetComponent<Image>().enabled = true;
                }

                g[i, j] = Instantiate(checkerboard);
                g[i, j].transform.SetParent(transform.Find("Panel1"));
                g[i, j].transform.localPosition = new Vector3(left, top);
                g[i, j].transform.name = i + "&" + j;
                left += size.x;
            }
            left = (cs.x - size.x) * -1;
            top -= size.y;
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
