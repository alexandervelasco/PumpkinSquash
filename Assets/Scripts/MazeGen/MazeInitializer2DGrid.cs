using System.Collections;
using System.Collections.Generic;

public class MazeInitializer2DGrid : IMazeInitializer {

	private int rowLength = 0;

	private MazeInitializer2DGrid(){}

	public MazeInitializer2DGrid(int rowLength)
	{
		this.rowLength = rowLength;
	}

	#region IMazeInitializer implementation
	public void InitializeMaze (IEnumerable<MazeNode> mazeNodes)
	{
		if (rowLength > 0)
		{
			int i = 0;
			List<MazeNode> currentRow = null, previousRow = null;
			MazeNode previousNode = null;
			foreach (MazeNode mazeNode in mazeNodes)
			{
				mazeNode.Links.Clear();
				mazeNode.Neighbors.Clear();
				if (i%rowLength == 0)
				{
					previousRow = currentRow;
					currentRow = new List<MazeNode>();
					previousNode = null;
				}
				else
					previousNode = currentRow[(i%rowLength)-1];
				currentRow.Add(mazeNode);
				if (previousNode != null)
					SetAsNeighbors(mazeNode, previousNode);
				if (previousRow != null)
					SetAsNeighbors(mazeNode, previousRow[i%rowLength]);
				i++;
			}
		}
	}
	#endregion

	private void SetAsNeighbors(MazeNode mazeNode1, MazeNode mazeNode2)
	{
		mazeNode1.Neighbors.Add(mazeNode2);
		mazeNode2.Neighbors.Add(mazeNode1);
	}
}
