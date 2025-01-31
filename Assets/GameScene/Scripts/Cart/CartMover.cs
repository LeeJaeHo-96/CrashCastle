using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartMover : MonoBehaviour
{
    CartSpawner factory;
    //시작점, 도착점
    GameObject startPoint;
    GameObject endPoint;

    protected Rigidbody rigid;

    [SerializeField] protected float moveSpeed;

    protected Coroutine CartMoveCo;
    protected Coroutine ReMoveCo;

    //카트 스탯
    public int cartHP;
    public int cartAttack;
    public int cartArmor;

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
    /// 카트 움직여주는 코루틴
    /// </summary>
    /// <returns></returns>
    protected IEnumerator CartMoveRoutine()
    {
        //코루틴 시작될 때 마다 무브 스피드를 줘서 시동걸어줌
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
    /// 다시 움직여주는 코루틴
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator ReMoveRoutine()
    {
        yield return new WaitForSeconds(3f);
        if (CartMoveCo == null)
            CartMoveCo = StartCoroutine(CartMoveRoutine());
        ReMoveCo = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Finish))
        {
            // 문에 박을때마다 100골드 추가
            GameManager.instance.gold += 100;
            GameManager.instance.attacked++;
            // 성문에 부딪혔을 때, 이동 코루틴을 멈추고 뒤로 후퇴 시킴
            StopCoroutine(CartMoveCo);
            CartMoveCo = null;

            moveSpeed = 0f;
            rigid.velocity = Vector3.back * 10f;
            rigid.AddForce(rigid.velocity, ForceMode.Impulse);

            //후퇴가 완료됐을 때, 다시 움직이는 코루틴 실행
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
    }
}
