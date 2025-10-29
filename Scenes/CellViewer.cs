using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class CellViewer : TileMapLayer
{
	[Signal]
	public delegate void TileClickedOnEventHandler(Vector2I position);

	[Export]
	private Label _countLabel;
	private List<Vector2I> ChangedCells = [];
	private bool mousebuttonClicked = false;
	public void Activate(IEnumerable<Vector2I> cells)
	{
		Clear();
		foreach (var cell in cells)
		{

			SetCell(cell, 0, Vector2I.Zero);
		}
		_countLabel.Text = $"Active Cells : {cells.Count()}";
	}

    public override void _Process(double delta)
	{
		if (mousebuttonClicked)
		{
			Vector2I Hovercell = LocalToMap(GetLocalMousePosition());
            if (!ChangedCells.Contains(Hovercell))
			{
				ChangedCells.Add(Hovercell);
				EmitSignal(SignalName.TileClickedOn, Hovercell);
				
            }
		}
    }


    public override void _UnhandledInput(InputEvent @event)
    {

	
		if (@event is InputEventMouseButton mouseButtonEvent)
		{
			if (mouseButtonEvent.ButtonIndex == MouseButton.Left)
			{
				if (mouseButtonEvent.Pressed)
				{
					mousebuttonClicked = true;
					
                }
                else
                {
					mousebuttonClicked = false;
					ChangedCells = [];
                }
			
			}


		}
	}
}
