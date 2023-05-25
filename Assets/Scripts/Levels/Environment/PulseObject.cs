using UnityEngine;

public class PulseObject : MonoBehaviour
{
    [SerializeField] private float _upperBound;
    [SerializeField] private float _lowerBound;
    bool upscale;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.localScale.x >= _upperBound) upscale = false;
        if (gameObject.transform.localScale.x <= _lowerBound) upscale = true;
        SetScale();
    }
    private void SetScale()
    {
        transform.localScale = new Vector3(GetAxisValue(transform.localScale.x),
           GetAxisValue(transform.localScale.y), GetAxisValue(transform.localScale.z));
    }
    private float GetAxisValue(float baseScale)
    {
        if (upscale) return baseScale + Time.deltaTime;
        if (upscale == false) return baseScale - Time.deltaTime;
        return 0;
    }
}
