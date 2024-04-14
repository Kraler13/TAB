using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//KA¯DY SQUADBUTTON MA SWOJ¥ LOGIKÊ
public class LightBotsSquadActionButtons : MonoBehaviour
{
    [SerializeField] private ActionButtonsScriptableObj ActionButtonsScriptableObj;
    private int maxUnites = 9;
    public GameObject SquadConnectedToButton;
    private SquadLogic squadLogic;
    public Image HPImage;
    private int uniteCount = 1;
    private void Start()
    {
        Button thisButton = GetComponent<Button>();
        gameObject.GetComponent<Button>().onClick.AddListener(AllButtons);
        squadLogic = GetComponent<SquadAndUniteButtonHendeler>().SquadConnectedToButton.GetComponent<SquadLogic>();
        foreach (var item in squadLogic.ListOfSpowningPoints)
        {
            squadLogic.ListOfSpowningPointsToChange.Add(item);
        }
    }

    private void AllButtons()
    {
        ActionButtonsScriptableObj.buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = "dodaj";
        ActionButtonsScriptableObj.buttons[0].onClick.RemoveAllListeners();
        ActionButtonsScriptableObj.buttons[0].onClick.AddListener(Multiply);
    }
    void Multiply()
    {
        if (maxUnites > uniteCount)
        {
            squadLogic.resorsSriptableObj.ResorsOne -= 10f;
            var createdButton = Instantiate(squadLogic.Unite, squadLogic.ListOfSpowningPointsToChange[0].transform);
            createdButton.GetComponent<UniteLogic>().pointToFollow = squadLogic.ListOfSpowningPointsToChange[0];
            squadLogic.ListOfSpowningPointsToChange.RemoveAt(0);
            uniteCount++;
        }
    }
}
