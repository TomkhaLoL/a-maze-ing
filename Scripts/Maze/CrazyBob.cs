using Godot;
using System;
using amazeing.Scripts.Globals;

public partial class CrazyBob : Sprite3D {
	private MazePlayer mazePlayer;
	[Export] private Area3D area;
	[Export] private AudioStream voiceLine;
	private bool playOnce;
	[Export] private AudioStreamPlayer3D audioPlayer;

	[Export] private Texture2D funnyFace;
	[Export] private Texture2D sadFace;

	[Export] public bool badBob;

	[Signal]
	public delegate void OnBadBobStartedEventHandler();
	[Signal]
	public delegate void OnBadBobFinishedEventHandler();
	public override void _Ready() {
		area = GetNode<Area3D>("Area3D");
		area.AreaEntered += AreaOnAreaEntered;
	}

	public void SetVoiceline(AudioStream audioStream) {
		voiceLine = audioStream;
	}

	private void AreaOnAreaEntered(Area3D area3D) {
		if (area3D.Name.Equals("PlayerArea") && !playOnce) {
			if (voiceLine != null) {
				audioPlayer.Finished += AudioPlayerOnFinished;
				if (badBob) {
					AudioStreamPlayer audioStreamPlayer = AudioManager.singleton.GetSfxPlayer();
					audioStreamPlayer.Finished += AudioPlayerOnFinished;
					audioStreamPlayer.PitchScale = 0.8f;
					audioStreamPlayer.Stream = voiceLine;
					audioStreamPlayer.Play();
					EmitSignal(SignalName.OnBadBobStarted);
				}
				else {
					audioPlayer.Stream = voiceLine;
					audioPlayer.Play();
				}
			}
			else {
				GD.PushError("No AudioStream for Crazy Bob oh no no no");
			}
		}
	}

	private void AudioPlayerOnFinished() {
		playOnce = true;
		if (badBob) {
			EmitSignal(SignalName.OnBadBobFinished);
		}
	}

	public override void _Process(double delta) {
		if (mazePlayer == null) {
			// Attempt to find the player in the scene
			mazePlayer = GetTree().Root.FindChild("LabyrinthPlayer", true, false) as MazePlayer;
		}

		if (mazePlayer != null) {
			// Orient the coin towards the player
			RotationDegrees = mazePlayer.RotationDegrees;
		}
	}
}
