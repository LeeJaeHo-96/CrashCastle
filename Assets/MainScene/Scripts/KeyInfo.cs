using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyInfo : BaseUI
{
    Button exitButton;

    [SerializeField] Canvas keyCanvas;
    [SerializeField] Canvas mainCanvas;

    GameObject lastButton;
    private void Awake()
    {
        Bind();
        Init();
    }
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(exitButton.gameObject);
    }

    private void Update()
    {
        NullClick();
    }

    void ExitButton()
    {
        mainCanvas.gameObject.SetActive(true);
        keyCanvas.gameObject.SetActive(false);
    }

    void NullClick()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            lastButton = EventSystem.current.currentSelectedGameObject;
        }

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (lastButton == null)
                EventSystem.current.SetSelectedGameObject(exitButton.gameObject);
            else
            EventSystem.current.SetSelectedGameObject(lastButton.gameObject);
        }
    }

    void Init()
    {
        exitButton = GetUI<Button>("KeyExitButton");

        exitButton.onClick.AddListener(ExitButton);
    }
}
