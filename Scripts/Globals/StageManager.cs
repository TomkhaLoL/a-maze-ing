using Godot;
using System;
using Godot.Collections;

public partial class StageManager {
   // [Export] PackedScene 
   [Export]
   private Dictionary<int, PackedScene> stages = new Dictionary<int, PackedScene>();

   private PackedScene stage1 = GD.Load<PackedScene>("res://Scenes/Stages/Stage1.tscn");
   private PackedScene stage2 = GD.Load<PackedScene>("res://Scenes/Stages/Stage2.tscn");
 
   private PackedScene stage666 = GD.Load<PackedScene>("res://Scenes/Stages/Stage666.tscn");
   
   
}

