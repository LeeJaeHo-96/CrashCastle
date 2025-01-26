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

    [SerializeField] GameObject cart;
    Coroutine cartOnCo;

    //카트 생성 체크용
    public GameObject makedCart;
    public void Awake()
    {
        Init();
    }


    /// <summary>
    /// 버튼용 _ 카트 업그레이드
    /// </summary>
    public void CartUpgrade()
    {
        if (cart == cartLevel1)
        {
            if (GameManager.instance.gold >= 500)
            {
                GameManager.instance.gold -= 500;
                cart = cartLevel2;
            }
        }
        else if (cart == cartLevel2)
        {
            if (GameManager.instance.gold >= 1000)
            {
                GameManager.instance.gold -= 1000;
                cart = cartLevel3;
            }
        }
    }

    /// <summary>
    /// 카트 생성해주는 메서드
    /// </summary>
    public void MakeCart()
    {
        if (makedCart == null)
        {
            makedCart = Instantiate(cart, cartSpawner, Quaternion.identity);
            Rigidbody rigid = makedCart.GetComponent<Rigidbody>();

            rigid.velocity = Vector3.down * 10f;
            rigid.AddForce(rigid.velocity, ForceMode.Impulse);

            if (cartOnCo == null)
            {
                StartCoroutine(CartOnRoutine(makedCart));
            }
        }
        else
        {
            //Todo : 텍스트 구현
            Debug.Log("이미 충차가 있습니다.");
        }
    }

    /// <summary>
    /// 카트가 낙하하면 스크립트 켜주는 코루틴
    /// </summary>
    /// <param name="makedCart">생성된 카트</param>
    /// <returns></returns>
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

        makedButton.onClick.AddListener(MakeCart);
    }
}
