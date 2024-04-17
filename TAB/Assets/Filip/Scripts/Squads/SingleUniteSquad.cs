using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SingleUniteSquad : MonoBehaviour
{
    public Button SquadButtonPrefab;
    public Sprite SquadImage;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private float uniteSpeed = 10f;
    [SerializeField] private float RotationSpeed = 360f;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = uniteSpeed;
        navMeshAgent.angularSpeed = RotationSpeed;
    }

    public void MoveToDestination(Vector3 destination)
    {
        Vector3 direction = destination - transform.position;
        navMeshAgent.SetDestination(destination);
    }
}
