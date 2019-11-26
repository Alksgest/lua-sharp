using System;
using System.Collections.Generic;
using System.IO;
using NLua;

namespace MainNamespace.LuaUtil
{
    class LuaStarter : IDisposable
    {
        private readonly Lua LuaVM = new Lua();
        private bool _isRuning;

        private readonly string _scriptPath = "lua";
        private readonly string _mainScript = "main.lua";
        private readonly FunctionRegistrator registrator = new FunctionRegistrator();

        public LuaStarter()
        {
            registrator.RegisterFunctions(this, LuaVM);
            RegisterModules();

            LuaVM.UseTraceback = true;
        }

        public void Run()
        {
            this._isRuning = true;

            string input;

            while (_isRuning)
            {
                try
                {
                    Console.WriteLine("> ");

                    input = Console.ReadLine();
                    Console.WriteLine();

                    LuaVM.DoString(input);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void RegisterModules()
        {
            //var currentDir = Directory.GetCurrentDirectory();

            //string fileMask = "%lua.lua%";

            //var pathPattern = "'%lua.lua%'";
            //var commandPattern = "package.path = ";

            //foreach (var file in Directory.GetFiles(currentDir + "\\" + _scriptPath))
            //{
            //    var fileName = file.Split('\\')[file.Split('\\').Length - 1];
            //    if (fileName != _mainScript)
            //    {
            //        var command = commandPattern + pathPattern.Replace(fileMask, file).Replace("\\", "/");
            //        this.LuaVM.DoString(command);
            //    }
            //}
        }

        [LuaFunction("Quit", "Exit the program.")]
        public void Quit()
        {
            this._isRuning = false;
        }

        [LuaFunction("ExecuteScripts", "Execute scripts on path lua-scripts/main.lua")]
        public void ExecuteScripts()
        {
            this.LuaVM.DoFile(_scriptPath + "\\" + _mainScript);
        }

        [LuaFunction("PrintToConsole", "Print line to Console", new string[] { "line" })]
        public void PrintToConsole(object line)
        {
            Console.WriteLine(line);
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.LuaVM.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }

}

