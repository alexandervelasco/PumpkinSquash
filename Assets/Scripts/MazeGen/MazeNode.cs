using System.Collections;
using System.Collections.Generic;

public class MazeNode {
	private HashSet<MazeNode> neighbors = null;
	private HashSet<MazeNode> links = null;
	private bool isSeedNode = false;

	public HashSet<MazeNode> Neighbors {
		get {
			if (neighbors == null)
				neighbors = new HashSet<MazeNode>();
			return neighbors;
		}
	}

	public HashSet<MazeNode> Links {
		get {
			if (links == null)
				links = new HashSet<MazeNode>();
			return links;
		}
	}

	public bool IsSeedNode {
		get {
			return isSeedNode;
		}
		set {
			isSeedNode = value;
		}
	}

	public void Clear()
	{
		Links.Clear();
		Neighbors.Clear();
		IsSeedNode = false;
	}
}
