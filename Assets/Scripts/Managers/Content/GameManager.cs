using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerMovement player;
    public GameObject tower;
    public Slider playerLocation;
    public Image infectionSlider;
    public GameObject ItemWindow;
    public GameObject Revival;
    public GameObject Vaccine;
    public GameObject shield;
    public int vaccineCount;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        vaccineCount = 0;
    }

    private void Update()
    {
        playerLocation.value = player.transform.position.x / tower.transform.position.x;
        infectionSlider.fillAmount = Player_Frog.Instance.infection / (float)Player_Frog.Instance.maxInfection;

        if(vaccineCount == 1)
        {
            Vaccine.SetActive(true);
        }
        else
        {
            Vaccine.SetActive(false);
        }
    }
}
