# Pathfinding Editor

## Introduction

This editor allows you to create, modify, and explore grid for the A* pathfinding algorithm. You can create a field, draw obstacles, draw traversable cells, and visualize paths.

## Features

- **Create Field**: Generate a new grid with specified rows, columns, and obstacles.
- **Draw Obstacle**: Mark cells as obstacles, making them non-traversable.
- **Draw Traversable Cell**: Mark cells as traversable.
- **Draw Path**: Visualize the pathfinding process.

## Controls

### Camera Control

Camera conntrolled with keyboard Movement (W, A, S, D) and mouse Movement (Right Mouse Button (Hold)). Scroll mouse wheel for zoom in and out.

### Example Usage

1. **Generate a Field**:
   - Enter the number of rows, columns, and obstacles.
   - Click the "Generate" button.

2. **Draw Obstacles**:
   - Click the "Draw Obstacle" button.
   - Click on the cells in the grid to mark them as obstacles.

3. **Draw Traversable Cells**:
   - Click the "Draw Traversable Cell" button.
   - Click on the cells in the grid to mark them as traversable.

4. **Visualize Pathfinding**:
   - Click the "Draw Path" button.
   - Choose start and end point (Invalid input will reset the pathfinding mode).
   - A* will run, and the path will be visualized on the field.
