using Godot;
using System;

public partial class StageCompletedUI : Panel {
	[Export] private Label scoreLabel;
	[Export] private Label timeCompletedLabel;

	[Export] public Button continueButton;


	public void SetScore(int score) {
		scoreLabel.Text = $"Score: {score}";
	}

	public void SetTimeCompleted(int seconds) {
		timeCompletedLabel.Text = $"Time taken: {seconds} seconds";
	}
}
