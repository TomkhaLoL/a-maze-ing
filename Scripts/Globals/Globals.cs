using Godot;

public partial class Globals : Node {

    public int coinCount;
    public static Globals singleton;
    

    [Signal]
    public delegate void CoinCountChangedEventHandler(int count);
    
    public override void _Ready() {
        base._Ready();
        singleton = this;
    }

    public void AddToCoinCounter() {
        coinCount++;
        EmitSignal(SignalName.CoinCountChanged, coinCount);
    } 
}

