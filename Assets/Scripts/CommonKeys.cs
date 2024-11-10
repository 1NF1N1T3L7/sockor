using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CommonKeys : MonoBehaviour
{
    [SerializeField] InputAction menu;


    private void Awake()
    {
        menu.performed += (ctx) => SceneChanger.Instance.MainMenu();
        menu.Enable();
    }

    private void OnDisable()
    {
        menu.Disable();
    }
}
