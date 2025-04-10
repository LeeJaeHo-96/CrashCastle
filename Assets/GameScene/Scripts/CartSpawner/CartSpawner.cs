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

    //īƮ ���� üũ��
    public GameObject makedCart;
    public void Awake()
    {
        Init();
    }

    /// <summary>
    /// ��ư�� _ īƮ ���׷��̵�
    /// </summary>
    public void CartUpgrade1()
    {
        //���� ������ ��� ���Ͻ�Ŵ
        if (cart == carObjectDatas[carObjectDatas.Count - 1]) return;
        
        CarObjectData cartData1 = cart.GetComponent<CarObjectData>();
        CarObjectData cartData = carObjectDatas[0];
        
        //���� ��尡 īƮ�� ���׷��̵� ��� �̻��� ��
        if (gameManager.gold >= cartData.upgradeCost)
        {
            gameManager.gold -= cartData.upgradeCost;
            cart = carObjectDatas[cartData.cartLevel].cartPrefab;
            cartCostText.text = $"������ ��ȭ�մϴ�.\n ��� : {cart.GetComponent<CarObjectData>().upgradeCost}";
        }
    }

    public void CartUpgrade()
    {
        Debug.Log("�������");
        // ���� cart�� �ش��ϴ� ScriptableObject ã��
        CarObjectData cartData = carObjectDatas.Find(data => data.cartPrefab == cart);
        Debug.Log(cartData.name);
        // �̹� ������ �����̸� ����
        if (cartData == carObjectDatas[carObjectDatas.Count - 1]) return;

        // ���׷��̵� �����ϸ�
        if (gameManager.gold >= cartData.upgradeCost)
        {
            gameManager.gold -= cartData.upgradeCost;
            // ���� ���� ������ ��������
            CarObjectData nextData = carObjectDatas[cartData.cartLevel];
            cart = nextData.cartPrefab;
            cartCostText.text = $"������ ��ȭ�մϴ�.\n ��� : {nextData.upgradeCost}";
        }
    }

    /// <summary>
    /// īƮ �������ִ� �ڷ�ƾ
    /// </summary>
    public IEnumerator MakeCart()
    {
        if (makedCart != null)
        {
            StopCoroutine("MakeCart");
        }

        yield return new WaitForSeconds(3f);


        cartMakeBar.gameObject.SetActive(false);

        //���� ����
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
    /// ���� ���� �� ä���ִ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    public IEnumerator CartMakeBarRoutine()
    {
        //�������϶� ���� ������
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
