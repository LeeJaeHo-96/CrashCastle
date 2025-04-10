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

    Dictionary<string, object> userDict;
    void Awake()
    {
        Init();
    }

    public void SaveScore(string userId, int score)
    {
        // 사용자 데이터를 Dictionary로 저장
        Dictionary<string, object> userData = new Dictionary<string, object>
        {
            { "id", userId },
            { "score", score }
        };

        // 해당 사용자의 점수 업데이트
        databaseReference.Child("scores").Child(userId).SetValueAsync(userData)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log($"[ID: {userId}, 점수: {score}]");
                }
            });
    }

    public void LoadLeaderboard(List<TMP_Text> rankList)
    {
        // 점수 기준으로 상위 5개 가져오기
        databaseReference.Child("scores").OrderByChild("score").LimitToLast(5).GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    List<string> leaderboardEntries = new List<string>();

                    foreach (DataSnapshot player in snapshot.Children)
                    {
                        string playerName = player.Child("id").Value.ToString();
                        int score = int.Parse(player.Child("score").Value.ToString());

                        leaderboardEntries.Add($"이름: {playerName} / {score}점");
                    }

                    // 점수가 높은 순서대로 정렬 (Firebase는 오름차순으로 반환)
                    leaderboardEntries.Reverse();

                    // UI 업데이트
                    for (int i = 0; i < leaderboardEntries.Count && i < rankList.Count; i++)
                    {
                        rankList[i].text = leaderboardEntries[i];
                    }
                }
            });
    }
        /// <summary>
        /// 새 버젼 출시 랭킹 비우기용 함수(테스트용)
        /// </summary>
        public void RankReset(List<TMP_Text> rankList)
    {
        databaseReference.Child("scores").RemoveValueAsync();

        foreach (var rank in rankList)
        {
            rank.text = " ";
        }
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
