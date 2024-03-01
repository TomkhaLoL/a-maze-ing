using System;
using amazeing.Scripts.Globals;
using Godot;

public partial class MazePlayer : Node3D {
    private RayCast3D forwardRay;
    private RayCast3D backwardRay;
    private RayCast3D leftRay;
    private RayCast3D rightRay;
    private RayCast3D interactRay;
    public static Direction currentFacingDirection { get; private set; }
    private Direction[] directions = (Direction[])Enum.GetValues(typeof(Direction));


    [Signal]
    public delegate void PlayerChangeDirectionEventHandler(int rotationDegrees);

    public override void _Ready() {
        forwardRay = GetNode<RayCast3D>("PlayerMesh/ForwardRay");
        backwardRay = GetNode<RayCast3D>("PlayerMesh/BackwardsRay");
        leftRay = GetNode<RayCast3D>("PlayerMesh/LeftRay");
        rightRay = GetNode<RayCast3D>("PlayerMesh/RightRay");
        interactRay = GetNode<RayCast3D>("PlayerMesh/InteractRay");
    }

    public void SetPlayerFacingDirection(Direction direction) {
        currentFacingDirection = direction;
    }

    public override void _Process(double delta) {
        Move(delta);
    }

    public void Move(double delta) {
        if (Input.IsActionPressed("move_forwards") && !forwardRay.IsColliding()) {
            Translate(Vector3.Forward * (6 * (float)delta));
        }

        if (Input.IsActionPressed("move_backwards") && !backwardRay.IsColliding()) {
            Translate(Vector3.Back * (6 * (float)delta));
        }

        if (Input.IsActionJustPressed("move_left") && !leftRay.IsColliding()) {
            TurnLeft();
        }

        if (Input.IsActionJustPressed("move_right") && !rightRay.IsColliding()) {
            TurnRight();
        }
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
}