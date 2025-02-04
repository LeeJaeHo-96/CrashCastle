using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EndPoint : MonoBehaviour
{
    [Inject]
    GameManager gameManager;
    [SerializeField] GameObject winText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Player))
        {
            gameManager.GameWin();
        }
    }
}
