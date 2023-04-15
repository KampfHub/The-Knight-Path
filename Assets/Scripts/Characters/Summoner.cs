using UnityEngine;

public class Summoner : Character
{
    [SerializeField] private float _maxHP;
    void Start()
    {
        maxHP = _maxHP;
        currentHP = maxHP;
        animator = GetComponent<Animator>();
    }

    protected override void OptionalDead()
    {
        if(gameObject.GetComponent<ObjectSpawner>() is not null)
        {
            gameObject.GetComponent<ObjectSpawner>().RockSpawn();
        }
    }
}
