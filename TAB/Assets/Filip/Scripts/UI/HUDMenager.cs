using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HUDMenager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject OneRowButtons;
    public GameObject TwoRowButtons;

    [SerializeField] private List<GameObject> heroObj;
    [SerializeField] private InputMenager InputMenager;
    [SerializeField] private SelectObj SelectObj;
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
            InputMenager.enabled = false;
            SelectObj.enabled = false;
            isOverHUD = true;
        }
        else if (k == null && isOverHUD)
        {
            InputMenager.enabled = true;
            SelectObj.enabled = true;
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
