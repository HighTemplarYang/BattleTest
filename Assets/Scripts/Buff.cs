using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BuffMachine
{
    List<Buff> buffList;
    Unit Self;

    public BuffMachine(Unit Self)
    {
        this.Self = Self;
        buffList = new List<Buff>();
    }

    public void SetBuff(Buff buff)
    {

        if(HaveOverlayBuff(buff,out Buff tempbuff))
        {
            tempbuff.BuffOverLay(buff.LayNum);
        }
        else
        {
            buffList.Add(buff);
        }
        buff.Self = this.Self;
        buff.OnBuffSet();
    }

    bool HaveOverlayBuff(Buff BuffCheck,out Buff buff)
    {
        if (!BuffCheck.Overlay)
        {
            buff = null;
            return false;
        } 
        foreach(var bf in buffList)
        {
            if (bf.ID == BuffCheck.ID)
            {
                buff = bf;
                return true;
            }
        }
        buff = null;
        return false;
    }

    public void EndBuff(Buff buff)
    {
        buffList.Remove(buff);
        buff.OnBuffEnd();
    }

    public void TurnEnd()
    {
        List<Buff> WaitToRemove = new List<Buff>();
        foreach(var bf in buffList)
        {
            bf.OnBuffTurnEnd();
            if (bf.RemainTurnNum <= 0)
            {
                WaitToRemove.Add(bf);
            }
        }
        foreach(Buff bf in WaitToRemove)
        {
            EndBuff(bf);
        }
    }

    

}

public enum BuffType
{
    Magic,
    Curse,
    Disease,
    Poisoning
} 

public class Buff
{
    public int ID;
    public BuffType buffType;
    public int turnNumber;
    public int RemainTurnNum;
    public List<Fix> fixes;
    public bool Overlay;
    public bool OverlayRefreshTurn;
    public int LayNum;
    public Unit Self;

    public BattleActionGroup battleActionGroupOnSet;
    public BattleActionGroup battleActionGroupOnEnd;
    public BattleActionGroup battleActionGroupOnTurnEnd;

    public Buff(int ID,BuffType buffType,int turnNumber, List<Fix> fixes,bool Overlay=false,bool OverlayRefreshTurn=true, int LayNum=1, BattleActionGroup battleActionGroupOnSet=null, BattleActionGroup battleActionGroupOnEnd=null, BattleActionGroup battleActionGroupOnTurnEnd = null)
    {
        this.ID = ID;
        this.buffType = buffType;
        this.turnNumber = turnNumber;
        this.RemainTurnNum = turnNumber;
        this.fixes = fixes;
        this.battleActionGroupOnEnd = battleActionGroupOnEnd;
        this.battleActionGroupOnSet = battleActionGroupOnSet;
        this.battleActionGroupOnTurnEnd = battleActionGroupOnTurnEnd;
        this.Overlay = Overlay;
        this.LayNum = LayNum;
        this.OverlayRefreshTurn = OverlayRefreshTurn;   
    }

    public static Buff Copybuff(Buff buff)
    {
        if (buff == null)
        {
            return null;
        }
        return new Buff(buff.ID, buff.buffType, buff.turnNumber, buff.fixes, buff.Overlay, buff.OverlayRefreshTurn, buff.LayNum, buff.battleActionGroupOnSet, buff.battleActionGroupOnEnd, buff.battleActionGroupOnTurnEnd);
    }

    public void BuffOverLay(int LayNum)
    {
        this.LayNum += LayNum;
        if (OverlayRefreshTurn)
        {
            RemainTurnNum = turnNumber;
        }
    }

    public void OnBuffSet()
    {
        if(battleActionGroupOnSet!=null)
            battleActionGroupOnSet.Act();
    }

    public void OnBuffEnd()
    {
        if(battleActionGroupOnEnd!=null)
            battleActionGroupOnEnd.Act();
    }

    public void OnBuffTurnEnd()
    {
        if (battleActionGroupOnTurnEnd != null)
            battleActionGroupOnTurnEnd.Act();
        RemainTurnNum--;
    }

   
}

public enum FixType
{
    StrengthFixRate,
    StrengthFixStatic,
    IntelligenceFixRate,
    IntelligenceFixStatic,
    CutResistanceRateFixRate,
    CutResistanceRateFixStatic,
    CutResistanceStaticFixRate,
    CutResistanceStaticFixStatic,
    KinectResistanceRateFixRate,
    KinectCutResistanceRateFixStatic,
    KinectResistanceStaticFixRate,
    KinectResistanceStaticFixStatic,
    FireResistanceRateFixRate,
    FireResistanceRateFixStatic,
    FireResistanceStaticFixRate,
    FireResistanceStaticFixStatic,
    CutPenetrateRateFixRate,
    CutPenetrateRateFixStatic,
    CutPenetrateStaticFixRate,
    CutPenetrateStaticFixStatic,
    KinectPenetrateRateFixRate,
    KinectCutPenetrateRateFixStatic,
    KinectPenetrateStaticFixRate,
    KinectPenetrateStaticFixStatic,
    FirePenetrateRateFixRate,
    FirePenetrateRateFixStatic,
    FirePenetrateStaticFixRate,
    FirePenetrateStaticFixStatic
}

public class Fix
{
    public FixType fixType;
    public float fixValue;
    public Fix(FixType fixType,float fixValue)
    {
        this.fixType = fixType;
        this.fixValue = fixValue;
    }
}



