using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Option : BaseUI
{
    List<Button> buttons = new List<Button>();

    [SerializeField] GameObject pause;
    GameObject lastButton;
    Color highlightButton;
    private void Awake()
    {
        Bind();
        Init();
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
    }

    private void OnDisable()
    {
        pause.SetActive(true);
    }

    void Update()
    {
        NullClick();
        ButtonHighlight();
    }

    void NullClick()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            lastButton = EventSystem.current.currentSelectedGameObject;
        }

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastButton.gameObject);
        }
    }

    void ButtonHighlight()
    {
        //컬러에 버튼 1넣음
        //투명도 0.1로 바꿈
        //2 3
        //컬러에 선택된 버튼 넣음
        //컬러 투명도 1로 바꿈
        //선택된 버튼에 컬러 넣음

        for (int i = 0; i < buttons.Count; i++)
        {
            highlightButton = buttons[i].GetComponent<Image>().color;
            highlightButton.a = 0.1f;
            buttons[i].GetComponent<Image>().color = highlightButton;
        }

        highlightButton = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color;
        highlightButton.a = 1f;
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = highlightButton;
    }

    void CloseButton()
    {
        gameObject.SetActive(false);
        //액션맵 수정하자
    }

    void Init()
    {
        buttons.Add(GetUI<Button>("Sound"));
        buttons.Add(GetUI<Button>("Light"));
        buttons.Add(GetUI<Button>("Close"));

        buttons[2].onClick.AddListener(CloseButton);
    }
}
