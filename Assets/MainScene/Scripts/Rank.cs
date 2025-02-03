using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class Rank : BaseUI
{
    [Inject]
    FirebaseManager firebaseManager;

    List<TMP_Text> rankList = new List<TMP_Text>();

    private void Awake()
    {
        Bind();
        Init();
    }

    private void Start()
    {
        firebaseManager.LoadLeaderboard(rankList);
    }

    private void OnEnable()
    {
        firebaseManager.LoadLeaderboard(rankList);
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
