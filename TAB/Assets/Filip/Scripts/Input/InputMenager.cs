using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using System.Linq;
public class InputMenager : MonoBehaviour
{
    [SerializeField] SquadSelection selectedSquads;
    [SerializeField] private List<Vector3> listOfPoints = new List<Vector3>();
    [SerializeField] LayerMask ground;

    private float distanceBetweenSquads = 5;

    void Update()
    {
        SquadHandleInput();
        BuildingSelection();
    }

    void SquadHandleInput()
    {
        var table = selectedSquads.SquadsSelected;
        if (Input.GetMouseButtonDown(1) && table.Count != 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {                
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    AttackEnemy(hit);
                }
                else
                {
                    JustMoving(hit);
                }
            }
        }
    }

    void BuildingSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                if (hit.transform.gameObject.tag == "Objective")
                {
                    selectedSquads.DeselectAll();
                }
            }
        }
    }
    void JustMoving(RaycastHit hit)
    {
        MathfHendle(hit.point);

        for (int i = 0; i < selectedSquads.SquadsSelected.Count; i++)
        {
            selectedSquads.SquadsSelected[i].TryGetComponent<SquadLogic>(out SquadLogic squadLogic);
            if (squadLogic != null)
            {
                squadLogic.MoveToDestination(listOfPoints[i]);
            }
            else
                selectedSquads.SquadsSelected[i].GetComponent<SingleUniteSquad>().MoveToDestination(listOfPoints[i]);
        }
    }

    
    void AttackEnemy(RaycastHit hit)
    {
        
        MathfHendle(hit.point);

        for (int i = 0; i < selectedSquads.SquadsSelected.Count; i++)
        {

            selectedSquads.SquadsSelected[i].TryGetComponent<SquadLogic>(out SquadLogic squadLogic);
            if (squadLogic != null)
            {
                squadLogic.MoveToDestination(listOfPoints[i]);
                squadLogic.enemy = hit.collider.gameObject;
            }
            else
                selectedSquads.SquadsSelected[i].GetComponent<SingleUniteSquad>().MoveToDestination(listOfPoints[i]);
        }
    }
    void MathfHendle(Vector3 hitPointValue)
    {
        listOfPoints.Clear();
        Vector3 pointA = selectedSquads.SquadsSelected[0].transform.position;
        Vector3 pointB = hitPointValue;
        Vector3 vectorAB = pointB - pointA;
        Vector3 normalizedAB = vectorAB.normalized;
        Vector3 offset = distanceBetweenSquads * normalizedAB;

        listOfPoints.Add(pointB);
        listOfPoints.Add(pointB + distanceBetweenSquads * new Vector3(normalizedAB.x, 0, -normalizedAB.z));
        listOfPoints.Add(pointB - distanceBetweenSquads * new Vector3(normalizedAB.x, 0, -normalizedAB.z));
        listOfPoints.Add(pointB - offset);
        listOfPoints.Add(pointB - offset + distanceBetweenSquads * new Vector3(normalizedAB.x, 0, -normalizedAB.z));
        listOfPoints.Add(pointB - offset - distanceBetweenSquads * new Vector3(normalizedAB.x, 0, -normalizedAB.z));
        listOfPoints.Add(pointB - offset * 2);
        listOfPoints.Add(pointB - offset * 2 + distanceBetweenSquads * new Vector3(normalizedAB.x, 0, -normalizedAB.z));
        listOfPoints.Add(pointB - offset * 2 - distanceBetweenSquads * new Vector3(normalizedAB.x, 0, -normalizedAB.z));
    } 
}

