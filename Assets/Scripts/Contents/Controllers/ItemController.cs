using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public static ItemController Instance;

    [SerializeField] private int attackUp;
    [SerializeField] private int attackSpeedUp;
    [SerializeField] private int InfectionRate;

    [SerializeField] private ItemData itemData;
    [SerializeField] private SkillData skillData;

    [SerializeField] private Text[] ItemName;
    [SerializeField] private Image[] ItemIcon;
    [SerializeField] private Text[] ItemLog;

    [SerializeField] private Text SkillName;
    [SerializeField] private Image SkillIcon;
    [SerializeField] private Text SkillLog;

    private string[] itemName;
    private Sprite[] itemIcon;
    private string[] itemLog;

    private string[] skillName;
    private Sprite[] skillIcon;
    private string[] skillLog;

    [SerializeField] private int curSyringeCooltime = 5;
    [SerializeField] private int curChangeCooltime = 5;

    private int maxSyringeCooltime = 5;
    private int maxChangeCooltime = 5;

    public AudioClip selectSound;

    private void Awake()
    {
        Instance = this;

        itemName = new string[itemData.items.Length];
        itemIcon = new Sprite[itemData.items.Length];
        itemLog = new string[itemData.items.Length];

        skillName = new string[skillData.skills.Length];
        skillIcon = new Sprite[skillData.skills.Length];
        skillLog = new string[skillData.skills.Length];

        for (int i = 0; i < itemData.items.Length; i++)
        {
            itemName[i] = itemData.items[i].ItemName;
            itemIcon[i] = itemData.items[i].ItemIcon;
            itemLog[i] = itemData.items[i].ItemLog;
        }

        for (int i = 0; i < skillData.skills.Length; i++)
        {
            skillName[i] = skillData.skills[i].SkillName;
            skillIcon[i] = skillData.skills[i].SkillIcon;
            skillLog[i] = skillData.skills[i].SkillLog;
        }
    }

    public void SelectCard()
    {
        int[] itemRandomSpawn = Util.RandomNumbers(itemData.items.Length, 3);

        for (int i = 0; i < 2; i++)
        {
            ItemName[i].text = itemName[itemRandomSpawn[i]];
            ItemIcon[i].sprite = itemIcon[itemRandomSpawn[i]];
            ItemLog[i].text = itemLog[itemRandomSpawn[i]];
            Debug.Log(ItemName[i].text + "/" + ItemIcon[i].sprite + "/" + ItemLog[i].text);
        }

        int skillRandomSpawn = Random.Range(0, 2);

        SkillName.text = skillName[skillRandomSpawn];
        SkillIcon.sprite = skillIcon[skillRandomSpawn];
        SkillLog.text = skillLog[skillRandomSpawn];
        Debug.Log(SkillName.text + "/" + SkillIcon.sprite + "/" + SkillLog.text);
    }

    public void SelectButton(string value)
    {
        switch (value)
        {
            case "Item 1":
                ItemSelect(1);
                break;
            case "Item 2":
                ItemSelect(2);
                break;
            case "Skill":
                SkillSelect();
                break;
        }
        GameManager.instance.ItemWindow.SetActive(false);
        Time.timeScale = 1.0f;
        AudioController.instance.SFXPlay("Select", selectSound);
        StartCoroutine(LoadSceneDelay());
    }

    private IEnumerator LoadSceneDelay()
    {
        yield return new WaitForSeconds(2.0f);
        Managers.Map.LoadScene(Define.Scene.InGame);
        yield break;
    }

    private void ItemSelect(int value)
    {
        if (value == 1)
        {
            if (ItemName[0].text == "공격력+")
            {
                Player_Frog.Instance.attack += attackUp;
            }
            else if (ItemName[0].text == "공격속도+")
            {
                Player_Frog.Instance.attackSpeed += attackSpeedUp;
            }
            else if (ItemName[0].text == "감염률-")
            {
                Player_Frog.Instance.OnDamage(-InfectionRate);
            }
            else if (ItemName[0].text == "보호막")
            {
                GameManager.instance.shield.SetActive(true);
            }
            else if (ItemName[0].text == "부활")
            {
                GameManager.instance.revival.SetActive(true);
            }
        }
        if (value == 2)
        {
            if (ItemName[1].text == "공격력+")
            {
                Player_Frog.Instance.attack += attackUp;
            }
            else if (ItemName[1].text == "공격속도+")
            {
                Player_Frog.Instance.attackSpeed += attackSpeedUp;
            }
            else if (ItemName[1].text == "감염률-")
            {
                Player_Frog.Instance.OnDamage(-InfectionRate);
            }
            else if (ItemName[1].text == "보호막")
            {
                GameManager.instance.shield.SetActive(true);
                Player_Frog.Instance.defenseCount = 10;
            }
            else if (ItemName[1].text == "부활")
            {
                GameManager.instance.revival.SetActive(true);
            }
        }
    }

    private void SkillSelect()
    {
        if (SkillName.text == "주사 맞아야겠지?")
        {
            GameManager.instance.syringeSkill.SetActive(true);
            if(curSyringeCooltime < maxSyringeCooltime)
            {
                GameManager.instance.playerSkill.SyringeCoolTime -= 1;
                curSyringeCooltime++;
            }
        }
        else if (SkillName.text == "분위기 전환!")
        {
            GameManager.instance.changeSkill.SetActive(true);
            if(curChangeCooltime < maxChangeCooltime)
            {
                GameManager.instance.playerSkill.ChangeCoolTime -= 1;
                curChangeCooltime++;
            }
        }
    }
}
