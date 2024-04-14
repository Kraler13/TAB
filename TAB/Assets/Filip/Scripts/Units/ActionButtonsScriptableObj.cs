using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Custom/ActionButtons")]

public class ActionButtonsScriptableObj : ScriptableObject
{
    public List<Button> buttons;
}
