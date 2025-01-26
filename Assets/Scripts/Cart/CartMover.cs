using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartMover : MonoBehaviour
{
    //시작점, 도착점
    GameObject startPoint;
    GameObject endPoint;

    protected Rigidbody rigid;

    [SerializeField] protected float moveSpeed;
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

    



    void Init()
    {
        startPoint = GameObject.FindGameObjectWithTag(Tag.Respawn);
        endPoint = GameObject.FindGameObjectWithTag(Tag.Finish);

        rigid = GetComponent<Rigidbody>();
    }
}
