using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquadSelection : MonoBehaviour
{
    [SerializeField] private HUDMenager hudMenager;

    public List<GameObject> SquadList = new List<GameObject>();
    public List<GameObject> SquadsSelected = new List<GameObject>();
    public List<Button> buttonList = new List<Button>();
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
        DeselectAll();
        DeleteButton();
        SquadsSelected.Add(uniteToAdd);
        AddButton(uniteToAdd);
    }
    public void ShiftClickSelect(GameObject uniteToAdd)
    {
        if (!SquadsSelected.Contains(uniteToAdd))
        {
            SquadsSelected.Add(uniteToAdd);
            //dodaæ button
            AddButton(uniteToAdd);
        }
        else
        {
            //usun¹æ button
            DeleteButton();
            SquadsSelected.Remove(uniteToAdd);
        }
    }
    public void DragSelect(GameObject uniteToAdd)
    {
        if (!SquadsSelected.Contains(uniteToAdd))
        {
            SquadsSelected.Add(uniteToAdd);
            //dodaæ button
            AddButton(uniteToAdd);
        }
    }
    public void DeselectAll()
    {
        DeleteButton();
        SquadsSelected.Clear();
    }
    public void Deselect(GameObject uniteToDeselect)
    {

    }
    void DeleteButton()
    {
        foreach (var item in buttonList)
        {
            Destroy(item.gameObject);
        }
        buttonList.Clear();
    }
    void AddButton(GameObject uniteToAdd)
    {
        if (SquadsSelected.Count > 0)
        {         
            hudMenager.OneRowButtons.SetActive(true);
            hudMenager.TwoRowButtons.SetActive(false);
            var button = Instantiate(uniteToAdd.GetComponent<SquadAndUniteButtonHendeler>().SquadButtonPrefab, hudMenager.OneRowButtons.transform);
            button.GetComponent<SquadAndUniteButtonHendeler>().SquadConnectedToButton = uniteToAdd;
            buttonList.Add(button);
        }
    }
}
