using UnityEngine.UI; 
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    private Image _imageBar;
    private void Start()
    {
        _imageBar = GetComponent<Image>();
    }
    public void ÑhangeWidgetValue(float value)
    {
        if(_imageBar is not null) _imageBar.fillAmount = value;
    }
}
