using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartLevel1 : CartMover
{
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
}
