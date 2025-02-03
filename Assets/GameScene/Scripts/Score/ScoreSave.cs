using TMPro;
using UnityEngine.UI;
using Zenject;


public class ScoreSave : BaseUI
{
    [Inject]
    GameManager gameManager;
    [Inject]
    FirebaseManager firebaseManager;
    Button saveButton;
    TMP_InputField saveNameField;
    string saveName;
    TMP_Text score;
    int saveScore;

    private void Awake()
    {
        Bind();
        Init();
    }
    private void OnEnable()
    {
        //saveScore = GameManager.instance.totalScore;
        saveScore = gameManager.totalScore;
        score.text = $"Á¡¼ö : {saveScore}";
    }

    void SaveButton()
    {
        saveName = saveNameField.text;
        firebaseManager.SaveScore(saveName, saveScore);
    }

    void Init()
    {
        //firebaseManager = FirebaseManager.instance.GetComponent<FirebaseManager>();
        saveButton = GetUI<Button>("SaveButton");
        saveNameField = GetUI<TMP_InputField>("SaveName");
        score = GetUI<TMP_Text>("SaveScore");

        saveName = saveNameField.GetComponent <TMP_InputField>().text;
        saveButton.onClick.AddListener(SaveButton);
    }
}
