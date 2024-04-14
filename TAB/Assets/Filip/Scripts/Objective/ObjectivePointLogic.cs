using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePointLogic : MonoBehaviour
{
    public bool IsObjectiveCaptured = false;
    public float PercentOfCapture = 0;
    private SquadLogic squadCaptureing;

    private void Update()
    {
        if (squadCaptureing == null)
        {
            if (PercentOfCapture > 0)
            {
                PercentOfCapture -= 2f;
            }            
        }
        //Debug.Log(PercentOfCapture);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Squad")
        {
            squadCaptureing = other.GetComponent<SquadLogic>();
            squadCaptureing.objective = this;
            if (other.GetComponent<SquadLogic>().IsCaptureingAPoint)
            {
                other.GetComponent<SquadLogic>().IsConnetedToAPoint = true;
            }
        }
    }
}
