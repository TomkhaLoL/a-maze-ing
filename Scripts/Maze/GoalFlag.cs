using Godot;
using System;

public partial class GoalFlag : Sprite3D {
	private Area3D area3D;

	private MazePlayer mazePlayer;

	[Signal]
	public delegate void OnStageFinishedEventHandler();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		area3D = GetNode<Area3D>("Area3D");
		area3D.AreaEntered += Area3DOnAreaEntered;
	}

	private void Area3DOnAreaEntered(Area3D area) {
		if (area.Name.Equals("PlayerArea")) {
			Announcer.PlayAnnouncerLine(Announcer.AMAZING);
			StageViewCamera stageViewCamera = new StageViewCamera();
			GetTree().Root.FindChild("Maze3D", true, false).AddChild(stageViewCamera);
			stageViewCamera.StartStagePreview(GlobalPosition);
			EmitSignal(SignalName.OnStageFinished);
		}
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
