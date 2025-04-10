using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CartSpawner : MonoBehaviour
{
    [Inject]
    GameManager gameManager;

    [Inject]
    DiContainer container;

    [SerializeField] Vector3 cartSpawner;

    [SerializeField] List<CarObjectData> carObjectDatas;


    [SerializeField] Slider cartMakeBar;

    [SerializeField] Button makedButton;

    [SerializeField] GameObject cart;
    [SerializeField] TMP_Text cartCostText;

    //카트 생성 체크용
    public GameObject makedCart;
    public void Awake()
    {
        Init();
    }

    /// <summary>
    /// 버튼용 _ 카트 업그레이드
    /// </summary>
    public void CartUpgrade1()
    {
        //최종 레벨일 경우 리턴시킴
        if (cart == carObjectDatas[carObjectDatas.Count - 1]) return;
        
        CarObjectData cartData1 = cart.GetComponent<CarObjectData>();
        CarObjectData cartData = carObjectDatas[0];
        
        //보유 골드가 카트의 업그레이드 비용 이상일 때
        if (gameManager.gold >= cartData.upgradeCost)
        {
            gameManager.gold -= cartData.upgradeCost;
            cart = carObjectDatas[cartData.cartLevel].cartPrefab;
            cartCostText.text = $"충차를 강화합니다.\n 비용 : {cart.GetComponent<CarObjectData>().upgradeCost}";
        }
    }

    public void CartUpgrade()
    {
        Debug.Log("실행됐음");
        // 현재 cart에 해당하는 ScriptableObject 찾기
        CarObjectData cartData = carObjectDatas.Find(data => data.cartPrefab == cart);
        Debug.Log(cartData.name);
        // 이미 마지막 레벨이면 리턴
        if (cartData == carObjectDatas[carObjectDatas.Count - 1]) return;

        // 업그레이드 가능하면
        if (gameManager.gold >= cartData.upgradeCost)
        {
            gameManager.gold -= cartData.upgradeCost;
            // 다음 레벨 데이터 가져오기
            CarObjectData nextData = carObjectDatas[cartData.cartLevel];
            cart = nextData.cartPrefab;
            cartCostText.text = $"충차를 강화합니다.\n 비용 : {nextData.upgradeCost}";
        }
    }

    /// <summary>
    /// 카트 생성해주는 코루틴
    /// </summary>
    public IEnumerator MakeCart()
    {
        if (makedCart != null)
        {
            StopCoroutine("MakeCart");
        }

        yield return new WaitForSeconds(3f);


        cartMakeBar.gameObject.SetActive(false);

        //충차 생산
        if (makedCart == null)
        {
            //makedCart = Instantiate(cart, cartSpawner, Quaternion.identity);
            makedCart = container.InstantiatePrefab(cart, cartSpawner, Quaternion.identity, null);
            Rigidbody rigid = makedCart.GetComponent<Rigidbody>();

            rigid.velocity = Vector3.down * 10f;
            rigid.AddForce(rigid.velocity, ForceMode.Impulse);

            StartCoroutine(DelayCoroutine());

        }
    }

    IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(1f);
        makedCart.GetComponent<CartMover>().enabled = true;
    }

    /// <summary>
    /// 충차 생산 바 채워주는 코루틴
    /// </summary>
    /// <returns></returns>
    public IEnumerator CartMakeBarRoutine()
    {
        //생성중일땐 동작 막아줌
        if (cartMakeBar.gameObject.activeSelf)
            yield break;

        if (makedCart != null)
        {
            StopCoroutine("CartMakeBarRoutine");
        }

        if ( makedCart == null)
        {
            cartMakeBar.value = 0;
            cartMakeBar.gameObject.SetActive(true);
            while (cartMakeBar.value < 1)
            {
                cartMakeBar.value += 0.03f;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

   

    void Init()
    {
        cartSpawner = transform.position;

        makedButton.onClick.AddListener
        (() =>
            {
                StartCoroutine(MakeCart());
                StartCoroutine(CartMakeBarRoutine());
            }
        );
    }
}
