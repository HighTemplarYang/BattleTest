using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class StaticManager
{
    public static int BattlePropertyCount;

    static StaticManager()
    {
        BattlePropertyCount = 4;
    }
}

