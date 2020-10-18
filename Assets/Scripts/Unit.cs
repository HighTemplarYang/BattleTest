using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CampType
{
    Allie,
    Enemy,
    Neutral
}

public class Unit 
{
    //单位阵营信息
    public CampType campType;

    //单位是否存活
    private bool alive;
    public bool Alive
    {
        get {
            return alive;
        }
    }

    //单位的剩余生命值
    private float life;
    public float Life
    {
        get
        {
            return life;
        }
    }

    //
    public BattleResourcesGroup BattleResourcesGroup;
    //单位的基础抗性
    public ResistanceGroup resistanceGroup;


    public List<int> BattleProperty;

    public Weapon weapon;

    public BuffMachine buffMachine;

    public Penetrate GetPenetrate(DamageType damageType)
    {
        return weapon.penetrateGroup.GetPenetrate(damageType);
    }
    

    public Unit(CampType campType,ResistanceGroup resistanceGroup,int life)
    {
        this.campType = campType;
        this.resistanceGroup = resistanceGroup;
        this.life = life;
        alive = true;
        buffMachine = new BuffMachine(this);
    }

    public FinnalDamage CorrectDamage(OriginDamage originDamage)
    {
        float Damage = 0;
        for(int i = 0; i < StaticManager.BattlePropertyCount; i++)
        {
            Damage += originDamage.DamageRateList[i] * BattleProperty[i] * weapon.WeaponCorrects[i];
        }
        Damage += originDamage.DamageStatic;
        return new FinnalDamage(originDamage.damageType, Damage, GetPenetrate(originDamage.damageType));
    }

    public bool SetBuff(Buff buff)
    {
        buffMachine.SetBuff(buff);
        return true;
    }

    public void TakeDamage(FinnalDamage finnalDamage)
    {
        float TakedNumber = resistanceGroup.GetResistance(finnalDamage.damageType).DamageCheck(finnalDamage);
        LoseLife(TakedNumber);
    }

    public void LoseLife(float lifeNum)
    {
        life -= lifeNum;
    }

}

