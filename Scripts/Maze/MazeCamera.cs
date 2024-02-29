using Godot;
using System;

public partial class MazeCamera : Camera3D
{
		public override void _Input(InputEvent @event) {
		// Handle Camera movement
		if (@event is InputEventMouseMotion) {
			// Rotate with mouse movement
			InputEventMouseMotion mouseMotion = @event as InputEventMouseMotion;
			this.RotateY(-mouseMotion.Relative.X * 0.1f);
			//this.head.RotateX(-mouseMotion.Relative.Y * 0.1f);
			// Clamp x rotation
			Vector3 cameraRotation = Rotation;
			cameraRotation.X = Mathf.Clamp(cameraRotation.X, Mathf.DegToRad(-80f), Mathf.DegToRad(80f));
		}
	}
}
