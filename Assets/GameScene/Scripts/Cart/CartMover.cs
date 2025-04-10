using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CartMover : MonoBehaviour
{
    [Inject]
    GameManager gameManager;

    CartSpawner factory;
    //������, ������
    GameObject startPoint;
    GameObject endPoint;

    protected Rigidbody rigid;

    [SerializeField] protected float moveSpeed;

    protected Coroutine CartMoveCo;
    protected Coroutine ReMoveCo;

    public CarObjectData CarObjectData;
    //īƮ ����
    public int cartHP;
    public int cartAttack;


    private void Awake()
    {
        Init();
    }

    private void OnDisable()
    {
        factory.makedCart = null;
    }

    protected void Start()
    {
        if (CartMoveCo == null)
            CartMoveCo = StartCoroutine(CartMoveRoutine());
    }

    protected void Update()
    {
        if(cartHP <= 0)
            gameObject.SetActive(false);
    }


    /// <summary>
    /// īƮ �������ִ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    protected IEnumerator CartMoveRoutine()
    {
        //�ڷ�ƾ ���۵� �� ���� ���� ���ǵ带 �༭ �õ��ɾ���
        moveSpeed = 10f;
        while (true)
        {
            rigid.velocity = Vector3.forward * moveSpeed;

            if (moveSpeed != 0)
            {
                rigid.AddForce(rigid.velocity, ForceMode.Acceleration);
            }
            yield return null;
        }
    }

    /// <summary>
    /// �ٽ� �������ִ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator ReMoveRoutine()
    {
        yield return new WaitForSeconds(CarObjectData.reCharging);
        if (CartMoveCo == null)
            CartMoveCo = StartCoroutine(CartMoveRoutine());
        ReMoveCo = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Finish))
        {
            gameManager.gold += 100;
            gameManager.attacked++;
            // ������ �ε����� ��, �̵� �ڷ�ƾ�� ���߰� �ڷ� ���� ��Ŵ
            StopCoroutine(CartMoveCo);
            CartMoveCo = null;

            moveSpeed = 0f;
            rigid.velocity = Vector3.back * 10f;
            rigid.AddForce(rigid.velocity, ForceMode.Impulse);

            //���� �Ϸ���� ��, �ٽ� �����̴� �ڷ�ƾ ����
            if (ReMoveCo == null)
                ReMoveCo = StartCoroutine(ReMoveRoutine());
        }
    }

    void Init()
    {
        startPoint = GameObject.FindGameObjectWithTag(Tag.Respawn);
        endPoint = GameObject.FindGameObjectWithTag(Tag.Finish);

        rigid = GetComponent<Rigidbody>();

        factory = GameObject.FindGameObjectWithTag(Tag.Factory).GetComponent<CartSpawner>();

        cartHP = CarObjectData.cartHp;
        cartAttack = CarObjectData.cartAttack;
    }
}
