using UnityEngine;
using UnityEngine.UI;

public class Dragon : MonoBehaviour
{
    private Animator animator;
    private Enemy BaseScript;
    private SpriteRenderer sr;
    private GameObject hpBar;
    private const int immortalLayer = 6;
    private const int normallayer = 8;
    private bool isDead;
    private float flySpeed;

    private void Start()
    {
        if(GetComponent<Animator>() is not null) animator = GetComponent<Animator>();
        if(GetComponent<Enemy>() is not null) BaseScript = GetComponent<Enemy>();
        if(GetComponent<SpriteRenderer>() is not null) sr = GetComponent<SpriteRenderer>();
        if(GameObject.Find("GragonHP") is not null) hpBar = GameObject.Find("GragonHP");
        gameObject.layer = immortalLayer;
        BaseScript.EnemyDead += Dead;
        BaseScript.EnemyHit += GetHit;
        isDead = false;
    }

    private void Update()
    {
        if(isDead)
        {
            transform.Translate(Time.deltaTime * flySpeed, Time.deltaTime, 0);
        }
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
    private void GetHit()
    {
        if(hpBar is not null)
        {
            hpBar.GetComponent<Image>().fillAmount = BaseScript.GetHPRatio();
        }
    }
    private void Dead()
    {
        GameObject summonRef = GameObject.FindWithTag("Summon");
        GameObject Cage = GameObject.FindWithTag("Finish");
        hpBar.SetActive(false);
        animator.SetBool("isFellOnHead", false);
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
        Invoke("SetIsDead", 2);
        if(sr.flipX) flySpeed = -1.5f;
        else flySpeed = 1.5f;
        if (Cage is not null) Cage.GetComponent<ObjectSpawner>().DestroyAndSpawn();
        if (summonRef is not null) Destroy(summonRef);
    }
    private void SetIsDead()
    {
        isDead = true;
    }
}
