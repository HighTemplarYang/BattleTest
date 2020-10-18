using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour
{
    public int BaseCoolDown;
    public BattleActionGroup SkillActions;

    

    public void Settlement()
    {
        SkillActions.Act();
    }


}
