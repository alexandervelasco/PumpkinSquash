using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using PathologicalGames;

public class TiledMazeGenerator2DGrid : EventReceiverBehavior {

	//enums
	public enum MazeConstructorType {StackPickRandomVisitGT};

	//serialized data
	public GameObject tile;
	public string spawnPoolName;
	public int width, length, gapSize, tileScale;
	public int[] seedNodeIndices;
	public MazeConstructorType mazeConstructorType;

	private Maze maze;
	private SpawnPool terrainPool;

	// Use this for initialization
	public void Start () {
		GenerateMaze();
		SpawnTiles ();
	}
	
	// Update is called once per frame
	public void Update () {
	
	}

	#region implemented abstract members of GameBehavior

	public override void ReceiveEvent (string eventName, object args, object sender)
	{
	}

	#endregion

	private void GenerateMaze()
	{
		IMazeInitializer mazeInitializer = new MazeInitializer2DGrid(width);
		IMazeConstructor mazeConstructor = null;

		switch (mazeConstructorType)
		{
		case MazeConstructorType.StackPickRandomVisitGT : {mazeConstructor = new MazeConstructorGT(new GTMazeNodeSetPickerStack(), new GTMazeNodeLinkerRandom()); break;}
		}

		maze = new Maze(mazeInitializer, mazeConstructor);
		maze.Init(width * length);
		foreach (int seedNodeIndex in seedNodeIndices)
		{
			if (seedNodeIndex < maze.MazeNodes.Count)
				maze.MazeNodes[seedNodeIndex].IsSeedNode = true;
		}
		maze.Build();
	}

	private void SpawnTiles()
	{
		Transform tileTransform = tile.transform;
		terrainPool = PoolManager.Pools[spawnPoolName];
		HashSet<MazeNode> spawnedLinks = new HashSet<MazeNode>();
		float fGapSize = (float)(gapSize+1);
		Vector3 offset = transform.position;
		foreach (MazeNode mazeNode in maze.MazeNodes)
		{
			float nodeX = maze.MazeNodes.IndexOf(mazeNode)%width;
			float nodeZ = maze.MazeNodes.IndexOf(mazeNode)/width;
			//Spawn nodes
			for (int i = 0; i < tileScale; i++)
				for (int j = 0; j < tileScale; j++)
				{
				Vector3 tilePosition = new Vector3((nodeX*(gapSize+1)*tileScale)+i,0,(nodeZ*(gapSize+1)*tileScale)+j);
				terrainPool.Spawn(tileTransform,tilePosition+offset,Quaternion.identity);
				}
			//Spawn links
			foreach (MazeNode linkNode in mazeNode.Links.Except(spawnedLinks))
			{
				float linkX = maze.MazeNodes.IndexOf(linkNode)%width;
				float linkZ = maze.MazeNodes.IndexOf(linkNode)/width;
				for (int i = 1; i < gapSize+1; i++)
				{
					float lerpX = Mathf.Lerp(nodeX, linkX, i/fGapSize);
					float lerpZ = Mathf.Lerp(nodeZ, linkZ, i/fGapSize);
					for (int j = 0; j < tileScale; j++)
						for (int k = 0; k < tileScale; k++)
						{
						Vector3 tilePosition = new Vector3((lerpX*(gapSize+1)*tileScale)+j,0,(lerpZ*(gapSize+1)*tileScale)+k);
						terrainPool.Spawn(tileTransform,tilePosition+offset,Quaternion.identity);
						}
				}
			}
			spawnedLinks.Add(mazeNode);
		}
	}
}
