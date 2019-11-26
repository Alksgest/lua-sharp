﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lua_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread luaThread;
            using (LuaStarter starter = new LuaStarter())
            {
                luaThread = new Thread(starter.Run);

                luaThread.Start();
                luaThread.Join();
            }
        }
    }

}
