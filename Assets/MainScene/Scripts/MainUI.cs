using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : BaseUI
{
    Button gameButton;
    Button keyButton;
    Button exitButton;

    List<Button> buttonList = new List<Button>();

    GameObject lastButton;

    Color highlightButton;

    [SerializeField] Canvas keyCanvas;
    [SerializeField] Canvas mainCanvas;
    private void Awake()
    {
        Bind();
        Init();
    }
    void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameButton.gameObject);
    }

    void Update()
    {
        NullClick();
        ButtonHighlight();
    }

    void NullClick()
    {
        if(EventSystem.current.currentSelectedGameObject != null)
        {
            lastButton = EventSystem.current.currentSelectedGameObject;
        }

        if(EventSystem.current.currentSelectedGameObject == null)
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

        for (int i = 0; i < buttonList.Count; i++)
        {
            highlightButton = buttonList[i].GetComponent<Image>().color;
            highlightButton.a = 0.1f;
            buttonList[i].GetComponent<Image>().color = highlightButton;
        }

        highlightButton = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color;
        highlightButton.a = 1f;
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = highlightButton;
    }

    void GameButton()
    {
        SceneManager.LoadScene(Scene.GameScene);
    }

    void KeyButton()
    {
        keyCanvas.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(false);
    }

    void ExitButton()
    {
#if UNITY_EDITOR
        //Comment : 유니티 에디터상에서 종료
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //Comment : 빌드 상에서 종료
        Application.Quit();
#endif
    }

    void Init()
    {
        gameButton = GetUI<Button>("gameButton");
        keyButton = GetUI<Button>("keyButton");
        exitButton = GetUI<Button>("exitButton");

        buttonList.Add(gameButton);
        buttonList.Add(keyButton);
        buttonList.Add(exitButton);

        gameButton.onClick.AddListener(GameButton);
        keyButton.onClick.AddListener(KeyButton);
        exitButton.onClick.AddListener(ExitButton);

    }
}
