using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTex;

    [SerializeField] private bool cursorLock;

    private void Awake() 
    {
        Cursor.SetCursor(cursorTex, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void Update()
    {
        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (!cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void SetCursorState(bool cursorState)
    {
        cursorLock = cursorState;
    }
}
