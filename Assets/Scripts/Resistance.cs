using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Resistance
{
    static float ResistanceFact = 100;

    public DamageType ResistanceType;
    public float ResistanceRate;
    public float ResistanceStatic;
    public Resistance(DamageType ResistanceType)
    {
        this.ResistanceType = ResistanceType;
        this.ResistanceRate = 0;
        this.ResistanceStatic = 0;
    }
    public Resistance(DamageType ResistanceType,float ResistanceRate,float ResistanceStatic)
    {
        this.ResistanceType = ResistanceType;
        this.ResistanceRate = ResistanceRate;
        this.ResistanceStatic = ResistanceStatic;
    }

    public float DamageCheck(FinnalDamage finnalDamage)
    {
        float resistanceRate = ResistanceRate - finnalDamage.penetrate.PenetrateRate;
        resistanceRate = Math.Max(0, resistanceRate);
        resistanceRate /= 100f;
        float resistanceStatic = ResistanceStatic - finnalDamage.penetrate.PenetrateStatic;
        resistanceStatic = Math.Max(0, resistanceStatic);
        return finnalDamage.damage * (1 - resistanceRate) - resistanceStatic;
    }
}

public class ResistanceGroup
{
    private Dictionary<DamageType, Resistance> resistanceDic;

    public ResistanceGroup(Dictionary<DamageType, Resistance> resistanceDic)
    {
        resistanceDic = new Dictionary<DamageType, Resistance>();
        for (int i = 0; i < 10; i++)
        {
            if(resistanceDic.TryGetValue((DamageType)i,out Resistance resistance))
            {
                this.resistanceDic.Add((DamageType)i, resistance);
            }
            else
            {
                this.resistanceDic.Add((DamageType)i, new Resistance((DamageType)i));
            }
        }
    }

    public Resistance GetResistance(DamageType damageType)
    {
        return resistanceDic[damageType];
    }
}

public class Penetrate
{
    public DamageType PenetrateType;
    public float PenetrateRate;
    public float PenetrateStatic;

    public Penetrate(DamageType PenetrateType)
    {
        this.PenetrateType = PenetrateType;
        this.PenetrateRate = 0;
        this.PenetrateStatic = 0;
    }

    public Penetrate(DamageType PenetrateType, float PenetrateRate, float PenetrateStatic)
    {
        this.PenetrateType = PenetrateType;
        this.PenetrateRate = PenetrateRate;
        this.PenetrateStatic = PenetrateStatic;
    }
}

public class PenetrateGroup
{
    private Dictionary<DamageType, Penetrate> penetrateDic;

    public PenetrateGroup(Dictionary<DamageType, Penetrate> penetrateDic)
    {
        penetrateDic = new Dictionary<DamageType, Penetrate>();
        for (int i = 0; i < 10; i++)
        {
            if (penetrateDic.TryGetValue((DamageType)i, out Penetrate penetrate))
            {
                this.penetrateDic.Add((DamageType)i, penetrate);
            }
            else
            {
                this.penetrateDic.Add((DamageType)i, new Penetrate((DamageType)i));
            }
        }
    }
    public Penetrate GetPenetrate(DamageType damageType)
    {
        return penetrateDic[damageType];
    }
}

