using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {

	public IntVector2 size;

	public MazeCell cellPrefab;

	public float generationStepDelay;

	public MazePassage passagePrefab;
	public MazeWall wallPrefab;
    public MazeWallDoor wallDoorPrefab;

	private MazeCell[,] cells;

    public Material[] wallColors;
    public Material[] floorColors;

	public IntVector2 RandomCoordinates {
		get {
			return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
		}
	}

	public bool ContainsCoordinates (IntVector2 coordinate) {
		return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
	}

	public MazeCell GetCell (IntVector2 coordinates) {
		return cells[coordinates.x, coordinates.z];
	}

	public void Generate () {
		cells = new MazeCell[size.x, size.z];
		List<MazeCell> activeCells = new List<MazeCell>();
		DoFirstGenerationStep(activeCells);
		while (activeCells.Count > 0) {
			DoNextGenerationStep(activeCells);
		}
	}

	private void DoFirstGenerationStep (List<MazeCell> activeCells) {
		activeCells.Add(CreateCell(RandomCoordinates));
	}

	private void DoNextGenerationStep (List<MazeCell> activeCells) {
		int currentIndex = activeCells.Count - 1;
		MazeCell currentCell = activeCells[currentIndex];
		if (currentCell.IsFullyInitialized) {
			activeCells.RemoveAt(currentIndex);
			return;
		}
		MazeDirection direction = currentCell.RandomUninitializedDirection;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
		if (ContainsCoordinates(coordinates)) {
			MazeCell neighbor = GetCell(coordinates);
			if (neighbor == null) {
				neighbor = CreateCell(coordinates);
				CreatePassage(currentCell, neighbor, direction);
				activeCells.Add(neighbor);
			}
			else {
                CreateWall(currentCell, neighbor, direction);
			}
		}
		else {
            if (coordinates.x == -1 & coordinates.z == 1) {
                CreateWallDoor(currentCell, null, direction);
                Debug.Log("Door Created");
            } else
                CreateWall(currentCell, null, direction);
		}
	}

	private MazeCell CreateCell (IntVector2 coordinates) {
		MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
		cells[coordinates.x, coordinates.z] = newCell;
		newCell.coordinates = coordinates;
		newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
        if (coordinates.x == 0 && coordinates.z == 0) {
            newCell.GetComponentInChildren<Renderer>().material = floorColors[0];
        } else if (coordinates.x == size.x - 1 && coordinates.z == size.z - 1){
            newCell.GetComponentInChildren<Renderer>().material = floorColors[1];
        }
        return newCell;
	}

	private void CreatePassage (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazePassage passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(cell, otherCell, direction);
		passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(otherCell, cell, direction.GetOpposite());
	}

	private void CreateWall (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazeWall wall = Instantiate(wallPrefab) as MazeWall;
		wall.Initialize(cell, otherCell, direction);
        ColorWall(wall, direction);
		if (otherCell != null) {
			wall = Instantiate(wallPrefab) as MazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
            ColorWall(wall, direction.GetOpposite());
        }
	}

    private void CreateWallDoor(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        MazeWallDoor wall = Instantiate(wallDoorPrefab) as MazeWallDoor;
        wall.Initialize(cell, otherCell, direction);
        ColorWallDoor(wall, direction);
        if (otherCell != null) {
            wall = Instantiate(wallDoorPrefab) as MazeWallDoor;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
            ColorWallDoor(wall, direction.GetOpposite());
        }
    }

    private void ColorWall(MazeWall wall, MazeDirection direction) {
        if (direction == MazeDirection.North)
            wall.GetComponentInChildren<Renderer>().material = wallColors[0];
        else if (direction == MazeDirection.East)
            wall.GetComponentInChildren<Renderer>().material = wallColors[1];
        else if (direction == MazeDirection.South)
            wall.GetComponentInChildren<Renderer>().material = wallColors[2];
        else if (direction == MazeDirection.West)
            wall.GetComponentInChildren<Renderer>().material = wallColors[3];
    }

    private void ColorWallDoor(MazeWallDoor wall, MazeDirection direction) {
        if (direction == MazeDirection.North)
            wall.GetComponentInChildren<Renderer>().material = wallColors[0];
        else if (direction == MazeDirection.East)
            wall.GetComponentInChildren<Renderer>().material = wallColors[1];
        else if (direction == MazeDirection.South)
            wall.GetComponentInChildren<Renderer>().material = wallColors[2];
        else if (direction == MazeDirection.West)
            wall.GetComponentInChildren<Renderer>().material = wallColors[3];
    }
}