using Godot;
using System;
using Godot.Collections;

public partial class StageManager : Node {
   // [Export] PackedScene 
   public static StageManager singleton;
   [Export]
   private Dictionary<int, PackedScene> stages = new Dictionary<int, PackedScene>();

   public static PackedScene stage1 = GD.Load<PackedScene>("res://Scenes/Stages/Stage1.tscn");
   public static PackedScene stage2 = GD.Load<PackedScene>("res://Scenes/Stages/Stage2.tscn");
   public static PackedScene stage3 = GD.Load<PackedScene>("res://Scenes/Stages/Stage2.tscn");
   public static PackedScene stage4 = GD.Load<PackedScene>("res://Scenes/Stages/Stage3.tscn");
   
   
   
   private PackedScene stage666 = GD.Load<PackedScene>("res://Scenes/Stages/Stage666.tscn");

   private PackedScene currentScene = null;

   public override void _Ready() {
      if (singleton == null) {
         singleton = this;
      }
   }
   
}

