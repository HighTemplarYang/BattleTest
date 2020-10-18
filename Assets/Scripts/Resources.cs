using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class BattleResources
{
    public int ID;
    public string Name;
    public int ResourcesMax;
    public int CurrentResources;

    public BattleResources(int ID,string Name,int ResourcesMax,int CurrentResources)
    {
        this.ID = ID;
        this.Name = Name;
        this.ResourcesMax = ResourcesMax;
        this.CurrentResources = CurrentResources;
    }

    public bool ResourceCheck(int Cost)
    {
        return Cost <= CurrentResources;
    }

    public void PayResource(int Cost)
    {
        CurrentResources -= Cost;
        CurrentResources = Math.Max(0, CurrentResources);
    }

    public void GainResource(int Gain)
    {
        CurrentResources += Gain;
        CurrentResources = Math.Min(ResourcesMax, CurrentResources);
    }
}

public class BattleResourcesGroup
{
    public Dictionary<int, BattleResources> BattleRsourceDic;

    public BattleResourcesGroup(Dictionary<int, BattleResources> BattleRsourceDic)
    {
        this.BattleRsourceDic = BattleRsourceDic;
    }

    public bool ResourceGroupCheck(CostOrGain cost)
    {
        foreach(var pair in cost.CostDic)
        {
            if (!BattleRsourceDic.ContainsKey(pair.Key) || !BattleRsourceDic[pair.Key].ResourceCheck(pair.Value))
                return false;
        }
        return true;
    }

    public void PayResourceGroup(CostOrGain cost)
    {
        foreach (var pair in cost.CostDic)
        {
            if (BattleRsourceDic.ContainsKey(pair.Key))
                BattleRsourceDic[pair.Key].PayResource(pair.Value);
        }
    }

    public void GainResourceGroup(CostOrGain gain)
    {
        foreach (var pair in gain.CostDic)
        {
            if (BattleRsourceDic.ContainsKey(pair.Key))
                BattleRsourceDic[pair.Key].GainResource(pair.Value);
        }
    }
}

public class CostOrGain
{
    public Dictionary<int, int> CostDic;

    public CostOrGain(Dictionary<int, int> CostDic)
    {
        this.CostDic = CostDic;
    }
}

