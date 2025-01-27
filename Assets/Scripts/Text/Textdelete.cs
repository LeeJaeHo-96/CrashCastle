using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Textdelete : MonoBehaviour
{
    Color color;
    void Start()
    {
        StartCoroutine(DeleteRoutine());
    }

    IEnumerator DeleteRoutine()
    {
        color = gameObject.GetComponent<TMP_Text>().color;
        yield return new WaitForSeconds(2f);

        while (color.a > 0f)
        {
            color.a -= 0.1f;
            gameObject.GetComponent<TMP_Text>().color = color;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }
}
