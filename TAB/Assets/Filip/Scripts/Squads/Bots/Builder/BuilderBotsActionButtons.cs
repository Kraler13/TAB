using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuilderBotsActionButtons : MonoBehaviour
{
    [SerializeField] private ActionButtonsScriptableObj ActionButtonsScriptableObj;
    public GameObject SquadConnectedToButton;
    public Image HPImage;

    private void Start()
    {
        Button thisButton = GetComponent<Button>();
        gameObject.GetComponent<Button>().onClick.AddListener(AllButtons);
    }

    private void AllButtons()
    {
        ActionButtonsScriptableObj.buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Zbuduj";
        ActionButtonsScriptableObj.buttons[0].onClick.RemoveAllListeners();
        ActionButtonsScriptableObj.buttons[0].onClick.AddListener(Build);
    }
    void Build()
    {

    }
}
