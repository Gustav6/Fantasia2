using Godot;
using System;

public partial class HealthComponent : Node2D
{
	public bool hasHealthRemaining => !Mathf.IsEqualApprox(currentHealth, 0f);

	[Export]
	public float maxHealth = 300;

    private float currentHealth;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		currentHealth = maxHealth;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void Damage(float damage)
    {
		currentHealth -= damage;
    }

    public void heal(float heal)
	{
		Damage(-heal);
	}
}
