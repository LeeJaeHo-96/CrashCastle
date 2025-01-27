using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    [SerializeField] GameObject winText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Player))
            winText.SetActive(true);
    }
}
