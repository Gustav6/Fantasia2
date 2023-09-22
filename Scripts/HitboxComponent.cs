 using Godot;
using System;

public partial class HitboxComponent : Node2D
{
	[Export]
	private HealthComponent healthComponent;

	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
    {

    }

	public bool CanAcceptDamage()
	{
		return healthComponent?.hasHealthRemaining ?? true;
	}

	public void HandleCollisions(AttackComponent attack)
	{
		if (healthComponent.hasHealthRemaining)
		{
            healthComponent.Damage(attack.attackDamage);
        }
    }
    public void OnEnter(Node2D other)
    {

    }
}
