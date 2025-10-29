using Godot;
using System;

public partial class LifeGameScene : Node
{
	[Export]
	private CellViewer viewer;

	[Export]
	private LifeCalculator calculator;

	bool IsGridInStartPosition = true;

	public override void _Ready()
	{
		viewer.TileClickedOn += ClickedOnTile;
	}

	public void ClickedOnTile(Vector2I position)
	{
		
        if (!IsGridInStartPosition)
		{
			IsGridInStartPosition = true;
			calculator.Pause();
			calculator.MakeActiveCellsNewStart();
        }
		calculator.SwitchCellToStart(position);

	}

	public void Start()
	{
		IsGridInStartPosition = false;
		calculator.Start();
	}

	public void Pause()
	{
		calculator.Pause();
	}

	public void Restart()
	{
		calculator.Restart();
	}
	
	public void Clear()
    {
		calculator.Clear();
    }

	public void ChangeSpeed(float newSpeed)
	{
		calculator.TimeBetweenUpdates = newSpeed;
	}
	


	
}
