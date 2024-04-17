using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HUDMenager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject selection;
    public GameObject OneRowButtons;
    public GameObject TwoRowButtons;
    private GameObject k;
    private bool isOverHUD;
    void Start()
    {
        OneRowButtons.SetActive(false);
        TwoRowButtons.SetActive(false);
    }
    void Update()
    {
        if (k != null)
        {
            selection.SetActive(false);
            isOverHUD = true;
        }
        else if (k == null && isOverHUD)
        {
            selection.SetActive(true);
            isOverHUD = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        k = eventData.pointerEnter;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        k = null;
    }
}
