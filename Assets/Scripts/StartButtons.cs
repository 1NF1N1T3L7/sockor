using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartButtons : MonoBehaviour
{
    [SerializeField] InputAction enter;


    private void Awake()
    {
        enter.performed += (ctx) =>  GameManager.Instance.StartGame();
        enter.Enable();
    }

    private void OnDisable()
    {
        enter.Disable();
    }

}
