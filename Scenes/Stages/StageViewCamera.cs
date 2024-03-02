using Godot;
using System;

public partial class StageViewCamera : Camera3D {
	public bool isRunning = false;
	public override void _Process(double delta)
	{
		if (isRunning) {
			Translate(Vector3.Back * (4 * (float)delta));
			RotationDegrees = new Vector3(-90, RotationDegrees.Y + (4 * (float)delta), RotationDegrees.Z);
		}
	}

	public void StartStagePreview(Vector3 startPosition) {
		GlobalPosition = startPosition;
		Current = true;
		isRunning = true;
	}
}
