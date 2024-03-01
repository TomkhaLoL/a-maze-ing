using Godot;

[GlobalClass]
public partial class TexturePack : Resource {
    [Export] public Material CeilingMaterial { get; private set; }
    [Export] public Material WallMaterial { get; private set; }
    [Export] public Material FloorMaterial { get; private set; }
}
