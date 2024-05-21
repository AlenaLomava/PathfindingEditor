using Assets.Scripts.Field;
using Priority_Queue;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Pathfinding
{
    public static class PathfindingAStar
    {
        private const int STEP_WEIGHT = 1;

        public static (List<Cell> path, int exploredNodes) FindPath(IField field, Cell startCell, Cell endCell)
        {
            var openSet = new SimplePriorityQueue<Cell, int>();
            var closedSet = new HashSet<Cell>();

            var gCosts = new Dictionary<Cell, int>();

            var hCosts = new Dictionary<Cell, int>();
            var parents = new Dictionary<Cell, Cell>();

            int exploredNodes = 0;

            gCosts[startCell] = 0;
            hCosts[startCell] = GetDistance(startCell, endCell);
            openSet.Enqueue(startCell, hCosts[startCell]);

            while (openSet.Count > 0)
            {
                var currentCell = openSet.Dequeue();

                exploredNodes++;

                if (currentCell == endCell)
                {
                    return (RetracePath(startCell, endCell, parents), exploredNodes);
                }

                closedSet.Add(currentCell);

                var neighbors = field.GetCachedNeighbors(currentCell);

                foreach (var neighbor in neighbors)
                {
                    if (closedSet.Contains(neighbor))
                    {
                        continue;
                    }

                    var newMovementCostToNeighbor = gCosts[currentCell] + STEP_WEIGHT;
                    if (!gCosts.ContainsKey(neighbor) || newMovementCostToNeighbor < gCosts[neighbor])
                    {
                        gCosts[neighbor] = newMovementCostToNeighbor;
                        hCosts[neighbor] = GetDistance(neighbor, endCell);
                        parents[neighbor] = currentCell;

                        if (!openSet.Contains(neighbor))
                        {
                            var priority = gCosts[neighbor] + hCosts[neighbor];
                            openSet.Enqueue(neighbor, priority);
                        }
                        else
                        {
                            openSet.UpdatePriority(neighbor, gCosts[neighbor] + hCosts[neighbor]);
                        }
                    }
                }
            }

            return (null, exploredNodes);
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

            path.Add(startCell);
            path.Reverse();
            return path;
        }

        private static int GetDistance(Cell a, Cell b)
        {
            var dstX = Mathf.Abs(a.Column - b.Column);
            var dstY = Mathf.Abs(a.Row - b.Row);

            return dstX + dstY;
        }

        //For test purposes
        private static float GetAggressiveHeuristic(Cell a, Cell b)
        {
            var k = 1.5f;
            return k * GetDistance(a, b);
        }
    }
}
