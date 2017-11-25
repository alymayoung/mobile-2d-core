using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.PlayerSkills
{
    public static class SkillLevels
    {
        public static Skill[] Skills = new Skill[4];
    }
    public class Skill
    {
        public string Name;
        public string Description { get; set; }
        public Level[] Levels;
    }
    public class Level
    {
        public float Cooldown;
        public float Duration;
        public int Uses;
    }

    public static class AntsInfo
    {
        public static HormigaProp[] Antsinformation = new HormigaProp[3];
    }

    public class HormigaProp
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Speed;
    }
}
