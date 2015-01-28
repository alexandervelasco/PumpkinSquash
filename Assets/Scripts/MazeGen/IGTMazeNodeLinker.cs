public interface IGTMazeNodeNeighborLinker {
	MazeNode VisitAndLinkNeighborNode(MazeNode mazeNode);
	void ClearVisitedNodes();
}
