using Godot;
using System;

public partial class Controller : CharacterBody2D
{
    public const float Speed = 300.0f;
    public const float JumpVelocity = -400.0f;
    private AnimationPlayer animationPlayer;
    private Sprite2D sprite;
    private bool isAttacking;

    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void _Ready()
	{
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        sprite = GetNode<Sprite2D>("Sprite2D");
	}

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("attack") && !isAttacking && IsOnFloor())
        {
            isAttacking = true;
            animationPlayer.Play("Attack");
        }
        OnAnimationPlayerAnimationFinished("Attack");
    }

    public void OnAnimationPlayerAnimationFinished(StringName anim)
    {
        if (anim.IsEmpty)
        {
            isAttacking = false;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        if (!IsOnFloor())
        {
            velocity.Y += gravity * (float)delta;
        }

        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
        {
            velocity.Y = JumpVelocity;
        }

        Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        if (!isAttacking && direction != Vector2.Zero)
        {
            velocity.X = direction.X * Speed;
            sprite.Scale = new Vector2(direction.X, 1);
            animationPlayer.Play("Run");
        }
        else if (!isAttacking && direction == Vector2.Zero)
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            animationPlayer.Play("Idle");
        }

        Velocity = velocity;
        MoveAndSlide();
    }

    public void Attack()
    {

    }
}
