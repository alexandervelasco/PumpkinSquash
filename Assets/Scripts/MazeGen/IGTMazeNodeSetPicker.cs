using System.Collections.Generic;

public interface IGTMazeNodeSetPicker {
	void BuildAndSeedNodeSet(IEnumerable<MazeNode> mazeNodes);
	void AddNode(MazeNode mazeNode);
	void RemoveNode(MazeNode mazeNode);
	MazeNode PickNode();
}
