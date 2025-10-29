using Godot;
using System;

public partial class Camera : Camera2D
{
	[Export]
	float ScrollSensitivity = 0.1f;
	[Export]
	float MoveSensitivity = 0.1f;
	[Export]
	float MaxZoom = 100;
	[Export]
	float MinZoom = 0.001f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseButtonEvent)
		{
			float newZoom = Zoom.X;
			if (mouseButtonEvent.ButtonIndex == MouseButton.WheelUp)
			{
				newZoom += newZoom * ScrollSensitivity;
			}
			else if (mouseButtonEvent.ButtonIndex == MouseButton.WheelDown)
			{
				newZoom -= newZoom * ScrollSensitivity;
			}
			newZoom = MathF.Max(MinZoom, MathF.Min(MaxZoom, newZoom));
			Zoom = newZoom * Vector2.One;


		}
		if (@event is InputEventMouseMotion mousemotionEvent)
		{
			if (Input.IsMouseButtonPressed(MouseButton.Middle))
			{
				float moveX = mousemotionEvent.Relative.X * MoveSensitivity * 1 / Zoom.X;
				float moveY = -mousemotionEvent.Relative.Y * MoveSensitivity * 1 / Zoom.X;
				Position += new Vector2(moveX, moveY);
			}
		}
	}
	
	public void Reset()
    {
		Zoom = Vector2.One;
		Position = Vector2.Zero;  
    }

}
