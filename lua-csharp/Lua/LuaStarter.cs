using System;
using System.Collections.Generic;
using System.IO;
using NLua;

namespace MainNamespace.LuaWrapper
{
    class LuaStarter : IDisposable
    {
        private readonly Lua LuaVM = new Lua();
        private bool _isRuning;

        private readonly string _scriptPath = "lua-scripts";
        private readonly FunctionRegistrator registrator = new FunctionRegistrator();

        public LuaStarter()
        {
            registrator.RegisterFunctions(this, this.LuaVM);
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

        [LuaFunction("Quit", "Exit the program.")]
        public void Quit()
        {
            this._isRuning = false;
        }

        [LuaFunction("ExecuteScripts", "Execute scripts on path lua-scripts/*.lua")]
        public void ExecuteScripts()
        {
            string[] files = GetLuaScripts();
            foreach (var file in files)
            {
                object[] result = this.LuaVM.DoFile(file);
            }
        }

        [LuaFunction("PrintToConsole", "Print line to Console", new string[] { "line" })]
        public void PrintToConsole(string line)
        {
            Console.WriteLine(line);
        }

        private string[] GetLuaScripts()
        {
            if (Directory.Exists(this._scriptPath))
            {
                return Directory.GetFiles(this._scriptPath);
            }
            return null;
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

