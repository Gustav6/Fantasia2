using Godot;
using System;

public partial class Controller : CharacterBody2D
{
    [Export(PropertyHint.Range, "0, 500, 2")]
    public float Speed { get; set; }
    public const float JumpVelocity = 400.0f;

    private bool isAttacking;
    private float direction;

    private AnimationPlayer animationPlayer;
    private Sprite2D sprite;

    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        sprite = GetNode<Sprite2D>("Sprite2D");
	}

    public override void _Process(double delta)
    {
        direction = Input.GetAxis("move_left", "move_right");
        if (isAttacking && animationPlayer.CurrentAnimationPosition == animationPlayer.CurrentAnimationLength)
        {
            isAttacking = false;
        }

        if (!isAttacking)
        {
            if (Velocity.X != 0)
                animationPlayer.Play("Run");
            else if (Velocity.X == 0)
                animationPlayer.Play("Idle");

            if (Input.IsActionJustPressed("attack") && IsOnFloor())
            {
                isAttacking = true;
                animationPlayer.Play("Attack");
            }
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
            velocity.Y -= JumpVelocity;
        }

        if (direction != 0)
        {
            velocity.X = direction * Speed;
            sprite.Scale = new Vector2(direction, 1);
        }
        else if (direction == 0)
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
        }

        Velocity = velocity;
        MoveAndSlide();
    }

    public void Attack()
    {

    }
}
