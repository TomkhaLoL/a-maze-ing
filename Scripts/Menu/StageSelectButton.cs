using Godot;
using System;

public partial class StageSelectButton : Button
{
	[Export] private int stageIndex;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Text = stageIndex.ToString();
		Pressed += OnStageSelectButtonPressed;
	}

	private void OnStageSelectButtonPressed() {
		var maze3D = (Maze3D) GetTree().Root.FindChild("Maze3D", true, false);
		maze3D.LoadNextStage();
	}
}
