using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public static ItemController Instance;

    [SerializeField] private int ItemSelectCount;
    [SerializeField] private int SkillSelectCount;

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
        int itemRandomSpawn = Random.Range(0, 5);
        int skillRandomSpawn = Random.Range(0, 2);

        for (int i = 0; i < ItemSelectCount; i++)
        {
            ItemName[i].text = itemName[itemRandomSpawn];
            ItemIcon[i].sprite = itemIcon[itemRandomSpawn];
            ItemLog[i].text = itemLog[itemRandomSpawn];
            Debug.Log(ItemName[i].text + "/" + ItemIcon[i].sprite + "/" + ItemLog[i].text);
        }

        SkillName.text = skillName[skillRandomSpawn];
        SkillIcon.sprite = skillIcon[skillRandomSpawn];
        SkillLog.text = skillLog[skillRandomSpawn];
        Debug.Log(SkillName.text + "/" + SkillIcon.sprite + "/" + SkillLog.text);
    }

    public void SelectButton(string value)
    {
        switch(value)
        {
            case "Item 1":

                break;
            case "Item 2":

                break;
            case "Skill":

                break;
        }
        GameManager.instance.ItemWindow.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private void ItemSelect()
    {
        for(int i = 0;  i < itemData.items.Length; i++)
        {
            if (ItemName[i].text == "공격력+")
            {
                Player_Frog.Instance.attack += 1;
            }
            else if (ItemName[i].text == "공격속도+")
            {
                Player_Frog.Instance.attackSpeed += 1;
            }
            else if(ItemName[i].text == "감염률-")
            {
                Player_Frog.Instance.infection -= 1;
            }
            else if(ItemName[i].text == "보호막")
            {

            }
            else if(ItemName[i].text == "부활")
            {

            }
            Debug.Log(ItemName[i]);
        }
    }
}
