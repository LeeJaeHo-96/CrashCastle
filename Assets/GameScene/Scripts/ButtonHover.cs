using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject infoText;

    void Awake()
    {
        Init();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoText.SetActive(false);
    }

    void Init()
    {
        infoText = transform.GetChild(1).gameObject;
    }
}
