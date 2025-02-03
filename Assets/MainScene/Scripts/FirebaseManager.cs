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
        Init();
    }

    public void SaveScore(string userId, int score)
    {
        databaseReference.Child("scores").Child(userId).SetValueAsync(score)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("점수 저장 성공!");
                    Debug.Log((userId,score));
                }
                else
                {
                    Debug.LogError("점수 저장 실패: " + task.Exception);
                }
            });
    }

    public void LoadLeaderboard(List<TMP_Text> rankList)
    {
        databaseReference.Child("scores").OrderByChild("score").LimitToLast(5).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("리더보드 불러오기 성공!");
                DataSnapshot snapshot = task.Result;

                List<string> leaderboardEntries = new List<string>();

                foreach (DataSnapshot player in snapshot.Children)
                {
                    string playerName = player.Key;
                    int score = int.Parse(player.Value.ToString());
                    leaderboardEntries.Add($"이름: {playerName} /{score}점");
                }

                // ?? 가져온 리스트를 뒤집어서 (Reverse) 높은 점수가 위로 가게 만듦
                leaderboardEntries.Reverse();

                for (int i = 0; i < leaderboardEntries.Count && i < rankList.Count; i++)
                {
                    rankList[i].text = leaderboardEntries[i];
                }
            }
            else
            {
                Debug.LogError("리더보드 불러오기 실패: " + task.Exception);
            }
        });
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
