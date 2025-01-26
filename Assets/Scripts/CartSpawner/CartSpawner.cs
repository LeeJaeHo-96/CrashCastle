using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartSpawner : MonoBehaviour
{
    [SerializeField] Vector3 cartSpawner;

    [SerializeField] GameObject cartLevel1;
    [SerializeField] GameObject cartLevel2;
    [SerializeField] GameObject cartLevel3;

    [SerializeField] Button makedButton;

    GameObject cart;
    Coroutine cartOnCo;
    public void Awake()
    {
        Init();
    }

    public void MakeCart()
    {
        Debug.Log("카트 생성");
        GameObject makedCart = Instantiate(cart, cartSpawner, Quaternion.identity);
        Rigidbody rigid = makedCart.GetComponent<Rigidbody>();

        rigid.velocity = Vector3.down * 10f;
        rigid.AddForce(rigid.velocity, ForceMode.Impulse);

        if (cartOnCo == null)
        {
            StartCoroutine(CartOnRoutine(makedCart));
        }
    }

    IEnumerator CartOnRoutine(GameObject makedCart)
    {
        yield return new WaitForSeconds(1f);
        if (makedCart.GetComponent<CartLevel1>() != null)
            makedCart.GetComponent<CartLevel1>().enabled = true;

        if (makedCart.GetComponent<CartLevel2>() != null)
            makedCart.GetComponent<CartLevel2>().enabled = true;

        if (makedCart.GetComponent<CartLevel3>() != null)
            makedCart.GetComponent<CartLevel3>().enabled = true;

        cartOnCo = null;

    }

    void Init()
    {
        cartSpawner = transform.position;
        cart = cartLevel1;

        makedButton.onClick.AddListener(MakeCart);
    }
}
