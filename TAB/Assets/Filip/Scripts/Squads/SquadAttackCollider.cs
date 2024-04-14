using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadAttackCollider : MonoBehaviour
{
    [SerializeField] private SquadLogic SquadLogic;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("atack");
            SquadLogic.IsAttacking = true;
            SquadLogic.InRangeAttack = true;
            SquadLogic.ListOfEnemys.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            SquadLogic.InRangeAttack = false;
            SquadLogic.ListOfEnemys.Remove(other.gameObject);
        }
    }
}
