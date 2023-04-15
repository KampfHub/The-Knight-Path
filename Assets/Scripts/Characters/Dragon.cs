using UnityEngine;

public class Dragon : MonoBehaviour
{
    Animator animator;
    Enemy BaseScript;
    private const int immortalLayer = 6;
    private const int normallayer = 8;
    void Start()
    {
        animator = GetComponent<Animator>();
        BaseScript = GetComponent<Enemy>();
        gameObject.layer = immortalLayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StoneFellOnHead(float knockoutTime)
    {
        animator.SetBool("isFellOnHead", true);
        BaseScript.enabled = false;
        gameObject.layer = normallayer;
        Invoke("DragonWakeUp", knockoutTime);
    }
    private void DragonWakeUp()
    {
        animator.SetBool("isFellOnHead", false);
        BaseScript.enabled = true;
        gameObject.layer = immortalLayer;
    }
}
