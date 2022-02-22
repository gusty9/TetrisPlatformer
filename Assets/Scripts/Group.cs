using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    //time since last gravity tick
    float lastFall = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (!isValidPosition())
        {
            Debug.Log("GAME OVER");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (isValidPosition())
            {
                updateGrid();
            } 
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        } else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (isValidPosition())
            {
                updateGrid();
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        } else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);
            if (isValidPosition())
            {
                updateGrid();
            }
            else
            {
                transform.Rotate(0, 0, 90); ;
            }
        } else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall >= 1)
        {
            transform.position += new Vector3(0, -1, 0);
            if (isValidPosition())
            {
                updateGrid();
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                Playfield.deleteFullRows();
                FindObjectOfType<Spawner>().spawnNext();
                enabled = false;
            }
            lastFall = Time.time;
        }
    }

    bool isValidPosition()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);
            if(!Playfield.insideBorder(v))
            {
                return false;
            }
            if (Playfield.grid[(int) v.x, (int) v.y] != null &&
                Playfield.grid[(int) v.x, (int) v.y].parent != transform)
            {
                return false;
            }
        }
        return true;
    }

    void updateGrid()
    {
        //remove old children from grid
        for (int y = 0; y < Playfield.h; y++)
        {
            for (int x = 0; x < Playfield.w; x++)
            {
                if (Playfield.grid[x,y] != null)
                {
                    if (Playfield.grid[x, y].parent == transform)
                    {
                        Playfield.grid[x, y] = null;
                    }
                }
               
            }
        }
        
        //add new children to the grid
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);
            Playfield.grid[(int)v.x, (int)v.y] = child;
        }
    }
}
