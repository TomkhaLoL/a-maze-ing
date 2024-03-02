using Godot;
using System;
using amazeing.Scripts.Globals;

public partial class Coin : AnimatedSprite3D {
	private MazePlayer mazePlayer;
	private Globals globals;
	private Area3D area;

	private AudioStream coinSfx;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		Autoplay = "default";
		area = GetNode<Area3D>("Area3D");
		area.AreaEntered += AreaOnAreaEntered;
		coinSfx = GD.Load<AudioStream>("res://Audio/Sfx/coin-sfx.wav");
	}

	private void AreaOnAreaEntered(Area3D area3D) {
		if (area3D.Name.Equals("PlayerArea")) { 
			PickupCoin();
		}
	}

	private void PickupCoin() {
		Globals.singleton.AddToCoinCounter();
		AudioManager.singleton.PlaySoundEffect(coinSfx);
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
