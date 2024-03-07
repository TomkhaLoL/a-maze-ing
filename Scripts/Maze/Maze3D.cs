using System;
using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class Maze3D : Node3D {
	[Export] private PackedScene nextStage;
	[Export] private bool preview;
	[Export] private bool postHole;
	[Export] private bool crazyBobChase;
	[Export] private float crazyBobMovementSpeed = 5;
	

	[Export] private PackedScene cellScene;
	[Export] private PackedScene spawnCellScene;
	[Export] private PackedScene goldCellScene;
	[Export] private PackedScene holeCellScene;
	[Export] private PackedScene flagCellScene;
	[Export] private PackedScene crazyBobScene;

	[Export] private AudioStream crazyBobVoiceLine;
	[Export] private AudioStream stageFinishedAnnouncerLine;


 
	[Export] private TileMap tileMap;

	[Export] private TexturePack texturePack;
	private static readonly Vector2I MAZE_CELL_ATLAS_COORDS = new Vector2I(0, 0);
	private static readonly Vector2I SPAWN_MAZE_CELL_ATLAS_COORDS = new Vector2I(1, 0);
	private static readonly Vector2I DESTINATION_MAZE_CELL_ATLAS_COORDS = new Vector2I(0, 1);
	private static readonly Vector2I GOLD_COIN_MAZE_CELL_ATLAS_COORDS = new Vector2I(1, 1);
	private static readonly Vector2I HOLE_MAZE_CELL_ATLAS_COORDS = new Vector2I(2, 0);
	private static readonly Vector2I CRAZY_EMOJI_MAZE_CELL_ATLAS_COORDS = new Vector2I(3, 0); //TODO

	private PackedScene stageCompletedUIPrefab = GD.Load<PackedScene>("res://Scenes/Menu/StageCompletedUI.tscn");
	private GoalFlag goalFlag;

	private int tileSize = 2;
	public override void _Ready() {
		GenerateMap();
	}

	private void GenerateMap() {
		List<Vector2I> mazeCells = tileMap.GetUsedCellsById(0, 0).ToList();
		foreach (var tileCoords in mazeCells) {
			Vector2I atlasCoords = tileMap.GetCellAtlasCoords(0, tileCoords);
			MazeCell cell = null;

			if (atlasCoords.Equals(MAZE_CELL_ATLAS_COORDS)) {
				cell = cellScene.Instantiate<MazeCell>();
			}
			else if (atlasCoords.Equals(SPAWN_MAZE_CELL_ATLAS_COORDS)) {
				cell = spawnCellScene.Instantiate<MazeCell>();
				MazePlayer mazePlayer = (MazePlayer) cell.FindChild("LabyrinthPlayer");
				if (preview) {
					mazePlayer.preview = true;
				}
			}
			else if (atlasCoords.Equals(GOLD_COIN_MAZE_CELL_ATLAS_COORDS)) {
				cell = goldCellScene.Instantiate<MazeCell>();
			}
			else if (atlasCoords.Equals(DESTINATION_MAZE_CELL_ATLAS_COORDS)) {
				cell = flagCellScene.Instantiate<MazeCell>();
				goalFlag = (GoalFlag)cell.FindChild("GoalFlag");
				if (stageFinishedAnnouncerLine != null) {
					goalFlag.SetStageFinishedAnnouncerLine(stageFinishedAnnouncerLine);
				}
				goalFlag.OnStageFinished += OnStageFinished;
			}
			else if (atlasCoords.Equals(HOLE_MAZE_CELL_ATLAS_COORDS)) {
				cell = holeCellScene.Instantiate<MazeCell>();
			}
			else if (atlasCoords.Equals(CRAZY_EMOJI_MAZE_CELL_ATLAS_COORDS)) {
				cell = crazyBobScene.Instantiate<MazeCell>();
				CrazyBob crazyBob = (CrazyBob) cell.FindChild("CrazyBob");
				crazyBob.SetVoiceline(crazyBobVoiceLine);
				if (crazyBobChase) {
					crazyBob.MovementSpeed = crazyBobMovementSpeed;
					crazyBob.MoveTowardsPlayer = true;
				}
				if (postHole) {
					crazyBob.badBob = true;
					crazyBob.OnBadBobStarted += BadBobStarted;
					crazyBob.OnBadBobFinished += BadBobFinished;
				}
			} 

		if (cell != null) {
				AddChild(cell);
				cell.Translate(new Vector3(tileCoords.X * tileSize, 0, tileCoords.Y * tileSize));
				cell.SetCellWalls(mazeCells);
				cell.SetTexturePack(texturePack);
			}
			cell = null;
		}
		tileMap.Free();
	}

	private void BadBobStarted() {
		GetTree().Root.FindChild("PlayerCamera", true, false).QueueFree();
	}
	
	private void BadBobFinished() {
		LoadNextStage();
	}

	private void OnStageFinished() {
		StageCompletedUI stageCompletedUI = (StageCompletedUI) stageCompletedUIPrefab.Instantiate();
		stageCompletedUI.continueButton.Pressed += LoadNextStage;
		stageCompletedUI.SetScore(Globals.singleton.coinCount);
		AddChild(stageCompletedUI);
	}

	public void LoadNextStage() {
		if (nextStage != null) {
			Globals.singleton.coinCount = 0;
			Timer timer = new Timer();
			AddChild(timer);
			Random random = new Random();
			int waitTime = random.Next(3, 9);
			GD.Print(waitTime + " seconds waiting");
			timer.WaitTime = random.Next(waitTime);
			timer.Timeout += () => GetTree().ChangeSceneToPacked(nextStage);
			timer.Start();
		}
		else {
			GD.PushError("No next stage has been set in Maze3D, pls fix");
		}
	}
}