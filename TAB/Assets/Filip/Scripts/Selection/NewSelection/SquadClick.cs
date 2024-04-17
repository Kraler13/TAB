using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadClick : MonoBehaviour
{
    private Camera myCamera;

    public LayerMask Clickable;
    public LayerMask Ground;
    // Start is called before the first frame update
    void Start()
    {
        myCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    SquadSelection.Instance.ShiftClickSelect(hit.collider.gameObject);
                }
                else
                {
                    SquadSelection.Instance.ClickSelect(hit.collider.gameObject);
                }
            }
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift)) 
                {
                    SquadSelection.Instance.DeselectAll();
                }
            }
        }
    }
}
