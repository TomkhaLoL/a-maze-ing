using Godot;
using System;

public partial class CoinCounter : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Globals.singleton.CoinCountChanged += SingletonOnCoinCountChanged;
		
	}
	public override void _ExitTree() {
		Globals.singleton.CoinCountChanged -= SingletonOnCoinCountChanged;
	}

	private void SingletonOnCoinCountChanged(int count) {
		Text = count.ToString();
	}
}
