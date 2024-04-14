using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Poruszanie oraz two¿enie pojedyñczych jednostek
public class UniteLogic : MonoBehaviour
{
    private SquadLogic squadLogic;
    private NavMeshAgent navMeshAgent;
    public GameObject pointToFollow;

    void Start()
    {
        squadLogic = GetComponentInParent<SquadLogic>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = squadLogic.SquadSpeed;        
        pointToFollow = squadLogic.ListOfSpowningPoints[0];
        squadLogic.ListOfSpowningPoints.RemoveAt(0);
    }

    void Update()
    {
        navMeshAgent.SetDestination(pointToFollow.transform.position);
    }
}
