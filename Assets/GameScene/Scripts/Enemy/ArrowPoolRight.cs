using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPoolRight : MonoBehaviour
{
    [SerializeField] GameObject arrowPoint;
    public GameObject arrowPrefab;
    public int poolSize = 10;

    public List<GameObject> arrowPool;

    private void Start()
    {
        // 화살 풀 생성
        arrowPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, arrowPoint.transform.position, Quaternion.identity);
            arrow.SetActive(false);
            arrowPool.Add(arrow);
        }
    }

    /// <summary>
    /// 화살 가져오는 메서드
    /// </summary>
    /// <returns></returns>
    public GameObject GetArrow()
    {
        foreach (GameObject arrow in arrowPool)
        {
            if (!arrow.activeInHierarchy)
            {
                arrow.SetActive(true);
                return arrow;
            }

        }

        // 모든 화살이 사용 중이면 새로 생성
        GameObject newArrow = Instantiate(arrowPrefab, arrowPoint.transform.position, Quaternion.identity);
        newArrow.SetActive(true);
        arrowPool.Add(newArrow);
        return newArrow;
    }

    /// <summary>
    /// 화살 반환하는 메서드
    /// </summary>
    /// <param name="arrow"></param>
    public void ReturnBullet(GameObject arrow)
    {
        arrow.SetActive(false);
    }
}
