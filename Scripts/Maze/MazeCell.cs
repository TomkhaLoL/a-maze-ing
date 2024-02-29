using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class MazeCell : Node3D {
	private Node3D ceiling;
	private Node3D floor;
	[Export] private Node3D southWall;
	[Export] private Node3D northWall;
	[Export] private Node3D eastWall;
	[Export] private Node3D westWall;
	
	public void SetCellWalls(List<Vector2I> cells) {
		Vector2I position = new Vector2I((int)Position.X/ 2, (int)Position.Z / 2);
		GD.Print(position);
		foreach (var cell in cells) {
		
			if(cell.Equals(position + Vector2I.Right)) {
				GD.Print("Removing east wall.");
				eastWall.QueueFree();
			} else if(cell.Equals(position + Vector2I.Left)) {
		 		GD.Print("Removing west wall.");
				westWall.QueueFree();
			} else if(cell.Equals(position + Vector2I.Up)) {
				northWall.QueueFree();
			} else if(cell.Equals(position + Vector2I.Down)) {
				southWall.QueueFree();
			}
		}
	}
}
