using Godot;
using System;

public partial class AttackComponent : Node2D
{
	[Export]
	public float attackDamage { get; set; }

	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
