using Godot;
using System;
using System.Collections.Generic;

public partial class CellViewer : TileMapLayer
{
	[Signal]
	public delegate void TileClickedOnEventHandler(Vector2I position);

	[Export]
	private Label _countLabel;
	private Vector2I? lastclick=null;
	public void Activate(List<Vector2I> cells)
	{
		Clear();
		foreach (var cell in cells)
		{
			
			SetCell(cell, 0,Vector2I.Zero);
		}
		_countLabel.Text = $"Active Cells : {cells.Count}";
	}

    public override void _UnhandledInput(InputEvent @event)
    {

	
		if (@event is InputEventMouseButton mouseButtonEvent)
		{
			if (mouseButtonEvent.ButtonIndex == MouseButton.Left && mouseButtonEvent.Pressed)
			{
				Vector2I clickedCell = LocalToMap(GetLocalMousePosition());
				
					EmitSignal(SignalName.TileClickedOn, clickedCell);
			
			}


		}
	}
}
