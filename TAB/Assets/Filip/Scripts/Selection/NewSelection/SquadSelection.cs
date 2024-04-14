using System.Collections.Generic;
using UnityEngine;

public class SquadSelection : MonoBehaviour
{
    public List<GameObject> SquadList = new List<GameObject>();
    public List<GameObject> SquadsSelected = new List<GameObject>();

    private static SquadSelection instance;
    public static SquadSelection Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void ClickSelect(GameObject uniteToAdd)
    {

    }
    public void ShiftClickSelect(GameObject uniteToAdd)
    {

    }
    public void DragSelect(GameObject uniteToAdd)
    {

    }
    public void DeselectAll()
    {

    }
    public void Deselect(GameObject uniteToDeselect)
    {

    }
}
