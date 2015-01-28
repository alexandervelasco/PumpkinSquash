using System.Collections;
using System.Collections.Generic;

public class MazeConstructorGT : IMazeConstructor {
	private IGTMazeNodeSetPicker mazeNodeSetPicker = null;
	private IGTMazeNodeNeighborLinker mazeNodeNeighborLinker = null;

	private MazeConstructorGT(){}

	public MazeConstructorGT(IGTMazeNodeSetPicker mazeNodeSetPicker, IGTMazeNodeNeighborLinker mazeNodeNeighborLinker)
	{
		this.mazeNodeSetPicker = mazeNodeSetPicker;
		this.mazeNodeNeighborLinker = mazeNodeNeighborLinker;
	}

	#region IMazeConstructor implementation
	public void ConstructMaze (IEnumerable<MazeNode> mazeNodes)
	{
		//Growing Tree Algorithm
		//1. Initialize node set with 1 seed node
		mazeNodeSetPicker.BuildAndSeedNodeSet(mazeNodes);
		//2. Pick a node from the set
		MazeNode currentNode = mazeNodeSetPicker.PickNode();
		while (currentNode != null)
		{
			//3. try to visit & link an unvisited neighbor
			MazeNode linkedNeighbor = mazeNodeNeighborLinker.VisitAndLinkNeighborNode(currentNode);
			//3.1. if a neighbor was visited, add it to the node set
			if (linkedNeighbor != null)
				mazeNodeSetPicker.AddNode(linkedNeighbor);
			//otherwise, remove the previous node from the node set
			else
				mazeNodeSetPicker.RemoveNode(currentNode);
			//4. repeat until the node set is empty
			currentNode = mazeNodeSetPicker.PickNode();
		}

	}
	#endregion
}
