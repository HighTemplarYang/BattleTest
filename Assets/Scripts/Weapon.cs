using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Weapon
{
    public int ID;
    public string Name;
    public List<float> WeaponCorrects;
    //武器的基础穿透
    public PenetrateGroup penetrateGroup;

    public Weapon(int ID,string Name,float StrengthCorrect, float AgileCorrect, float IntelligenceCorrect, float FaithCorrect,PenetrateGroup penetrateGroup)
    {
        this.ID = ID;
        this.Name = Name;
        this.WeaponCorrects = new List<float>() { StrengthCorrect, AgileCorrect, IntelligenceCorrect, FaithCorrect };
        this.penetrateGroup = penetrateGroup;
    }

}

