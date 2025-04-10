using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowSpear : MonoBehaviour
{
    [SerializeField] GameObject spearPrefab;
    //â������ ���� ī�޶�
    [SerializeField] Camera spearCam;
    [SerializeField] GameObject circle;

    int cooldown;

    private void Awake()
    {
       Init();
    }


    public void OnFire(InputAction.CallbackContext context)
    {
        if (Camera.main.depth == 0)
        {
            if (cooldown <= 0)
            {
                if (context.performed)
                {
                    //����ĳ��Ʈ �� ī�޶� ���� �� �� ķ���� ���� ����
                    Ray ray = spearCam.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        // Ŭ���� ��ġ�� â ����
                        Vector3 spawnPosition = hit.point + Vector3.up * 10;
                        GameObject spear = Instantiate(spearPrefab, spawnPosition, Quaternion.identity);
                        cooldown = 2;
                        StartCoroutine(SpearRoutine());
                        // �߷��� �̿��� �������� ����
                        Rigidbody rb = spear.GetComponent<Rigidbody>();
                        if (rb == null)
                        {
                            rb = spear.AddComponent<Rigidbody>();
                        }
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (Camera.main != null && Camera.main.depth == 0)
        {
            Ray ray = spearCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 spawnPosition = hit.point + Vector3.up * 10;

                // ����� ���� ����
                Gizmos.color = Color.red;

                // â�� ������ ��ġ�� ��ü�� ǥ��
                Gizmos.DrawSphere(spawnPosition, 0.5f);

                // ���� ������ ������ ǥ��
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(ray.origin, hit.point + Vector3.down * 15f);
            }
        }
    }

    IEnumerator SpearRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            cooldown--;

            if (cooldown <= 0)
            {
                StopCoroutine("SpearRoutine");
                break;
            }
        }
    }

    void Init()
    {
        cooldown = 0;
    }
}
