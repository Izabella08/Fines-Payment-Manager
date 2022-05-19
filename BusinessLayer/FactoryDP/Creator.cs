using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.FactoryDP
{
    public abstract class Creator
    {
        public abstract void createWriter(string method);
    }
}
