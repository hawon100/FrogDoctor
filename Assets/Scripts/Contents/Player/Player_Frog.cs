using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Frog : PlayerController
{
    public static Player_Frog Instance;

    public float infection;
    public float maxInfection;

    public int attack;
    public int attackSpeed;

    public int defenseCount = 0;

    public float hSpeed = 10f;
    public float vSpeed = 6f;

    [SerializeField] private Animator anim;

    public AudioClip overSound;

    private void Start()
    {
        Instance = this;
        infection = GameManager.instance.gameData.infection;
        maxInfection = 100;
        attack = GameManager.instance.gameData.attack;
        attackSpeed  = GameManager.instance.gameData.attackSpeed;
    }

    private void Update()
    {
        if (infection >= 100)
        {
            if (GameManager.instance.revival.activeSelf)
            {
                infection = 50;
                GameManager.instance.revival.SetActive(false);
            }
            else
            {
                infection = 100;
                Debug.Log("Game Over!");
                GameManager.instance.gameOverText.SetActive(true);
                Managers.Sound.Play(overSound);
            }
        }
    }

    public override void OnDamage(int value)
    {
        anim.SetTrigger("doHit");
        if(defenseCount > 0)
        {
            defenseCount--;
        }
        else
        {
            infection += value;
            GameManager.instance.shield.SetActive(false);
        }
    }
}