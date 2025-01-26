using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartMover : MonoBehaviour
{
    //시작점, 도착점
    GameObject startPoint;
    GameObject endPoint;

    Rigidbody rigid;

    [SerializeField] float moveSpeed = 3f;
    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        rigid.velocity = Vector3.forward * moveSpeed;

        if(moveSpeed != 0)
        rigid.AddForce(rigid.velocity, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
            moveSpeed = 0f;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            moveSpeed = 5f;
            rigid.AddForce(rigid.velocity, ForceMode.Impulse);
        }
    }



    void Init()
    {
        startPoint = GameObject.FindGameObjectWithTag(Tag.Respawn);
        endPoint = GameObject.FindGameObjectWithTag(Tag.Finish);

        rigid = GetComponent<Rigidbody>();
    }
}
