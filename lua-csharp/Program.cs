using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lua_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (LuaStarter starter = new LuaStarter())
            {
                starter.Run();
            }
        }
    }

}
