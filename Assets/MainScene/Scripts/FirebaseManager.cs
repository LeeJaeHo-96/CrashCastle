using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    static public FirebaseManager instance;

    DatabaseReference databaseReference;

    int listNum;
    void Awake()
    {
        //SingletonInit();
        Init();
    }

    public void SaveScore(string userId, int score)
    {
        databaseReference.Child("users").Child(userId).Child("score").SetValueAsync(score)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("점수 저장 성공!");
                }
                else
                {
                    Debug.LogError("점수 저장 실패: " + task.Exception);
                }
            });
    }

    public void LoadScore(string userId)
    {
        databaseReference.Child("users").Child(userId).Child("score").GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    if (snapshot.Exists)
                    {
                        int score = int.Parse(snapshot.Value.ToString());
                        Debug.Log("불러온 점수: " + score);
                    }
                    else
                    {
                        Debug.Log("저장된 점수가 없습니다.");
                    }
                }
                else
                {
                    Debug.LogError("데이터 불러오기 실패: " + task.Exception);
                }
            });
    }

    
    public void LoadLeaderboard(List<TMP_Text> rankList)
    {
        databaseReference.Child("scores").OrderByValue().LimitToLast(5).GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot player in snapshot.Children)
                {
                    string playerName = player.Key;
                    int score = int.Parse(player.Value.ToString());
                    Debug.Log($"{playerName}: {score}");
                    //텍스트 [int i].text = $"{playerName}: {score}";
                    //i++
                    rankList[listNum].text = $"{playerName} : {score}";
                    listNum++;
                }
            }
        });
    }

    void SingletonInit()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(instance);

        DontDestroyOnLoad(gameObject);
    }

    void Init()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        });

        listNum = 0;
    }
}
