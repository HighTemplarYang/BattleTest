using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class WeaponStore:Store<Weapon>
{
    public override void Init()
    {
        base.Init();
        //武器0，空手
        Dictionary<DamageType, Penetrate> tempPenetrateDic=new Dictionary<DamageType, Penetrate>();
        tempPenetrateDic.Add(DamageType.Kinect, new Penetrate(DamageType.Kinect, 5, 0));
        Weapon tempWeapon = new Weapon(0, "空手", 1f, 1f, 0.75f, 0.75f, new PenetrateGroup(tempPenetrateDic));


    }

}

