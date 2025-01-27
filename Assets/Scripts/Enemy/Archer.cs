using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    int archerHP;

    Animation Animation;
    Animator animator;

    Coroutine shootCo;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        archerHP = 3;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Player))
        {
            StartCoroutine(ShootRoutine(other.gameObject));
            animator.SetBool("Attack", true);
            StartCoroutine(StopShoot(other.gameObject));
        }
    }

    IEnumerator StopShoot(GameObject player)
    {
        while (true)
        {
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag.Player))
        {
            Debug.Log("³ª°¬À½");
            animator.SetBool("Attack", false);
            StopCoroutine(shootCo);
            shootCo = null;
        }
    }

    IEnumerator ShootRoutine(GameObject cart)
    {
        while (true)
        {
            cart.GetComponent<CartMover>().cartHP -= 1;
            yield return new WaitForSeconds(4f);
            
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        Die();
        Physics.OverlapSphere(transform.position, gameObject.GetComponent<SphereCollider>().radius);
    }

    void Die()
    {
        if (archerHP <= 0)
        {
            animator.SetBool("Die", true);
            gameObject.SetActive(false);
        }
    }
}
