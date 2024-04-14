
using UnityEngine;

public class Squads : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SquadSelection.Instance.SquadList.Add(this.gameObject);
    }

    private void OnDestroy()
    {
        SquadSelection.Instance.SquadList.Remove(this.gameObject);
    }
}
