using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResorsMenager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resorsOneTxt;
    [SerializeField] private TextMeshProUGUI resorsTwoTxt;
    [SerializeField] private ResorsSriptableObj ResorsSriptableObj;
    private bool k = true;
//    void Start()
//    {
////#if UNITY_EDITOR
        
////#endif
//    }
    private void FixedUpdate()
    {
        if (k)
            StartCoroutine(UpdateResorsOnHUD());
    }

    IEnumerator UpdateResorsOnHUD()
    {
        k = false;
        resorsOneTxt.text = ResorsSriptableObj.ResorsOne.ToString();
        resorsTwoTxt.text = ResorsSriptableObj.ResorsTwo.ToString();
        yield return new WaitForSeconds(1);
        k = true;
    }
}
