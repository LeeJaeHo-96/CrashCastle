using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tag.Enemy))
        {
            Debug.Log("피안달았음");
            if (collision.gameObject.GetComponent<ArcherLeft>() != null)
            {
                Debug.Log("피달았음");
                collision.gameObject.GetComponent<ArcherLeft>().archerHP--;
            }
            else if (collision.gameObject.GetComponent<ArcherRight>() != null)
                collision.gameObject.GetComponent<ArcherRight>().archerHP--;

            gameObject.SetActive(false);
        }
    }
}
