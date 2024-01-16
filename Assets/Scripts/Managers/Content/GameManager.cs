using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerMovement player;
    public Player_Skill playerSkill;
    public GameObject tower;
    public Slider playerLocation;
    public Image infectionSlider;
    public GameObject ItemWindow;
    public GameObject revival;
    public GameObject Vaccine;
    public GameObject shield;
    public int vaccineCount;
    public GameObject syringeSkill;
    public GameObject changeSkill;
    public PresentData gameData;
    public PresentData pastData;
    public bool isLive;
    public GameObject gameOverWindow;
    public GameObject gameOverText;
    public int score;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        isLive = false;
        vaccineCount = 0;

        if (isLive)
        {
            gameData.score = score;
            gameData.infection = Player_Frog.Instance.infection;
            gameData.revival = revival.activeSelf;
            gameData.shield = shield.activeSelf;
            gameData.attack = Player_Frog.Instance.attack;
            gameData.attackSpeed = Player_Frog.Instance.attackSpeed;
            gameData.skillSyringe = syringeSkill.activeSelf;
            gameData.skillChange = changeSkill.activeSelf;
            gameData.skillSyringeCoolTime = Player_Skill.Instance.syringeCoolTime;
            gameData.skillChangeCoolTime = Player_Skill.Instance.changeCoolTime;

            gameData.enemyHp += 10;
            gameData.enemyInfection += 5;
            gameData.enemyInfectionSpeed += 5;
            gameData.enemySpeed += 1;
        }
        else if (!isLive)
        {
            gameData.score = pastData.score;
            gameData.infection = pastData.infection;
            gameData.revival = pastData.revival;
            gameData.shield = pastData.shield;
            gameData.attack = pastData.attack;
            gameData.attackSpeed = pastData.attackSpeed;
            gameData.skillSyringe = pastData.skillSyringe;
            gameData.skillChange = pastData.skillChange;
            gameData.skillSyringeCoolTime = pastData.skillSyringeCoolTime;
            gameData.skillChangeCoolTime = pastData.skillChangeCoolTime;

            gameData.enemyHp = pastData.enemyHp;
            gameData.enemyInfection = pastData.enemyInfection;
            gameData.enemyInfectionSpeed = pastData.enemyInfectionSpeed;
            gameData.enemySpeed = pastData.enemySpeed;
        }

        revival.SetActive(gameData.revival);
        shield.SetActive(gameData.shield);
    }

    private void Update()
    {
        playerLocation.value = player.transform.position.x / tower.transform.position.x;
        infectionSlider.fillAmount = Player_Frog.Instance.infection / Player_Frog.Instance.maxInfection;

        if (vaccineCount == 1)
        {
            Vaccine.SetActive(true);
            Save();
        }
        else
        {
            Vaccine.SetActive(false);
        }

        if (gameOverWindow.activeSelf)
        {
            player.gameObject.SetActive(false);
        }
    }

    private void Save()
    {
        gameData.infection = Player_Frog.Instance.infection;
        gameData.revival = revival.activeSelf;
        gameData.shield = shield.activeSelf;
        gameData.attack = Player_Frog.Instance.attack;
        gameData.attackSpeed = Player_Frog.Instance.attackSpeed;
        gameData.skillSyringe = syringeSkill.activeSelf;
        gameData.skillChange = changeSkill.activeSelf;
        gameData.skillSyringeCoolTime = Player_Skill.Instance.syringeCoolTime;
        gameData.skillChangeCoolTime = Player_Skill.Instance.changeCoolTime;
    }
}
