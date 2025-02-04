using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Setting : BaseUI
{
    [SerializeField] GameObject mainCanvas;

    float sound;
    float lightness;

    string soundKey = "SoundKey";
    string lightnessKey = "LightKey";

    Button soundButton;
    Button lightButton;
    Button closeButton;

    List<Button> buttonList = new List<Button>();

    Color highlightButton;
    GameObject lastButton;

    [SerializeField] Slider soundSlider;
    [SerializeField] Slider lightSlider;
    private void Awake()
    {
        Bind();
        Init();
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(soundButton.gameObject);
        
    }

    private void OnDisable()
    {
        OptionSave();
    }

    void Update()
    {
        NullClick();
        ButtonHighlight();
        SliderControl();
    }

    void SliderControl()
    {
        if (EventSystem.current.currentSelectedGameObject == soundButton.gameObject)
        {
            if(Keyboard.current.aKey.isPressed)
            {
                soundSlider.value -= 0.1f * Time.deltaTime * 3;
            }
            else if(Keyboard.current.dKey.isPressed)
            {
                soundSlider.value += 0.1f * Time.deltaTime * 3;
            }
        }
        else if(EventSystem.current.currentSelectedGameObject == lightButton.gameObject)
        {
            if (Keyboard.current.aKey.isPressed)
            {
                lightSlider.value -= 0.1f * Time.deltaTime * 3;
            }
            else if (Keyboard.current.dKey.isPressed)
            {
                lightSlider.value += 0.1f * Time.deltaTime * 3;
            }
        }
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
            {
                lastButton = soundButton.gameObject;
            }

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

    // 옵션 세팅 수치 세이브
    public void OptionSave()
    {
        sound = soundSlider.value;
        lightness = lightSlider.value;

        PlayerPrefs.SetFloat(soundKey, sound);
        PlayerPrefs.SetFloat(lightnessKey, lightness);
        PlayerPrefs.Save();
        Debug.Log($"세이브 : 소리는 {sound}, 밝기는 {lightness}");
        Debug.Log("옵션 세팅 저장");
    }

    // 옵션 세팅 수치 로드
    public void OptionLoad()
    {
        if (!PlayerPrefs.HasKey(soundKey) || !PlayerPrefs.HasKey(lightnessKey))
        {
            Debug.Log("기본 세팅 완료");
            OptionResetAll();
            return;
        }
        Debug.Log("옵션 세팅 불러오기");
        Debug.Log($"로드직전 : 소리는 {soundKey}, 밝기는 {lightnessKey}");

        sound = PlayerPrefs.GetFloat(soundKey);
        soundSlider.value = sound;
        lightness = PlayerPrefs.GetFloat(lightnessKey);
        lightSlider.value = lightness;
        Debug.Log($"로드 : 소리는 {sound}, 밝기는 {lightness}");
    }

    void OptionResetAll()
    {
        sound = 1f;
        lightness = 1f;
    }

    void CloseButton()
    {
        gameObject.SetActive(false);
        mainCanvas.SetActive(true);
        //액션맵 수정하자
    }

    void Init()
    {
        buttonList.Add(soundButton = GetUI<Button>("SoundButton"));
        buttonList.Add(lightButton = GetUI<Button>("LightButton"));
        buttonList.Add(closeButton = GetUI<Button>("CloseButton"));

        closeButton.onClick.AddListener(CloseButton);

    }
}
