using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Frog : PlayerController
{
    public static Player_Frog Instance;

    [SerializeField] private PlayerData data;
    [SerializeField] private int hp;
    [SerializeField] private int maxHp;

    public int attack;
    [SerializeField] private int attackSpeed;

    public int infectionRate;

    private void Start()
    {
        Instance = this;

        hp = data.hp;
        maxHp = data.hp;
        attack = data.attack;
        attackSpeed = data.attackSpeed;
        infectionRate = data.infectionRate;
    }

    private void Update()
    {
        if (hp <= 0)
        {
            Debug.Log("Game Over!");
        }
    }

    public override void OnDamage(int value)
    {
        hp -= value;
    }
}