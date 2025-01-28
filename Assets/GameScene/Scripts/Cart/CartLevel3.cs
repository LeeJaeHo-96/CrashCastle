using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartLevel3 : CartMover
{

    private void Start()
    {
        base.Start();
        cartHP = 7;
    }
    protected override IEnumerator ReMoveRoutine()
    {
        yield return new WaitForSeconds(2f);
        if (CartMoveCo == null)
            CartMoveCo = StartCoroutine(CartMoveRoutine());
        ReMoveCo = null;
    }
}
