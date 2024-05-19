using Assets.Scripts.Field;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Pathfinding
{
    public static class PathfindingAStar
    {
        public static List<Cell> FindPathAStar(GameGrid grid, Cell startCell, Cell endCell)
        {
            var openSet = new List<Cell> { startCell };
            var closedSet = new HashSet<Cell>();

            var gCosts = new Dictionary<Cell, int>();
            var hCosts = new Dictionary<Cell, int>();
            var parents = new Dictionary<Cell, Cell>();

            gCosts[startCell] = 0;
            hCosts[startCell] = GetDistance(startCell, endCell);

            while (openSet.Count > 0)
            {
                var currentCell = openSet.OrderBy(cell => gCosts[cell] + hCosts[cell]).ThenBy(cell => hCosts[cell]).First();

                if (currentCell == endCell)
                {
                    return RetracePath(startCell, endCell, parents);
                }

                openSet.Remove(currentCell);
                closedSet.Add(currentCell);

                foreach (var neighbor in grid.GetNeighbors(currentCell))
                {
                    if (closedSet.Contains(neighbor))
                    {
                        continue;
                    }

                    var newMovementCostToNeighbor = gCosts[currentCell] + GetDistance(currentCell, neighbor);
                    if (!gCosts.ContainsKey(neighbor) || newMovementCostToNeighbor < gCosts[neighbor])
                    {
                        gCosts[neighbor] = newMovementCostToNeighbor;
                        hCosts[neighbor] = GetDistance(neighbor, endCell);
                        parents[neighbor] = currentCell;

                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }

            return null; // Path not found
        }

        private static List<Cell> RetracePath(Cell startCell, Cell endCell, Dictionary<Cell, Cell> parents)
        {
            var path = new List<Cell>();
            var currentCell = endCell;

            while (currentCell != startCell)
            {
                path.Add(currentCell);
                currentCell = parents[currentCell];
            }

            path.Reverse();
            return path;
        }

        private static int GetDistance(Cell a, Cell b)
        {
            var dstX = Mathf.Abs(a.Column - b.Column);
            var dstY = Mathf.Abs(a.Row - b.Row);

            return dstX + dstY;
        }
    }
}
