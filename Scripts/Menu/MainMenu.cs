using Godot;
using System;
using System.Net.Mime;

public partial class MainMenu : Control {
	[Export] private Control menu;
	[Export] private Button playButton;

	[Export] private Button settingsButton;

	[Export] private Button creditsButton;

	[Export] private Button quitButton;

	[Export] private Control stageSelect;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playButton.Pressed += PlayButtonOnPressed;
		creditsButton.Pressed += CreditsButtonOnPressed;
		settingsButton.Pressed += SettingsButtonOnPressed;
		quitButton.Pressed += QuitButtonOnPressed;
	}

	private void SettingsButtonOnPressed() {
		GD.PushError("Not implemented yet");
	}

	private void CreditsButtonOnPressed() {
		GD.PushError("Not implemented yet");
	}

	private void PlayButtonOnPressed() {
		stageSelect.Visible = true;
		menu.Visible = false;
	}
	
	private void QuitButtonOnPressed() {
		GetTree().Quit();
	}
}
