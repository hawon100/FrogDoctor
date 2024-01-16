using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    public string SkillName;
    public Sprite SkillIcon;
    [TextArea] public string SkillLog;
}

[CreateAssetMenu(fileName = "New SkillData", menuName = "Data/SkillData", order = int.MinValue)]
public class SkillData : BaseData
{
    public Skill[] skills;
}