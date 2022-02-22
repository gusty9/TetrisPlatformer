using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playfield : MonoBehaviour
{
    public static int w = 10;
    public static int h = 20;
    public static Transform[,] grid = new Transform[w, h];


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //round a vector to whole integer coordinates
    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    //see if a game object is inside of the playfield
    public static bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < w && (int)pos.y >= 0);
    }

    //delete a specific row
    public static void deleteRow(int row)
    {
        for (int x = 0; x < w; x++)
        {
            Destroy(grid[x, row].gameObject);
            grid[x, row] = null;
        }
    }

    //decrease one whole row to a row below it
    public static void decreaseRow(int y)
    {
        for (int x = 0; x < w; x++)
        {
            if (grid[x,y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    //decrease all rows above a specific row
    public static void decreaseRowsAbove(int row)
    {
        for (int i = row; i < h; i++)
        {
            decreaseRow(i);
        }
    }

    //determine if an entire row is full
    public static bool isRowFull(int row)
    {
        for (int x = 0; x < w; x++)
        {
            if (grid[x,row] == null)
            {
                return false;
            }
        }
        return true;
    }

    public static void deleteFullRows()
    {
        for (int y = 0; y < h; y++)
        {
            if (isRowFull(y))
            {
                deleteRow(y);
                decreaseRowsAbove(y + 1);
                y--;
            }
        }
    }
}
