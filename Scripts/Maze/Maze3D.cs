using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Godot.Collections;

public partial class Maze3D : Node3D {
	[Export]
	private PackedScene cellScene;

	[Export] 
	private PackedScene doorCellScene;

	[Export]
	private PackedScene tileMapScene;

	private List<Cell> cells = new List<Cell>();
	
	public override void _Ready() {
		GenerateMap();
	}

	private void GenerateMap() {
		if(tileMapScene == null) {
			GD.PrintErr("TileMapScene is null. Cannot generate map.");
			return;
		}

		TileMap tileMap = tileMapScene.Instantiate<TileMap>();
		List<Vector2I> usedCells = tileMap.GetUsedCells(0).ToList();
		List<Vector2I> doorCells = tileMap.GetUsedCells(1).ToList();
		tileMap.Free();
		foreach (var tile in usedCells) {
			Cell cell = cellScene.Instantiate<Cell>();
			AddChild(cell);
			cell.Translate(new Vector3(tile.X * 2, 0, tile.Y * 2));
			cells.Add(cell);
		}
		foreach (var tile in doorCells) {
			Cell cell = doorCellScene.Instantiate<Cell>();
			AddChild(cell);
			cell.Translate(new Vector3(tile.X * 2, 0, tile.Y * 2));
			cells.Add(cell);
		}
		foreach(Cell cell in cells) {
			usedCells.Add(doorCells[0]); // this door code is giga ass
			cell.SetCellWalls(usedCells);
		}
	}
}
