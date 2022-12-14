using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class Blackout : MonoBehaviour
{
    public float BlackoutTime = 2f;
    public float BlackoutChangeRatePerSecond = 10f;
    public TMP_Text _text;
    
    private Color _color;
    private Image _image;
    
    public event System.Action OnBlackoutRoutineEnd;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _color = _image.color;
    }

    public Coroutine ShowBlackout() => 
        StartCoroutine(BlackoutRoutine());

    public Coroutine ShowLight() => 
        StartCoroutine(LightRoutine());


    private IEnumerator LightRoutine()
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

    private IEnumerator BlackoutRoutine()
    {
        float k = 0;
        float delay = 1 / (BlackoutChangeRatePerSecond * BlackoutTime);
        
        while (k <= 1)
        {
            _color.a = k;
            _image.color = _color;
            yield return new WaitForSeconds(1f / BlackoutChangeRatePerSecond);
            k += delay;
        }
        OnBlackoutRoutineEnd?.Invoke();
    }



}
