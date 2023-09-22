using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MazeSpawner : MonoBehaviour
{

    public int seed;
    public Vector2Int MazeSize;
    public GameObject MazeCellExample;
    public Maze maze;
    // Start is called before the first frame update
    void Start()
    {

        MazeGenerator generator = new MazeGenerator();
        generator.Width = MazeSize.x;
        generator.Lenght = MazeSize.y;
        maze = generator.GenerateMaze();
        
        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.cells.GetLength(1); y++)
            {
                Cell c = Instantiate(MazeCellExample, new Vector3(x, 0.5f, y), Quaternion.identity).GetComponent<Cell>();
                c.WallLeft.SetActive(maze.cells[x, y].WallLeft);
                c.WallBack.SetActive(maze.cells[x, y].WallBack);
            }
        }
    }

}
