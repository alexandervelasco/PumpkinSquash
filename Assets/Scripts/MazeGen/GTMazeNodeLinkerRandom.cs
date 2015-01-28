using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GTMazeNodeLinkerRandom : IGTMazeNodeNeighborLinker {

	private HashSet<MazeNode> visitedNodes = new HashSet<MazeNode>();

	#region IGTMazeNodeNeighborLinker implementation
	public MazeNode VisitAndLinkNeighborNode (MazeNode mazeNode)
	{
		MazeNode result = null;

		if (!visitedNodes.Contains(mazeNode))
			visitedNodes.Add(mazeNode);

		HashSet<MazeNode> unvisitedNeighborNodes = new HashSet<MazeNode>(mazeNode.Neighbors.Except(visitedNodes));
		if (unvisitedNeighborNodes.Count > 0)
		{
			result = unvisitedNeighborNodes.Random();
			mazeNode.Links.Add(result);
			result.Links.Add(mazeNode);
			visitedNodes.Add(result);
		}

		return result;
	}

	public void ClearVisitedNodes ()
	{
		visitedNodes.Clear();
	}

	#endregion
}
