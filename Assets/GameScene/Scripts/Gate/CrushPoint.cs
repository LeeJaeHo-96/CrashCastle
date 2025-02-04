using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushPoint : MonoBehaviour
{
    [SerializeField] GameObject gate;
    int gateHP;

    private void Start()
    {
        gateHP = 1;
    }

    private void Update()
    {
        if (gateHP == 0)
        {
            gate.SetActive(false);
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Player))
            gateHP--;
    }
}
