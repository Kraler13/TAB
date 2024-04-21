using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    Dictionary<Vector3Int, PlacementData> placedObjects = new();

    public void AddObejctAt(Vector3Int gridPosition, Vector2Int objectSize, int ID, int placedObjIndex)
    {
        List<Vector3Int> positionToOccupy = CalculatePosition(gridPosition, objectSize);
        PlacementData data = new PlacementData(positionToOccupy, ID, placedObjIndex);
        foreach (var position in positionToOccupy)
        {
            if (placedObjects.ContainsKey(position))
                throw new Exception($"This Position is occupyed {position}");
            placedObjects[position] = data;
        }
    }

    private List<Vector3Int> CalculatePosition(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> returnValues = new();
        for (int x = 0; x < gridPosition.x; x++)
        {
            for (int y = 0; y < gridPosition.y; y++)
            {
                returnValues.Add(gridPosition + new Vector3Int(x, 0, y));
            }
        }
        return returnValues;
    }

    public bool CanPlaceObj(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> positionToOccupy = CalculatePosition(gridPosition, objectSize);
        foreach (var position in positionToOccupy)
        {
            if (placedObjects.ContainsKey(position))
                return false;
        }
        return true;
    }
}

public class PlacementData
{
    public List<Vector3Int> OccupiedPositions;
    public int ID { get; private set; }
    public int PlacedObjectIndex { get; private set; }

    public PlacementData(List<Vector3Int> occupiedPositions, int iD, int placedObjectIndex)
    {
        OccupiedPositions = occupiedPositions;
        ID = iD;
        PlacedObjectIndex = placedObjectIndex;
    }
}
