using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Zenject;

public class Rank : BaseUI
{
    [Inject]
    FirebaseManager firebaseManager;

    [SerializeField] GameObject mainCanvas;

    List<TMP_Text> rankList = new List<TMP_Text>();

    private void Awake()
    {
        Bind();
        Init();
    }

    private void OnEnable()
    {
        firebaseManager.LoadLeaderboard(rankList);
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Close();
        }
    }

    private void Close()
    {
        mainCanvas.SetActive(true);
        gameObject.SetActive(false);
    }


    void Init()
    {
        //firebaseManager = FirebaseManager.instance.GetComponent<FirebaseManager>();

        rankList.Add(GetUI<TMP_Text>("Rank1"));
        rankList.Add(GetUI<TMP_Text>("Rank2"));
        rankList.Add(GetUI<TMP_Text>("Rank3"));
        rankList.Add(GetUI<TMP_Text>("Rank4"));
        rankList.Add(GetUI<TMP_Text>("Rank5"));
    }
}
