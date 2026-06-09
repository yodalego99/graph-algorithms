# Graph Theory Algorithms Visualizer

Windows Forms application for creating graphs and visualizing classic graph algorithms step by step.

The project lets you draw nodes and edges interactively, configure directed/undirected and weighted graphs, then run multiple algorithms with visual feedback.

## Features

- Interactive graph editor (add, move, and delete nodes/edges)
- Support for both undirected and directed edges
- Manual edge weight editing
- Random weight generation in a user-defined range
- Start and end node selection
- Algorithm explanation panel
- Step-by-step controls for selected algorithms (`Forward`, `Backward`, `Stop`)
- Save/load graph states to/from XML, with generated image thumbnails

## Implemented Algorithms

1. Depth-First Search (DFS)
	- Traversal starts from the selected start node.
	- Supports step-by-step forward/backward replay.

2. Breadth-First Search (BFS)
	- Traversal starts from the selected start node.
	- Uses forward stepping.

3. Hamiltonian Route (Path)
	- Backtracking-based path search.
	- Displays animated result if a valid path is found.

4. Dijkstra Shortest Path
	- Requires both a start node and an end node.
	- Uses edge weights and highlights the shortest path.
	- Assumes positive edge weights.

5. Boruvka Minimum Spanning Tree
	- Works on weighted, undirected graphs.
	- Disabled when directed edges are present.

6. Topological Sort
	- Works on directed acyclic graphs (DAGs).
	- Blocked if the graph has cycles or undirected edges.

## Requirements

- Windows OS
- .NET 6 SDK or newer with .NET 6 targeting pack

## Usage

1. Click `Add Node` to place nodes on the canvas.
2. Click `Add Edge` or `Add Directed Edge`, then drag from one node to another.
3. (Optional) Click `Add Weight`, then click an edge and enter a positive integer.
4. Select a start node with `Select Starting Point`.
5. (For Dijkstra) select an end node with `Select End Point`.
6. Choose an algorithm from the dropdown.
7. Click `Start`.
8. Use `Forward` / `Backward` / `Stop` where supported.

## Save and Load graphs

Before saving or loading:

1. Click `Select Folder`.
2. Choose a directory.

When saving a graph named `Example`, the app generates:

- `Example.bmp` (graph snapshot)
- `Example nodes.xml` (serialized nodes)
- `Example edges.xml` (serialized edges)

Loading is done from thumbnail previews generated from the `.bmp` files in the selected folder.

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.