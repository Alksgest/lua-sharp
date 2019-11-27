using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using NLua;
using System.Text;
using System.ComponentModel;

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
            LuaVM.State.Encoding = Encoding.UTF8;
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
            //this.LuaVM.DoFile(_scriptFolder + "\\" + _mainScript);
            this.LuaVM.DoString(File.ReadAllText(_scriptFolder + "\\" + _mainScript, Encoding.UTF8));
        }

        [LuaFunction("PrintToConsole", "Print line to Console", new string[] { "line" })]
        public void PrintToConsole(object line) => Console.WriteLine(line);

        [LuaFunction("GetFiles", "Get files form dir", new string[] { "path" })]
        public string GetFiles(string path)
        {
            try
            {
                if (Directory.Exists(path))
                    return String.Join(",", Directory.GetFiles(path));
            }
            catch (UnauthorizedAccessException e)
            {
                PrintExceptionWithDelay(e);
            }
            return "";
        }

        [LuaFunction("GetLogicalDrives", "Return logical drives separeted by comma.")]
        public string GetLogicalDrives() => String.Join(",", Directory.GetLogicalDrives());

        [LuaFunction("GetDirectories", "GetDirectories", new string[] { "path" })]
        public string GetDirectories(string path)
        {
            try
            {
                if (Directory.Exists(path))
                    return String.Join(",", Directory.GetDirectories(path));
            }
            catch (UnauthorizedAccessException e)
            {
                PrintExceptionWithDelay(e);
            }

            return "";
        }

        [LuaFunction("RunFile", "Run file", new string[] { "filePath" })]
        public void RunFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    System.Diagnostics.Process.Start(filePath);
                }
                catch(Win32Exception e)
                {
                    PrintExceptionWithDelay(e);
                }
            }
        }

        [LuaFunction("CheckIsPathPresentFile", "CheckIsPathPresentFile", new string[] { "filePath" })]
        public bool CheckIsPathPresentFile(string filePath) => File.Exists(filePath);

        [LuaFunction("GetParentFolder", "GetParentFolder", new string[] { "path" })]
        public string GetParentFolder(string path)
        {
            var r = Directory.GetParent(path).FullName;
            return r;
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

        [LuaFunction("PrintLuaTable", "PrintLuaTable", new string[] { "table", "keyToHighlight" })]
        public void PrintLuaTable(object table, object keyToHighlight)
        {
            var res = table as LuaTable;
            foreach (var key in res.Keys)
            {
                if (keyToHighlight != null && (long)keyToHighlight == (long)key)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(res[key]);
                    Console.ResetColor();
                }
                else
                    Console.WriteLine(res[key]);
            }
        }

        private void PrintExceptionWithDelay(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n-=-=-=-=-=-=-=-=-=-=-=-=-\n");
            Console.WriteLine(e.Message);
            Console.WriteLine("Press any key to continue.   ");
            Console.WriteLine("\n-=-=-=-=-=-=-=-=-=-=-=-=-\n");
            Console.ResetColor();
            Console.ReadKey();
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