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
    [SerializeField] SelectDictionary SelectDictionary;
    [SerializeField] private float distanceBetweenSquads = 5;
    [SerializeField] private List<GameObject> list;
    private List<Vector3> listOfPoints = new List<Vector3>();
    [SerializeField] LayerMask ground;
    void Update()
    {
        SquadHandleInput();
    }

    void SquadHandleInput()
    {
        var table = SelectDictionary.selectedTable;
        if (Input.GetMouseButtonDown(1) && table.Count != 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                if (hit.transform.gameObject.tag == "Objective")
                {
                    CaptureAPoint(hit);
                }
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    AttackEnemy(hit);
                }
                else
                {
                    JustMoving(hit);
                }
            }
            list.Clear();
            listOfPoints.Clear();
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
                    SelectDictionary.deselectAll();
                }
            }
        }
    }
    void JustMoving(RaycastHit hit)
    {

        foreach (KeyValuePair<int, GameObject> gO in SelectDictionary.selectedTable)
        {
            list.Add(gO.Value);
        }
        MathfHendle(hit.point);

        for (int i = 0; i < SelectDictionary.selectedTable.Count; i++)
        {

            list[i].TryGetComponent<SquadLogic>(out SquadLogic squadLogic);
            if (squadLogic != null)
            {
                squadLogic.MoveToDestination(listOfPoints[i]);
                squadLogic.IsCaptureingAPoint = false;
                squadLogic.IsConnetedToAPoint = false;
            }
            else
                list[i].GetComponent<SingleUniteSquad>().MoveToDestination(listOfPoints[i]);
        }
    }

    void CaptureAPoint(RaycastHit hit)
    {
        foreach (KeyValuePair<int, GameObject> gO in SelectDictionary.selectedTable)
        {
            list.Add(gO.Value);
        }
        DistanceToObjetive(hit.point);
        MathfHendle(hit.point);
        for (int i = 0; i < SelectDictionary.selectedTable.Count; i++)
        {
            if (list != null)
                list[i].GetComponent<SquadLogic>().MoveToDestination(listOfPoints[i]);
        }
    }
    void AttackEnemy(RaycastHit hit)
    {
        foreach (KeyValuePair<int, GameObject> gO in SelectDictionary.selectedTable)
        {
            list.Add(gO.Value);
        }
        MathfHendle(hit.point);

        for (int i = 0; i < SelectDictionary.selectedTable.Count; i++)
        {

            list[i].TryGetComponent<SquadLogic>(out SquadLogic squadLogic);
            if (squadLogic != null)
            {
                squadLogic.MoveToDestination(listOfPoints[i]);
                squadLogic.enemy = hit.collider.gameObject;
            }
            else
                list[i].GetComponent<SingleUniteSquad>().MoveToDestination(listOfPoints[i]);
        }
    }
    void MathfHendle(Vector3 hitPointValue)
    {
        Vector3 pointA = list[0].transform.position;
        Vector3 pointB = hitPointValue;
        float radius = Vector3.Distance(pointB, pointA);
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

    void DistanceToObjetive(Vector3 hitPointValue)
    {
        Dictionary<float, GameObject> Dick = new Dictionary<float, GameObject>();
        foreach (KeyValuePair<int, GameObject> gO in SelectDictionary.selectedTable)
        {
            Vector3 pointA = gO.Value.transform.position;
            Vector3 pointB = hitPointValue;
            float radius = Vector3.Distance(pointB, pointA);
            Dick.Add(radius, gO.Value);
        }
        var sortedDick = from entry in Dick orderby entry.Key ascending select entry;
        var firstEntry = sortedDick.FirstOrDefault();
        firstEntry.Value.GetComponent<SquadLogic>().IsCaptureingAPoint = true;
        GameObject foundObject = list.Find(obj => obj.name == firstEntry.Value.name);
        int foundObjectIndex = list.IndexOf(foundObject);
        GameObject temp = list[0];
        list[0] = foundObject;
        list[foundObjectIndex] = temp;
    }
}

