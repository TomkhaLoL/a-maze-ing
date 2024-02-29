using Godot;

public partial class MazePlayer : Node3D {
	private RayCast3D forwardRay;
	private RayCast3D backwardRay;
	private RayCast3D leftRay;
	private RayCast3D rightRay;
	private RayCast3D interactRay;
	
	public override void _Ready() {
		forwardRay = GetNode<RayCast3D>("PlayerMesh/ForwardRay");
		backwardRay = GetNode<RayCast3D>("PlayerMesh/BackwardsRay");
		leftRay = GetNode<RayCast3D>("PlayerMesh/LeftRay");
		rightRay = GetNode<RayCast3D>("PlayerMesh/RightRay");
		interactRay = GetNode<RayCast3D>("PlayerMesh/InteractRay");
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
			RotationDegrees = new Vector3(0, RotationDegrees.Y + 90, 0);
		}
		if (Input.IsActionJustPressed("move_right") && !rightRay.IsColliding()) {
			RotationDegrees = new Vector3(0, RotationDegrees.Y - 90, 0);
		}
	}
}
