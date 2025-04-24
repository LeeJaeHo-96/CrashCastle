using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class MainUI : BaseUI
{
    [Inject]
    FirebaseManager firebaseManager;


    Button gameButton;
    Button keyButton;
    Button optionButton;
    Button rankingButton;
    Button exitButton;

    List<Button> buttonList = new List<Button>();
    List<UnityAction> actionList = new List<UnityAction>();

    GameObject lastButton;

    Color highlightButton;

    [SerializeField] Canvas keyCanvas;
    [SerializeField] Canvas rankingCanvas;
    [SerializeField] Canvas optionCanvas;
    [SerializeField] Canvas mainCanvas;

    [SerializeField] Setting setting;
    private void Awake()
    {
        Bind();
        Init();
    }
    private void Start()
    {
        setting.OptionLoad();
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
        //�÷��� ��ư 1����
        //���� 0.1�� �ٲ�
        //2 3
        //�÷��� ���õ� ��ư ����
        //�÷� ���� 1�� �ٲ�
        //���õ� ��ư�� �÷� ����

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

    void OptionButton()
    {
        optionCanvas.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(false);
    }

    void RankingButton()
    {
        rankingCanvas.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(false);
    }

    void ExitButton()
    {
#if UNITY_EDITOR
        //Comment : ����Ƽ �����ͻ󿡼� ����
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //Comment : ���� �󿡼� ����
        Application.Quit();
#endif
    }

    void Init()
    {
        setting.GetComponent<Setting>().OptionLoad();


        gameButton = GetUI<Button>("gameButton");
        keyButton = GetUI<Button>("keyButton");
        optionButton = GetUI<Button>("optionButton");
        rankingButton = GetUI<Button>("rankingButton");
        exitButton = GetUI<Button>("exitButton");

        buttonList.Add(gameButton);
        buttonList.Add(keyButton);
        buttonList.Add(optionButton);
        buttonList.Add(rankingButton);
        buttonList.Add(exitButton);

        actionList.Add(() => {GameButton();});
        actionList.Add(() => {KeyButton();});
        actionList.Add(() => {OptionButton();});
        actionList.Add(() => {RankingButton();});
        actionList.Add(() => {ExitButton();});

        for (int i = 0; i < actionList.Count; i++)
        {
            buttonList[i].onClick.AddListener(actionList[i]);
        }

    }
}
