using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Blackout : MonoBehaviour
{
    public float BlackoutTime = 2f;
    public float BlackoutChangeRatePerSecond = 10f;
    
    private Color _color;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _color = _image.color;
    }

    private void OnEnable()
    {
        StartCoroutine(BlackoutRoutine());
    }

    private void OnDisable()
    {
        StopCoroutine(BlackoutRoutine());
    }

    public IEnumerator BlackoutRoutine()
    {
        float k = 1;
        float delay = 1 / (BlackoutChangeRatePerSecond * BlackoutTime);
        while (k >= 0)
        {
            _color.a = k;
            _image.color = _color;
            yield return new WaitForSeconds(1f / BlackoutChangeRatePerSecond);
            k -= delay;
        }
    }
    
}
