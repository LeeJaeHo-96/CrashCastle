using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    [SerializeField] public int gold;

    [SerializeField] TMP_Text haveGold;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if(instance != this)
            Destroy(instance);
        
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        haveGold.text = $"°ñµå : {gold}";
    }
}
