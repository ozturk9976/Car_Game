using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
public class EditorCineExtension : MonoBehaviour
{


    private void LateUpdate()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                CineExtension.instance._cinemachineInputProvider.enabled = true;
            }
            else
            {
                CineExtension.instance._cinemachineInputProvider.enabled = false;
            }
        }
    }
}
#endif
