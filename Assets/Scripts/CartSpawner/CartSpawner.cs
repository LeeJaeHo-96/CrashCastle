using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartSpawner : MonoBehaviour
{
    [SerializeField] Vector3 cartSpawner;

    [SerializeField] GameObject cartLevel1;
    [SerializeField] GameObject cartLevel2;
    [SerializeField] GameObject cartLevel3;

    GameObject cart;
    public void Start()
    {
        cartSpawner = transform.position;
        cart = cartLevel1;
    }

    public void MakeCart()
    {
        Instantiate(cart, cartSpawner, Quaternion.identity);
    }
}
