using Godot;
using System.Collections.Generic;

public partial class MazeCell : Node3D {
	[Export] private MeshInstance3D ceiling;
	[Export] private MeshInstance3D floor;
	[Export] private MeshInstance3D southWall;
	[Export] private MeshInstance3D northWall;
	[Export] private MeshInstance3D eastWall;
	[Export] private MeshInstance3D westWall;
	[Export] private bool open;
	
	public override void _Ready() {
		//base._Ready();
		//ceiling = GetNode<MeshInstance3D>("Ceiling");
		//floor = GetNode<MeshInstance3D>("Floor");
		//northWall = GetNode<MeshInstance3D>("NorthWall");
		//eastWall = GetNode<MeshInstance3D>("EastWall");
		//southWall = GetNode<MeshInstance3D>("SouthWall");
		//westWall = GetNode<MeshInstance3D>("WestWall");
	}

	public void SetTexturePack(TexturePack texturePack) {
		southWall.MaterialOverride = texturePack.WallMaterial;
		northWall.MaterialOverride = texturePack.WallMaterial;
		eastWall.MaterialOverride = texturePack.WallMaterial;
		westWall.MaterialOverride = texturePack.WallMaterial;
		ceiling.MaterialOverlay = texturePack.CeilingMaterial;
		if (floor != null) {
			floor.MaterialOverlay = texturePack.FloorMaterial;
		}
	}
	
	public void SetCellWalls(List<Vector2I> cells) {
		if (open) {
			eastWall.QueueFree();
			westWall.QueueFree();
			northWall.QueueFree();
			southWall.QueueFree();
			return;
		}
		Vector2I position = new Vector2I((int)Position.X/ 2, (int)Position.Z / 2);
		GD.Print(position);
		foreach (var cell in cells) {
			if(cell.Equals(position + Vector2I.Right)) {
				eastWall.QueueFree();
			} else if(cell.Equals(position + Vector2I.Left)) {
				westWall.QueueFree();
			} else if(cell.Equals(position + Vector2I.Up)) {
				northWall.QueueFree();
			} else if(cell.Equals(position + Vector2I.Down)) {
				southWall.QueueFree();
			}
		}


	}

	public void SetCellSize(float cellSize) {
		//northWall.Mesh.
	}
}
