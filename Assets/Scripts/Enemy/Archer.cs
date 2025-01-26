using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    Coroutine shootCo;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Player))
            if(shootCo == null)
            shootCo = StartCoroutine(ShootRoutine(other.gameObject));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag.Player))
        {
            StopCoroutine(shootCo);
            shootCo = null;
        }
    }

    IEnumerator ShootRoutine(GameObject cart)
    {
        cart.GetComponent<CartMover>().cartHP -= 1;
        yield return new WaitForSeconds(1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
