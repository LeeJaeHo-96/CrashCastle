using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowSpear : MonoBehaviour
{
    [SerializeField] GameObject spearPrefab;
    //창던지기 전용 카메라
    [SerializeField] Camera spearCam;

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
                    //레이캐스트 용 카메라 설정 후 그 캠에서 레이 쏴줌
                    Ray ray = spearCam.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        // 클릭한 위치에 창 생성
                        Vector3 spawnPosition = hit.point + Vector3.up * 10;
                        GameObject spear = Instantiate(spearPrefab, spawnPosition, Quaternion.identity);

                        // 중력을 이용해 떨어지게 설정
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

    void Init()
    {
        int cooldown = 0;
    }
}
