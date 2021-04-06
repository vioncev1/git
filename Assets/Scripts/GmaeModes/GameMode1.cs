using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode1 : MonoBehaviour, IGameStrategy
{
    private GameObject rightBorder = null;
    private GameObject config = null;

    public void DeleteFullRows()
    {
        for (int y = 0; y < Main.FieldHeight; ++y)
        {
            if (Main.IsRowFull(y))
            {
                Main.DeleteRow(y);
                Main.DecreaseRowsAbove(y + 1);
                --y;
            }
        }
    }

    public void IncScore()
    {
        Main.Score += 100;
    }

    public void SetParams()
    {
        Main.FieldWidth = 10;
        Main.FieldHeight = 20;
        Main.Grid = new Transform[Main.FieldWidth, Main.FieldHeight];

        FindObjectOfType<Spawner>().ShapeChances = new Dictionary<int, int>()
        {
            // GroupT
            { 5, 20 },
            // GroupS
            { 4, 15 },
            // GroupJ
            { 1, 15 },
            // GroupL
            { 2, 15 },
            // GroupZ
            { 9, 15 },
            // GroupI
            { 0, 10 },
            // GroupO
            { 3, 10 },
        };

        rightBorder.transform.position = new Vector3(9.5f, 10.0f, 0.0f);
        config.transform.position = new Vector3(14.0f, 16.0f, 0.0f);

        Main.Score = 0;
    }

    void Start()
    {
        rightBorder = GameObject.Find("RightBorder");
        config = GameObject.Find("config");
    }
}
