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
    private void Awake()
    {
        Bind();
        Init();
    }

    void ExitButton()
    {
        mainCanvas.gameObject.SetActive(true);
        keyCanvas.gameObject.SetActive(false);
    }

    void Init()
    {
        exitButton = GetUI<Button>("KeyExitButton");

        exitButton.onClick.AddListener(ExitButton);

        EventSystem.current.SetSelectedGameObject(exitButton.gameObject);
    }
}
