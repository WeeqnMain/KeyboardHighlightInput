using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private GameObject highlight;

    public void Highlight(bool isButtonPressed)
    {
        highlight.SetActive(isButtonPressed);
    }
}
