using UnityEngine;

public class GameplayCursorController : MonoBehaviour
{
    bool cursorLocked = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !cursorLocked)
        {
            LockCursor();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockCursor();
        }
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cursorLocked = true;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cursorLocked = false;
    }
}
