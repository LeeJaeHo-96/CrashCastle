using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    static public PlayerInput PlayerInput;

    [SerializeField] public int gold;

    [SerializeField] TMP_Text haveGold;
    [SerializeField] TMP_Text curTimer;
    [SerializeField] TMP_Text result;
    [SerializeField] TMP_Text scoreText;

    public int timer;

    GameObject cam;
    GameObject pause;
    GameObject rankPanel;

    //점수 관련
    public int score;
    public int diedEnemy;
    public int attacked;
    public int timerScore;

    public int totalScore;

    private void Awake()
    {
        //SingletonInit();
        Init();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimerRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInfo();

    }

    IEnumerator TimerRoutine()
    {
        while (true)
        {
            timer--;
            yield return new WaitForSeconds(1f);
        }
    }

    void UpdateInfo()
    {
        haveGold.text = $"골드 : {gold}";
        curTimer.text = $"남은 시간 : {timer}";

        if (timer == 0)
        {
            StopAllCoroutines();
            totalScore = ScoreCal();
            scoreText.text = $"점수 : {totalScore}점";
            scoreText.gameObject.SetActive(true);
            result.text = "성문을 파괴하지 못하고 전멸하였습니다.. 패배.. \n 'E' 버튼을 눌러 메인화면으로 돌아가세요..";
            result.gameObject.SetActive(true);
            //Todo: 게임 패배
        }
    }

    public void GameWin()
    {
        StopAllCoroutines();
        totalScore = ScoreCal();
        scoreText.text = $"점수 : {totalScore}점";
        scoreText.gameObject.SetActive(true);
        result.text = "성문이 열렸고 기사들이 돌진합니다.. 승리!!  \n 'E' 버튼을 눌러 점수를 저장하세요!";
        rankPanel.gameObject.SetActive(true);
    }

    int ScoreCal()
    {
        // 죽인 적 수, 문을 공격한 타수, 남은 시간 비례로 점수 부여
        return score = (diedEnemy * 100) + (attacked * 100) + (timer * 100);
    }

    void SingletonInit()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(instance);

        DontDestroyOnLoad(gameObject);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if ((result == null))
            return;

        if ((result.gameObject.activeSelf))
        {
            if (context.performed)
            {
                rankPanel.gameObject.SetActive(true);
            }
        }

        if ((result.gameObject.activeSelf))
        {
            if (context.performed)
            {
                SceneManager.LoadScene(Scene.MainScene);
            }
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pause.SetActive(!pause.activeSelf);
            if (!pause.activeSelf)
            {
                GameManager.PlayerInput.SwitchCurrentActionMap(ActionMap.Cam);
                Time.timeScale = 1f;
            }
        }
    }

    void Init()
    {
        gold = 0;
        timer = 100;

        cam = Camera.main.gameObject;
        PlayerInput = cam.GetComponent<PlayerInput>();
        rankPanel = GameObject.FindGameObjectWithTag(Tag.GameController);
        pause = GameObject.FindGameObjectWithTag(Tag.Ship);
    }
}
