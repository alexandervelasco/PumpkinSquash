using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GTMazeNodeSetPickerStack : IGTMazeNodeSetPicker {

	private List<MazeNode> nodeSet = null;

	#region IGTMazeNodeSetPicker implementation
	public void BuildAndSeedNodeSet (IEnumerable<MazeNode> mazeNodes)
	{
		if (nodeSet == null)
			nodeSet = new List<MazeNode>();
		nodeSet.Clear();
		//select random seed if there are multiple
		MazeNode seedNode = mazeNodes.Where(mazeNode => mazeNode.IsSeedNode).Random();
		//if no seed node, choose any node at random
		if (seedNode == null)
			seedNode = mazeNodes.Random();
		AddNode(seedNode);
	}
	public void AddNode (MazeNode mazeNode)
	{
		if (nodeSet != null && !nodeSet.Contains(mazeNode))
			nodeSet.Add(mazeNode);
	}
	public void RemoveNode (MazeNode mazeNode)
	{
		if (nodeSet != null)
			nodeSet.Remove(mazeNode);
	}
	public MazeNode PickNode ()
	{
		MazeNode result = null;

		if (nodeSet != null && nodeSet.Count > 0)
			result = nodeSet[nodeSet.Count-1];

		return result;
	}
	#endregion
}
