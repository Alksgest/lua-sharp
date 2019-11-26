﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using NLua;

namespace MainNamespace.LuaUtil
{
    class LuaStarter : IDisposable
    {
        private readonly Lua LuaVM = new Lua();

        private readonly string _scriptFolder = "lua";
        private readonly string _mainScript = "main.lua";
        private readonly FunctionRegistrator registrator = new FunctionRegistrator();

        private bool _isRuning = false;

        private bool disposedValue = false;

        public LuaStarter()
        {
            registrator.RegisterFunctions(this, LuaVM);
            RegisterModules();

            LuaVM.UseTraceback = true;
        }

        public void RunLoop()
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

        public void RunScripts()
        {
            ExecuteScripts();
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
            this.LuaVM.DoFile(_scriptFolder + "\\" + _mainScript);
        }

        [LuaFunction("PrintToConsole", "Print line to Console", new string[] { "line" })]
        public void PrintToConsole(object line) => Console.WriteLine(line);

        [LuaFunction("GetFiles", "Get files form dir", new string[] { "path" })]
        public string GetFiles(string path)
        {
            if (Directory.Exists(path))
                return String.Join(",", Directory.GetFiles(path));
            return "";
        }

        [LuaFunction("GetLogicalDrives", "Return logical drives separeted by comma.")]
        public string GetLogicalDrives() => String.Join(",", Directory.GetLogicalDrives());

        [LuaFunction("GetDirectories", "GetDirectories", new string[] { "path" })]
        public string GetDirectories(string path)
        {
            if (Directory.Exists(path))
                return String.Join(",", Directory.GetDirectories(path));
            return "";
        }

        [LuaFunction("HandleKeyPress", "Handle pressed key.")]
        public int HandleKeyPress()
        {
            var ch = Console.ReadKey().Key;
            switch (ch)
            {
                case ConsoleKey.UpArrow:
                    Console.WriteLine("Up arrow");
                    break;
                case ConsoleKey.DownArrow:
                    Console.WriteLine("Down arrow");
                    break;
            }
            return (int)ch;
        }

        [LuaFunction("PrintLuaTable", "PrintLuaTable", new string[] { "table" })]
        public void PrintLuaTable(object table, object keyToHighlight)
        {
            var res = table as LuaTable;
            foreach (var key in res.Keys)
            {
                if (keyToHighlight != null && (int)keyToHighlight == (int)key)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(res[key]);
                    Console.ResetColor();
                }
                else
                    Console.WriteLine(res[key]);
            }
        }

        [LuaFunction("CleareConsole", "Cleare console")]
        public void CleareConsole()
        {
            Console.Clear();
        }

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

