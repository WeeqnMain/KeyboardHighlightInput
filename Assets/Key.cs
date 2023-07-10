using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private GameObject highlight;
    private KeyCode _keyCode;

    private void Awake()
    {
        Enum.TryParse(gameObject.name, true, out _keyCode);
        if (_keyCode == KeyCode.None)
        {
            throw new Exception("can't identify button: " + gameObject.name);
        }
    }

    private void SetHighlight(bool isButtonPressed)
    {
        highlight.SetActive(isButtonPressed);
    }

    private void Update()
    {
        if (Input.GetKeyDown(_keyCode))
        {
            SetHighlight(true);
        }
        if (Input.GetKeyUp(_keyCode))
        {
            SetHighlight(false);
        }
    }
}
