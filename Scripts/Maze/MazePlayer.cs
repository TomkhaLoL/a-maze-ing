using System;
using System.Collections.Generic;
using System.Linq;
using amazeing.Scripts.Globals;
using Godot;

public partial class MazePlayer : Node3D {
	//private RayCast3D forwardRay;
	private List<RayCast3D> forwardRays = new List<RayCast3D>();
	private List<RayCast3D> backwardRays = new List<RayCast3D>();
	//private RayCast3D backwardRay;
	private RayCast3D leftRay;
	private RayCast3D rightRay;
	private RayCast3D interactRay;
	private bool canMove = true;
	private bool isFalling = false;
	private Area3D playerArea;
	public static Direction currentFacingDirection { get; private set; }
	private Direction[] directions = (Direction[])Enum.GetValues(typeof(Direction));


	[Signal]
	public delegate void PlayerChangeDirectionEventHandler(int rotationDegrees);

	public override void _Ready() {
		Node3D forwardRaysParent = GetNode<Node3D>("PlayerMesh/ForwardRays");
		foreach (Node raycast in forwardRaysParent.GetChildren()) {
			forwardRays.Add((RayCast3D) raycast);
		}
		
		Node3D backwardRaysParent = GetNode<Node3D>("PlayerMesh/BackwardRays");
		foreach (Node raycast in backwardRaysParent.GetChildren()) {
			backwardRays.Add((RayCast3D) raycast);
		}
		
		leftRay = GetNode<RayCast3D>("PlayerMesh/LeftRay");
		rightRay = GetNode<RayCast3D>("PlayerMesh/RightRay");
		interactRay = GetNode<RayCast3D>("PlayerMesh/InteractRay");
		playerArea = GetNode<Area3D>("PlayerMesh/PlayerArea");
		playerArea.AreaEntered += PlayerAreaOnAreaEntered;
	}

	private void PlayerAreaOnAreaEntered(Area3D area) {
		if (area.Name.Equals("HoleArea")) {
			isFalling = true;
			canMove = false;
		}
	}

	public void SetPlayerFacingDirection(Direction direction) {
		currentFacingDirection = direction;
	}

	public override void _Process(double delta) {
		if (canMove) {
			Move(delta);
		}

		if (isFalling) {
			Fall(delta);
		}
	}

	private void Move(double delta) {
		if (Input.IsActionPressed("move_forwards") && !AreForwardRaysColliding()) {
			Translate(Vector3.Forward * (6 * (float)delta));
		}

		if (Input.IsActionPressed("move_backwards") && !AreBackwardRaysColliding()) {
			Translate(Vector3.Back * (6 * (float)delta));
		}

		if (Input.IsActionJustPressed("move_left")) {
			TurnLeft();
		}

		if (Input.IsActionJustPressed("move_right")) {
			TurnRight();
		}
	}

	private bool AreForwardRaysColliding() {
		return forwardRays.Any(ray => ray.IsColliding());
	}
	
	private bool AreBackwardRaysColliding() {
		return backwardRays.Any(ray => ray.IsColliding());
	}


	private void TurnRight() {
		if ((int)currentFacingDirection + 1 == directions.Length) {
			currentFacingDirection = directions[0];
		}
		else {
			currentFacingDirection = directions[(int)currentFacingDirection + 1];
		}
		RotatePlayer(currentFacingDirection);
	}

	private void TurnLeft() {
		if ((int)currentFacingDirection - 1 < 0) {
			currentFacingDirection = directions[^1];
		}
		else {
			currentFacingDirection = directions[(int)currentFacingDirection - 1];
		}
		RotatePlayer(currentFacingDirection);
	}

	private void RotatePlayer(Direction direction) {
		int rotationDegrees;
		switch (direction) {
			case Direction.North:
				rotationDegrees = 0;
				break;
			case Direction.East:
				rotationDegrees = -90;
				break;
			case Direction.South:
				rotationDegrees = -180;
				break;
			case Direction.West:
				rotationDegrees = -270;
				break;
			default:
				rotationDegrees = 0;
				GD.PushError("No valid direction for the player to turn towards...");
				break;
		}

		RotationDegrees = new Vector3(0, rotationDegrees, 0);
		EmitSignal(SignalName.PlayerChangeDirection, rotationDegrees);
	}
	
	// this is horrible and should never be done like this...
	// falling through the hole is a one time occurence and it shouldn't really be all happening in this class
	// however I don't really have time to refactor this shit until the game jam is over
	
	private bool doOnce = false;
	private void Fall(double delta) {
		Translate(Vector3.Down * (12 * (float)delta));
		if (doOnce == false) {
			AudioManager audioManager = AudioManager.singleton;
			audioManager.StopBackgroundMusic();
			AudioStreamPlayer audioStreamPlayer = audioManager.GetSfxPlayer();
			audioStreamPlayer.Stream = audioManager.bgmMusicCutoff;
			audioStreamPlayer.Finished += AudioStreamPlayerOnFinished;
			audioStreamPlayer.Play();
			doOnce = true;
		}
		
	}

	private void AudioStreamPlayerOnFinished() {
		Maze3D maze3D = (Maze3D)GetTree().Root.FindChild("Maze3D", true, false);
		maze3D.LoadNextStage();
	}
}
