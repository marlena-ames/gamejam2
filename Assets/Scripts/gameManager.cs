using System;   
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameManager : MonoBehaviour
{
    static string k, k2 = "";
    static Image Pieces;
    static Color color = Color.white;
    public GameObject Checkerboard;
    public TMP_Text r, w;

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
            int co = -1;

            if (Pieces.color == Color.red)
            {
                co = 1;
            }
            try
            {
                if (!checkerboardInitializer.g[i + co, j - 1].transform.Find("Pieces").GetComponent<Image>().enabled)
                {
                    checkerboardInitializer.g[i + co, j - 1].transform.Find("Outline").GetComponent<Image>().enabled = true;
                }
                else if (checkerboardInitializer.g[i + co, j - 1].transform.Find("Pieces").GetComponent<Image>().color != Pieces.color && !checkerboardInitializer.g[i + (co * 2), j - 2].transform.Find("Pieces").GetComponent<Image>().enabled)
                {
                    checkerboardInitializer.g[i + (co * 2), j - 2].transform.Find("Outline").GetComponent<Image>().enabled = true;
                    k2 = (i + co) + " " + (j - 1);
                }
            }
            catch { }
            try
            {
                if (!checkerboardInitializer.g[i + co, j + 1].transform.Find("Pieces").GetComponent<Image>().enabled)
                {
                    checkerboardInitializer.g[i + co, j + 1].transform.Find("Outline").GetComponent<Image>().enabled = true;
                }
                else if (checkerboardInitializer.g[i + co, j + 1].transform.Find("Pieces").GetComponent<Image>().color != Pieces.color && !checkerboardInitializer.g[i + (co * 2), j + 2].transform.Find("Pieces").GetComponent<Image>().enabled)
                {
                    checkerboardInitializer.g[i + (co * 2), j + 2].transform.Find("Outline").GetComponent<Image>().enabled = true;
                    k2 = (i + co) + " " + (j + 1);
                }
            }
            catch { }
            k = i + " " + j;
        }
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

        //These if statements can be changed depending on color
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

    public void OutlineClick(RectTransform t)
    {
        checkerboardInitializer.OutlineSingleInstance();
        string name = t.name;
        int i, j;
        i = Convert.ToInt32((name.Split('&'))[0]);
        j = Convert.ToInt32((name.Split('&'))[1]);

        checkerboardInitializer.g[i, j].transform.Find("Pieces").GetComponent<Image>().color = color;
        checkerboardInitializer.g[i, j].transform.Find("Pieces").GetComponent<Image>().enabled = true;
        
        i = Convert.ToInt32((k.Split(' '))[0]);
        j = Convert.ToInt32((k.Split(' '))[1]);

        checkerboardInitializer.g[i, j].transform.Find("Pieces").GetComponent<Image>().enabled = false;
        
        if (k2 != "" && ((Convert.ToInt32((name.Split('&'))[1]) - 1) == Convert.ToInt32((k2.Split(' '))[1]) || (Convert.ToInt32((name.Split('&'))[1]) + 1) == Convert.ToInt32((k2.Split(' '))[1])))
        {
            i = Convert.ToInt32((k2.Split(' '))[0]);
            j = Convert.ToInt32((k2.Split(' '))[1]);
            checkerboardInitializer.g[i, j].transform.Find("Pieces").GetComponent<Image>().enabled = false;

            if (color == Color.white)
            {
                A2('w');
            }
            else
            {
                A2('r');
            }
            r.text = checkerboardInitializer.Cmp.x + "";
            w.text = checkerboardInitializer.Cmp.y + "";

            k2 = "";
        }

        if (color == Color.white)
        {
            color = Color.red;
        }
        else
        {
            color = Color.white;
        }
    }
}
