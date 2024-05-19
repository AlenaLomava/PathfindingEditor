using Assets.Scripts.Field;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Pathfinding
{
    public static class JPSPathfinding
    {
        public static List<Cell> FindPathJPS(GameGrid grid, Cell startCell, Cell endCell)
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

                foreach (var neighbor in IdentifySuccessors(grid, currentCell, endCell, gCosts, hCosts, parents))
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

        private static List<Cell> IdentifySuccessors(GameGrid grid, Cell currentCell, Cell endCell,
                                                     Dictionary<Cell, int> gCosts, Dictionary<Cell, int> hCosts,
                                                     Dictionary<Cell, Cell> parents)
        {
            var successors = new List<Cell>();

            foreach (var direction in GetNeighborDirections())
            {
                var jumpPoint = Jump(grid, currentCell, direction, endCell, gCosts);
                if (jumpPoint != null)
                {
                    successors.Add(jumpPoint);
                }
            }

            return successors;
        }

        private static Cell Jump(GameGrid grid, Cell currentCell, Vector2Int direction, Cell endCell, Dictionary<Cell, int> gCosts)
        {
            int newRow = currentCell.Row + direction.y;
            int newCol = currentCell.Column + direction.x;

            if (!grid.IsInBounds(newRow, newCol))
            {
                return null;
            }

            var nextCell = grid.GetCell(newRow, newCol);
            if (nextCell == null || !nextCell.IsTraversable)
            {
                return null;
            }

            if (nextCell == endCell)
            {
                return nextCell;
            }

            if (HasForcedNeighbors(grid, nextCell, direction))
            {
                return nextCell;
            }

            if (direction.x != 0 && direction.y != 0)
            {
                if (Jump(grid, nextCell, new Vector2Int(direction.x, 0), endCell, gCosts) != null ||
                    Jump(grid, nextCell, new Vector2Int(0, direction.y), endCell, gCosts) != null)
                {
                    return nextCell;
                }
            }
            else if (direction.x != 0)
            {
                if (Jump(grid, nextCell, new Vector2Int(direction.x, 0), endCell, gCosts) != null)
                {
                    return nextCell;
                }
            }
            else if (direction.y != 0)
            {
                if (Jump(grid, nextCell, new Vector2Int(0, direction.y), endCell, gCosts) != null)
                {
                    return nextCell;
                }
            }

            return Jump(grid, nextCell, direction, endCell, gCosts);
        }

        private static bool HasForcedNeighbors(GameGrid grid, Cell cell, Vector2Int direction)
        {
            if (direction.x != 0 && direction.y != 0)
            {
                return (grid.GetCell(cell.Row - direction.y, cell.Column + direction.x)?.IsTraversable == false &&
                        grid.GetCell(cell.Row + direction.y, cell.Column - direction.x)?.IsTraversable == true) ||
                       (grid.GetCell(cell.Row + direction.y, cell.Column + direction.x)?.IsTraversable == false &&
                        grid.GetCell(cell.Row - direction.y, cell.Column - direction.x)?.IsTraversable == true);
            }
            else if (direction.x != 0)
            {
                return grid.GetCell(cell.Row - 1, cell.Column + direction.x)?.IsTraversable == false &&
                       grid.GetCell(cell.Row + 1, cell.Column + direction.x)?.IsTraversable == true;
            }
            else if (direction.y != 0)
            {
                return grid.GetCell(cell.Row + direction.y, cell.Column - 1)?.IsTraversable == false &&
                       grid.GetCell(cell.Row + direction.y, cell.Column + 1)?.IsTraversable == true;
            }

            return false;
        }

        private static List<Vector2Int> GetNeighborDirections()
        {
            return new List<Vector2Int>
        {
            new Vector2Int(-1, 0), // Up
            new Vector2Int(1, 0), // Down
            new Vector2Int(0, -1), // Left
            new Vector2Int(0, 1) // Right
        };
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
