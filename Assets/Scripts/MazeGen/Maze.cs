using System.Collections;
using System.Collections.Generic;

public class Maze {
	private List<MazeNode> mazeNodes = null;
	private IMazeInitializer mazeInitializer = null;
	private IMazeConstructor mazeConstructor = null;
	private bool isBuilt = false;

	public List<MazeNode> MazeNodes 
	{
		get {
			if (mazeNodes == null)
				mazeNodes = new List<MazeNode>();
			return mazeNodes;
		}
	}

	public bool IsBuilt {
		get {
			return isBuilt;
		}
	}

	public Maze(IMazeInitializer mazeInitializer, IMazeConstructor mazeConstructor)
	{
		this.mazeInitializer = mazeInitializer;
		this.mazeConstructor = mazeConstructor;
	}

	public void Init(int size)
	{
		for (int i = 0; i < size; i++)
			MazeNodes.Add(new MazeNode());
	}

	public void Build()
	{
		Clear();
		mazeInitializer.InitializeMaze(MazeNodes);
		mazeConstructor.ConstructMaze(MazeNodes);
		this.isBuilt = true;
	}

	public void Clear()
	{
		foreach (MazeNode mazeNode in MazeNodes)
			mazeNode.Clear();
		this.isBuilt = false;
	}
}
