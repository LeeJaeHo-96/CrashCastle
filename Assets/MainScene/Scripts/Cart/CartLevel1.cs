using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartLevel1 : CartMover
{
    private void Start()
    {
        base.Start();
        cartHP = 3;
    }
    protected override IEnumerator ReMoveRoutine()
    {
        yield return new WaitForSeconds(3f);
        if (CartMoveCo == null)
            CartMoveCo = StartCoroutine(CartMoveRoutine());
        ReMoveCo = null;
    }
}
