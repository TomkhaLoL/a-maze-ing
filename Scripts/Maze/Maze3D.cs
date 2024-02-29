using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Godot.Collections;

public partial class Maze3D : Node3D {
	[Export] private PackedScene cellScene;

	[Export] private PackedScene spawnCellScene;

	[Export] private TileMap tileMap;

	private static Vector2I MAZE_CELL_ATLAS_COORDS = new Vector2I(0, 0);
	private static Vector2I SPAWN_MAZE_CELL_ATLAS_COORDS = new Vector2I(1, 0);
	private static Vector2I GOLD_COIN_MAZE_CELL_ATLAS_COORDS = new Vector2I(0, 1);
	private static Vector2I DESTINATION_MAZE_CELL_ATLAS_COORDS = new Vector2I(1, 1);

	private int tileSize = 2;
	public override void _Ready() {
		GenerateMap();
	}

	private void GenerateMap() {
		List<Vector2I> mazeCells = tileMap.GetUsedCellsById(0, 0).ToList();
		foreach (var tileCoords in mazeCells) {
			Vector2I atlasCoords = tileMap.GetCellAtlasCoords(0, tileCoords);
			MazeCell cell = null;

			if (atlasCoords.Equals(MAZE_CELL_ATLAS_COORDS)) {
				cell = cellScene.Instantiate<MazeCell>();
			}
			else if (atlasCoords.Equals(SPAWN_MAZE_CELL_ATLAS_COORDS)) {
				cell = spawnCellScene.Instantiate<MazeCell>();
			}
			else if(atlasCoords.Equals(GOLD_COIN_MAZE_CELL_ATLAS_COORDS))
			{
				cell = cellScene.Instantiate<MazeCell>();
			}
			else if(atlasCoords.Equals(DESTINATION_MAZE_CELL_ATLAS_COORDS))
			{
				cell = cellScene.Instantiate<MazeCell>();
			}
			
			if (cell != null) {
				AddChild(cell);
				cell.Translate(new Vector3(tileCoords.X * tileSize, 0, tileCoords.Y * tileSize));
				cell.SetCellWalls(mazeCells);
			}
			cell = null;
		}
		tileMap.Free();
	}
}
