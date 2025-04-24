using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CamController : MonoBehaviour
{
    [SerializeField] GameObject mainCam;
    Camera _mainCam;
    [SerializeField] GameObject subCam;

    [SerializeField] GameObject camCart;
    Vector3 camPos;

    [SerializeField] GameObject left;
    [SerializeField] GameObject right;

    //��ư ���̱� �����
    [SerializeField] Button makeButton;
    [SerializeField] Button upButton;

    GameObject cart;

    Vector2 inputMoved;
    bool inputFixed;
    bool inputChange;

    PlayerInput inputAction;


    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        CamMove();
        CamFix();
        CamChange();

    }

    /// <summary>
    /// ī�޶� �̵� ����
    /// </summary>
    void CamMove()
    {
        // Ư�� ��ġ���� �̵��ϸ� �̵��� ����
        if (camCart.transform.position.z >= left.transform.position.z)
            if (inputMoved.x < 0)
            {
                return;
            }
        if (camCart.transform.position.z <= right.transform.position.z)
            if (inputMoved.x > 0)
                return;

        camCart.transform.position += new Vector3(0, 0, -inputMoved.x) / 2;
    }

    /// <summary>
    /// ī�޶� ���� ����
    /// </summary>
    void CamFix()
    {
        //īƮ�� ���� ��� ���� ã��
        if (cart == null || !cart.activeSelf)
        {
            cart = GameObject.FindGameObjectWithTag(Tag.Player);
            inputFixed = false;
        }
        camPos = camCart.transform.position;

        //īƮ�� ���� ��� ī�޶� ���� ���¸� ī�޶� īƮ�� ��ǥ�� ����
        if (inputFixed)
        {
            if (cart != null && cart.activeSelf)
                camPos.z = cart.transform.position.z;
            camCart.transform.position = camPos;
        }
    }

    void CamChange()
    {
        if(!inputChange)
        {
            _mainCam.depth = 2;
        }
        else if(inputChange)
        {
            _mainCam.depth = 0;
        }

        _mainCam.enabled = !inputChange;
        subCam.SetActive(inputChange);

        makeButton.gameObject.SetActive(!inputChange);
        upButton.gameObject.SetActive(!inputChange);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputMoved = context.ReadValue<Vector2>();


    }

    public void OnFixed(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            inputFixed = !inputFixed;
        }
    }

    public void OnCamChange(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            inputChange = !inputChange;
        }
    }


    void Init()
    {
        _mainCam = mainCam.GetComponent<Camera>();

        Camera.main.transform.position = camCart.transform.position;
        inputAction = camCart.GetComponent<PlayerInput>();

        inputFixed = false;
        inputChange = false;
    }
}
