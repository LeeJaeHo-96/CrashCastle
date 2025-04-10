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
        // ����� �����͸� Dictionary�� ����
        Dictionary<string, object> userData = new Dictionary<string, object>
        {
            { "id", userId },
            { "score", score }
        };

        // �ش� ������� ���� ������Ʈ
        databaseReference.Child("scores").Child(userId).SetValueAsync(userData)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log($"[ID: {userId}, ����: {score}]");
                }
            });
    }

    public void LoadLeaderboard(List<TMP_Text> rankList)
    {
        // ���� �������� ���� 5�� ��������
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

                        leaderboardEntries.Add($"�̸�: {playerName} / {score}��");
                    }

                    // ������ ���� ������� ���� (Firebase�� ������������ ��ȯ)
                    leaderboardEntries.Reverse();

                    // UI ������Ʈ
                    for (int i = 0; i < leaderboardEntries.Count && i < rankList.Count; i++)
                    {
                        rankList[i].text = leaderboardEntries[i];
                    }
                }
            });
    }
        /// <summary>
        /// �� ���� ��� ��ŷ ����� �Լ�(�׽�Ʈ��)
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
