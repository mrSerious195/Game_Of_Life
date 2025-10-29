using Godot;
using System;
using System.Collections.Generic;

public partial class LifeCalculator : Node
{
	
	[Export] 
	private CellViewer _cellViewer;
	private HashSet<Vector2I> _startingPositions = new();
	private HashSet<Vector2I> _activepositions = new();

	[Export] private Timer _timer;

	private float _timeBetweenUpdates=1.5f;
	[Export] 
	public float  TimeBetweenUpdates{ get=>_timeBetweenUpdates; set{ _timeBetweenUpdates = Mathf.Max(value, 0.01f); } }

    public override void _Ready()
    {
		_timer.Timeout += Update;
    }

	public void Restart()
	{
		Pause();
		_activepositions = new(_startingPositions);
		_cellViewer.Activate(_activepositions);
		
	}

	public void Clear()
	{
		_startingPositions = new();
		_activepositions = new();
		_cellViewer.Activate(_activepositions);
		
	}

	public void SwitchCellToStart(Vector2I cell)
	{
		
		if (_startingPositions.Contains(cell))
		{
			
			_startingPositions.Remove(cell);
			_activepositions.Remove(cell);
		}
		else
		{
			
			_startingPositions.Add(cell);
			_activepositions.Add(cell);
		}
	
		_cellViewer.Activate(_activepositions);
	}

	public void MakeActiveCellsNewStart()
	{
		_startingPositions = _activepositions;
	}

	public void Pause()
	{
		_timer.Paused = true;
	}
	
	public void Start()
    {
		_timer.Paused = false;
		_timer.Start(TimeBetweenUpdates);
    }

	static readonly IReadOnlyList<Vector2I> adjecents = new List<Vector2I>()
	{
		new (-1,-1),
		new (-1,0),
		new (-1,1),
		new (0,-1),
		new (0,1),
		new (1,-1),
		new (1,0),
		new (1,1),

	}.AsReadOnly();
	public void Update()
	{
		HashSet<Vector2I> CheckIngPositions = new(_activepositions);
		HashSet<Vector2I> newActivePositions = new();
		Queue<Vector2I> PositionsQueue = new(_activepositions);
		int activeAdjecents;
		Vector2I currentPos;
		Vector2I currentAdj;
		while (PositionsQueue.Count != 0)
		{
			currentPos = PositionsQueue.Dequeue();
			bool livingcell = _activepositions.Contains(currentPos);
			activeAdjecents = 0;
			foreach (var adj in adjecents)
			{
				currentAdj = currentPos + adj;
				if (_activepositions.Contains(currentAdj))
				{
					activeAdjecents++;
				}
				if (livingcell&&!CheckIngPositions.Contains(currentAdj))
				{
					CheckIngPositions.Add(currentAdj);
					PositionsQueue.Enqueue(currentAdj);
				}

			}
			if (livingcell)
			{
				if (activeAdjecents == 3 || activeAdjecents == 2)
				{
					newActivePositions.Add(currentPos);
				}
			}
			else
			{
				if (activeAdjecents == 3)
				{
					newActivePositions.Add(currentPos);
				}
			}
		}
		_activepositions = newActivePositions;
		_cellViewer.Activate(newActivePositions);
		
		if(newActivePositions.Count>0)
        _timer.Start(TimeBetweenUpdates);
    }
}
