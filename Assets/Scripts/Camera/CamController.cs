using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamController : MonoBehaviour
{
    [SerializeField] GameObject camCart;

    [SerializeField] GameObject left;
    [SerializeField] GameObject right;

    Vector2 inputMoved;

    PlayerInput inputAction;


    private void Awake()
    {
        Init();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (camCart.transform.position.z == left.transform.position.z)
            if (inputMoved.x < 0)
                return;

        if (camCart.transform.position.z == right.transform.position.z)
            if (inputMoved.x > 0)
                return;

        camCart.transform.position += new Vector3(0, 0, -inputMoved.x)/2;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputMoved = context.ReadValue<Vector2>();


    }


    void Init()
    {
        Camera.main.transform.position = camCart.transform.position;
        inputAction = camCart.GetComponent<PlayerInput>();


    }
}
