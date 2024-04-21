using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mousieIndicator;
    [SerializeField] private GameObject cellIndicator;
    [SerializeField] private InputForGridSystem inputForGridSystem;
    [SerializeField] private Grid grid;
    [SerializeField] private BuildingsDataScriptableObj buildingsDataScriptableObj;
    [SerializeField] private GameObject gridVisual;
    private int selectedObjIndex = -1;
    private GridData groundData;
    private GridData buildingData;
    private Renderer previewRenderer;
    private List<GameObject> placedBuildings = new List<GameObject>();

    private void Start()
    {
        StopPlacement();
        groundData = new GridData();
        buildingData = new GridData();
        previewRenderer = cellIndicator.GetComponent<Renderer>();
    }

    void Update()
    {
        if (selectedObjIndex < 0)
            return;
        Vector3 mousePositio = inputForGridSystem.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePositio);
        bool placmentValidyty = CheckPlacementValidyty(gridPosition, selectedObjIndex);
        previewRenderer.material.color = placmentValidyty ? Color.white : Color.red;
        mousieIndicator.transform.position = mousePositio;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        selectedObjIndex = buildingsDataScriptableObj.buildingsDatas.FindIndex(data => data.ID == ID);
        if (selectedObjIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }
        gridVisual.SetActive(true);
        cellIndicator.SetActive(true);
        inputForGridSystem.OnClicked += PlaceStructure;
        inputForGridSystem.OnExit += StopPlacement;
    }

    private void StopPlacement()
    {
        selectedObjIndex = -1;
        gridVisual.SetActive(false);
        cellIndicator.SetActive(false);
        inputForGridSystem.OnClicked -= PlaceStructure;
        inputForGridSystem.OnExit -= StopPlacement;
    }

    private void PlaceStructure()
    {
        if(inputForGridSystem.IsPointerOverUI())
        {
            return;
        }
        Vector3 mousePositio = inputForGridSystem.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePositio);
        bool placmentValidyty = CheckPlacementValidyty(gridPosition, selectedObjIndex);
        if (!placmentValidyty)
            return;
        GameObject newBuilding = Instantiate(buildingsDataScriptableObj.buildingsDatas[selectedObjIndex].Prefab);
        newBuilding.transform.position = grid.CellToWorld(gridPosition);
        placedBuildings.Add(newBuilding);
        GridData selectedData = buildingsDataScriptableObj.buildingsDatas[selectedObjIndex].ID == 0 ? groundData : buildingData;
        selectedData.AddObejctAt(gridPosition, buildingsDataScriptableObj.buildingsDatas[selectedObjIndex].Size,
            buildingsDataScriptableObj.buildingsDatas[selectedObjIndex].ID,
            placedBuildings.Count - 1);
    }

    private bool CheckPlacementValidyty(Vector3Int gridPosition, int selectedObjIndex)
    {
        GridData selectedData = buildingsDataScriptableObj.buildingsDatas[selectedObjIndex].ID == 0 ? groundData : buildingData;

        return selectedData.CanPlaceObj(gridPosition, buildingsDataScriptableObj.buildingsDatas[selectedObjIndex].Size);
    }
}
