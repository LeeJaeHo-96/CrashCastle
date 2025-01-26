using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartLevel3 : CartMover
{
    protected override IEnumerator ReMoveRoutine()
    {
        yield return new WaitForSeconds(2f);
        if (CartMoveCo == null)
            CartMoveCo = StartCoroutine(CartMoveRoutine());
        ReMoveCo = null;
    }
}
