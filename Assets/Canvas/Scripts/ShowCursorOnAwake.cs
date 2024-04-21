using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCursorOnAwake : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
