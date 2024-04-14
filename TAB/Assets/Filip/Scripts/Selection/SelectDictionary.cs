using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class SelectDictionary : MonoBehaviour
{
    public Dictionary<int, GameObject> selectedTable = new Dictionary<int, GameObject>();
    public void addSelected(GameObject go)
    {
        int id = go.GetInstanceID();
        bool noneAreSquads = selectedTable.Values.All(obj => obj.CompareTag("Squad"));
        if (!(selectedTable.ContainsKey(id)) && (go.gameObject.tag == "Squad" || go.gameObject.tag == "Builder"))
        {

            selectedTable.Add(id, go);
            go.AddComponent<SelectionHelper>();

        }
    }

    public void deselect(int id)
    {
        Destroy(selectedTable[id].GetComponent<SelectionHelper>());
        selectedTable.Remove(id);
    }

    public void deselectAll()
    {
        foreach (KeyValuePair<int, GameObject> pair in selectedTable)
        {
            if (pair.Value != null)
            {
                Destroy(selectedTable[pair.Key].GetComponent<SelectionHelper>());
            }
        }
        selectedTable.Clear();
    }
}
