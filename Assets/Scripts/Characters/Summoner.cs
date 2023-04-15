using UnityEngine;

public class Summoner : Character
{
    [SerializeField] private float _maxHP;
    [SerializeField] private Vector3 selfSpawnPoint1;
    [SerializeField] private Vector3 selfSpawnPoint2;
    private SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        maxHP = _maxHP;
        currentHP = maxHP;
        animator = GetComponent<Animator>();
        if(transform.position == selfSpawnPoint2) sr.flipX= true;
        if(transform.position == selfSpawnPoint1) sr.flipX= false;
        gameObject.layer = 8;
    }
    protected override void OptionalDead()
    {
        if(gameObject.GetComponent<ObjectSpawner>() is not null)
        {
            gameObject.GetComponent<ObjectSpawner>().RockSpawn();
        }
        Invoke("Reborn", 2);
    }
    private void Reborn()
    {
        if(transform.position == selfSpawnPoint1)
        {
            Instantiate(gameObject, selfSpawnPoint2, Quaternion.identity);
        }
        if (transform.position == selfSpawnPoint2)
        {
            Instantiate(gameObject, selfSpawnPoint1, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
