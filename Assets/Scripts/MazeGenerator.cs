using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze
{
    public MazeCell[,] cells;
    public Vector2Int finishPosition;
}
public class MazeCell
{
    public int X;
    public int Y;

    public bool WallLeft = true;
    public bool WallBack = true;
    public bool Visited = false;

    public int DistanceFromStart;
}
public class MazeGenerator
{
    public int Width;
    public int Lenght;

    public Maze GenerateMaze()
    {
        MazeCell[,] cells = new MazeCell[Width, Lenght];

        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                cells[x, y] = new MazeCell { X = x, Y = y };
            }
        }

        for (int x = 0; x < cells.GetLength(0); x++)
        {
            cells[x, Lenght- 1].WallLeft = false;
        }

        for (int y = 0; y < cells.GetLength(1); y++)
        {
            cells[Width - 1, y].WallBack = false;
        }

        RemoveWallsWithBackTracker(cells);

        Maze maze = new Maze();

        maze.cells = cells;
        maze.finishPosition = PlaceMazeExit(cells);

        return maze;

    }

    private void RemoveWallsWithBackTracker(MazeCell[,] maze)

    {
        MazeCell current = maze[0, 0];
        current.Visited = true;

        current.DistanceFromStart = 0;

        Stack<MazeCell> stack = new Stack<MazeCell>();

        do
        {
            List<MazeCell> unvisitedNeighbours = new List<MazeCell>();

            int x = current.X;
            int y = current.Y;

            if (x > 0 && !maze[x - 1, y].Visited) unvisitedNeighbours.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].Visited) unvisitedNeighbours.Add(maze[x, y - 1]);
            if (x < Width - 2 && !maze[x + 1, y].Visited) unvisitedNeighbours.Add(maze[x + 1, y]);
            if (y < Lenght - 2 && !maze[x, y + 1].Visited) unvisitedNeighbours.Add(maze[x, y + 1]);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeCell chosen = unvisitedNeighbours[Random.Range(0 ,unvisitedNeighbours.Count)];
                RemoveWall(current, chosen);
                chosen.Visited = true;

                stack.Push(chosen);
                chosen.DistanceFromStart = current.DistanceFromStart + 1;
                current = chosen;


            }
            else
            {
                current = stack.Pop();
            }

        } while (stack.Count > 0); 
    }

    private void RemoveWall(MazeCell a, MazeCell b)
    {
        if (a.X == b.X)
        {
            if (a.Y > b.Y) a.WallBack = false;
            else b.WallBack = false;
        }
        else
        {
            if (a.X > b.X) a.WallLeft = false;
            else b.WallLeft = false;
        }
    }
    private Vector2Int PlaceMazeExit(MazeCell[,] maze)
    {
        MazeCell furthest = maze[0, 0];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            if (maze[x, Lenght - 2].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[x, Lenght - 2];
            if (maze[x, 0].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[x, 0];
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            if (maze[Width - 2, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[Width - 2, y];
            if (maze[0, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[0, y];
        }

        if (furthest.X == 0) furthest.WallLeft = false;
        else if (furthest.Y == 0) furthest.WallBack = false;
        else if (furthest.X == Width - 2) maze[furthest.X + 1, furthest.Y].WallLeft = false;
        else if (furthest.Y == Lenght - 2) maze[furthest.X, furthest.Y + 1].WallBack = false;

        return new Vector2Int(furthest.X, furthest.Y);
    }
}



