using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : BaseUI
{
    Button gameButton;
    Button keyButton;
    Button exitButton;
    private void Awake()
    {
        Bind();
        Init();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void GameButton()
    {
        Debug.Log("게임실행");
    }

    void KeyButton()
    {
        Debug.Log("조작키 설명");
    }

    void ExitButton()
    {
        Debug.Log("게임종료");
    }

    void Init()
    {
        gameButton = GetUI<Button>("gameButton");
        keyButton = GetUI<Button>("keyButton");
        exitButton = GetUI<Button>("exitButton");

        gameButton.onClick.AddListener(GameButton);
        keyButton.onClick.AddListener(KeyButton);
        exitButton.onClick.AddListener(ExitButton);

    }
}
