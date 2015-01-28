using System.Collections.Generic;

public interface IMazeInitializer {
	void InitializeMaze(IEnumerable<MazeNode> mazeNodes);
}
