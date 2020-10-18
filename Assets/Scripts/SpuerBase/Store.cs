using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Store<T>
{
    public static Dictionary<int, T> StoreDic;

    public virtual void Init()
    {
        StoreDic = new Dictionary<int, T>();
    }

}

