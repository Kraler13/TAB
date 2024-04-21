using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputForGridSystem : MonoBehaviour
{
    public event Action OnClicked, OnExit;
    [SerializeField] private Camera myCamera;
    [SerializeField] private LayerMask placementLayermask;

    private Vector3 lastPosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnClicked?.Invoke();
        if (Input.GetKeyDown(KeyCode.Escape))
            OnExit?.Invoke();        
    }
    public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();
    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = myCamera.nearClipPlane;
        Ray ray = myCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayermask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }
}
