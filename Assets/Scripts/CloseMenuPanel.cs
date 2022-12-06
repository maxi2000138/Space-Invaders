using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenuPanel : MonoBehaviour
{
    public void ClosePanel()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        
    }
}
