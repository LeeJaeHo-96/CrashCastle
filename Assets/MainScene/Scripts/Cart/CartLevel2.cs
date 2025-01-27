using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartLevel2 : CartMover
{

    private void Start()
    {
        base.Start();
        cartHP = 5;
    }

    protected override IEnumerator ReMoveRoutine()
    {
        yield return new WaitForSeconds(2.5f);
        if (CartMoveCo == null)
            CartMoveCo = StartCoroutine(CartMoveRoutine());
        ReMoveCo = null;
    }
}
