using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowRight : MonoBehaviour
{
    Rigidbody rigid;
    float arrowSpeed = 45f;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        if (GameObject.FindGameObjectWithTag(Tag.Player) != null)
        {
            
            Vector3 direction = (GameObject.FindGameObjectWithTag(Tag.Player).transform.position - transform.position).normalized;
            gameObject.transform.up = direction;
            rigid.velocity = direction * arrowSpeed;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Player))
        {
            other.gameObject.GetComponent<CartMover>().cartHP--;
            gameObject.SetActive(false);
        }
    }
}
