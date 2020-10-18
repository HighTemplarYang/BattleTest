using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActionGroup
{
    List<BattleActionBase> battleActionBaseList;

    public BattleActionGroup(List<BattleActionBase> battleActionBaseList)
    {
        this.battleActionBaseList = battleActionBaseList;
    }

    public void Act()
    {
        foreach(var action in battleActionBaseList)
        {
            action.Act();
        }
    }
}

public enum BattleActionType
{
    DealDamage,
    Healing,
    SetBuff,
    DisperseBuff
}
public abstract class BattleActionBase 
{
    public BattleActionType battleActionType;
    public List<Unit> Targets;
    public Unit Caster;

    public BattleActionBase(BattleActionType battleActionType,List<Unit> Targets,Unit Caster)
    {
        this.battleActionType = battleActionType;
        this.Targets = Targets;
        this.Caster = Caster;
    }

    public abstract bool Act();
}

public class SetBuffAction : BattleActionBase
{
    public Buff buff;
    public SetBuffAction(Buff buff,List<Unit> Targets, Unit Caster) : base(BattleActionType.SetBuff, Targets, Caster)
    {
        this.buff = buff;
    }

    public override bool Act()
    {
        foreach (var target in Targets)
        {
            target.SetBuff(Buff.Copybuff(buff));
        }
        return true;
    }
}


public class DealDamageAction: BattleActionBase
{
    public OriginDamage originDamage;
    public DealDamageAction(OriginDamage originDamage, List<Unit> Targets,Unit Caster):base(BattleActionType.DealDamage,Targets,Caster)
    {
        this.originDamage = originDamage;
    }

    public override bool Act()
    {
        FinnalDamage finnalDamage = Caster.CorrectDamage(originDamage);
        foreach(var target in Targets)
        {
            target.TakeDamage(finnalDamage);
        }
        return true;
    }
}