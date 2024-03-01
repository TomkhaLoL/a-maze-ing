using Godot;
using System;

public partial class Coin : AnimatedSprite3D {
	private MazePlayer mazePlayer;
	private Globals globals;
	private Area3D area;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		Autoplay = "default";
		area = GetNode<Area3D>("Area3D");
		area.AreaEntered += AreaOnAreaEntered;
	}

	private void AreaOnAreaEntered(Area3D area3D) {
		if (area3D.Name.Equals("PlayerArea")) { 
			PickupCoin();
		}
	}

	private void PickupCoin() {
		Globals.singleton.AddToCoinCounter();
		//Play Coin Sound Effect
		QueueFree();
	}

	public override void _Process(double delta) {
		if (mazePlayer == null)
		{
			// Attempt to find the player in the scene
			mazePlayer = GetTree().Root.FindChild("LabyrinthPlayer", true, false) as MazePlayer;
		}

		if (mazePlayer != null)
		{
			// Orient the coin towards the player
			RotationDegrees = mazePlayer.RotationDegrees;
		}
	}
}
