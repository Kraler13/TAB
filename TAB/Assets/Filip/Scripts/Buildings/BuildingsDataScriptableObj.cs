using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/BuildingsData")]
public class BuildingsDataScriptableObj : ScriptableObject
{
    public List<BuildingsData> buildingsDatas;
}

[Serializable]
public class BuildingsData
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public Vector2Int Size { get; private set; } = Vector2Int.one;
    [field: SerializeField] public GameObject Prefab { get; private set; }
}
