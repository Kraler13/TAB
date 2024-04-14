using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddingButtonsToScriptableObj : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private ActionButtonsScriptableObj actionButtonsScriptableObj;
    private void Awake()
    {
        actionButtonsScriptableObj.buttons.Clear();
        foreach (var button in buttons)
        {
            actionButtonsScriptableObj.buttons.Add(button);
        }
    }
}
