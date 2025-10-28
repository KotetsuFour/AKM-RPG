using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestSwitchInput : MonoBehaviour
{
    private SwitchControls controls;
    private Vector2 leftStick;
    private Vector2 rightStick;
    // Start is called before the first frame update
    void Awake()
    {
        controls = new SwitchControls();

        controls.Gameplay.SwitchA.performed += whatever => switchA();
        controls.Gameplay.SwitchB.performed += whatever => switchB();
        controls.Gameplay.SwitchX.performed += whatever => switchX();
        controls.Gameplay.SwitchY.performed += whatever => switchY();

        controls.Gameplay.DpadDown.performed += whatever => down();
        controls.Gameplay.DpadLeft.performed += whatever => left();
        controls.Gameplay.DpadRight.performed += whatever => right();
        controls.Gameplay.DpadUp.performed += whatever => up();

        controls.Gameplay.LeftStick.performed += whatever => leftStick = whatever.ReadValue<Vector2>();
        controls.Gameplay.RightStick.performed += whatever => rightStick = whatever.ReadValue<Vector2>();

        controls.Gameplay.LeftStick.canceled += whatever => leftStick = Vector2.zero;
        controls.Gameplay.RightStick.canceled += whatever => rightStick = Vector2.zero;

    }

    private void switchA()
    {
        Debug.Log("Triggered A");
    }
    private void switchB()
    {
        Debug.Log("Triggered B");
    }
    private void switchX()
    {
        Debug.Log("Triggered X");
    }
    private void switchY()
    {
        Debug.Log("Triggered Y");
    }
    private void down()
    {
        Debug.Log("Triggered DOWN");
    }
    private void left()
    {
        Debug.Log("Triggered LEFT");
    }
    private void right()
    {
        Debug.Log("Triggered RIGHT");
    }
    private void up()
    {
        Debug.Log("Triggered UP");
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (leftStick != Vector2.zero)
        {
            Debug.Log(leftStick);
        }
        if (rightStick != Vector2.zero)
        {
            Debug.Log(rightStick);
        }
    }
}
