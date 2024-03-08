using Godot;
using System;

public partial class MainMenu : Control {
    [Export] private Control menu;
    [Export] private Button playButton;
    [Export] private Button creditsButton;
    [Export] private Button quitButton;
    [Export] private Control stageSelect;
    [Export] private Control creditsMenu;
    [Export] private Button exitCreditsButton;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        playButton.Pressed += PlayButtonOnPressed;
        creditsButton.Pressed += CreditsButtonOnPressed;
        quitButton.Pressed += QuitButtonOnPressed;
        exitCreditsButton.Pressed += ExitCreditsButtonOnPressed;
    }

    private void CreditsButtonOnPressed() {
        creditsMenu.Visible = true;
        menu.Visible = false;
    }

    private void ExitCreditsButtonOnPressed() {
        creditsMenu.Visible = false;
        menu.Visible = true;
    }

    private void PlayButtonOnPressed() {
        stageSelect.Visible = true;
        menu.Visible = false;
    }

    private void QuitButtonOnPressed() {
        GetTree().Quit();
    }
}