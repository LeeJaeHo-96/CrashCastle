using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArcherSpawner : MonoBehaviour
{
    GameObject archer;
    WaitForSeconds spawnCooldown;

    [SerializeField] TMP_Text archerText;

    private void Awake()
    {
        Init();
    }
    private void Start()
    {
        StartCoroutine(ArcherSpawn());
    }

    private void Update()
    {

    }

    IEnumerator ArcherSpawn()
    {
        while (true)
        {
            if (!archer.activeSelf)
            {
                yield return spawnCooldown;
                archer.SetActive(true);
                if(!archerText.gameObject.activeSelf)
                archerText.gameObject.SetActive(true);
            }

            else
            {
                yield return null;
            }
        }
    }

    void Init()
    {
        archer = transform.GetChild(0).gameObject;
        spawnCooldown = new WaitForSeconds(5f);
    }
}
