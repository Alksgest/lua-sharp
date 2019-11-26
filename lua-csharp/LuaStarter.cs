using System;
using System.Collections.Generic;
using System.Reflection;
using NLua;

namespace lua_csharp
{
    class LuaStarter: IDisposable
    {
        private readonly Lua LuaVM;
        private bool _isRuning;

        public LuaStarter()
        {
            this.LuaVM = new Lua();

            this.LuaVM.RegisterFunction("PrintSmth", this, this.GetType().GetMethod("PrintSmth"));
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

        [LuaFunction("PrintSmth", "PrintSmth.")]
        public void PrintSmth()
        {
            Console.WriteLine("PrintSmth is working.");
        }

        private bool disposedValue = false; // To detect redundant calls

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
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        //private void RegisterFunctions()
        //{
        //    if (this.LuaVM == null)
        //        return;

        //    Type type = this.GetType();

        //    foreach (var mInfo in type.GetMethods())
        //    {
        //        foreach (var attr in Attribute.GetCustomAttributes(mInfo))
        //        {
        //            if (attr.GetType() == typeof(LuaFunctionAttribute))
        //            {
        //                LuaFunctionAttribute func = (LuaFunctionAttribute)attr;

        //                string fName = func.FunctionName;
        //                string fDesck = func.FunctionDescription;
        //                string[] parametres = func.FunctionParametres;

        //                ParameterInfo[] pPrmInfo = mInfo.GetParameters();

        //                if (parametres != null && parametres.Length != pPrmInfo.Length)
        //                {
        //                    Console.WriteLine("Function " + mInfo.Name + " (exported as " +
        //                        fName + ") argument number mismatch. Declared " +
        //                        parametres.Length + " but requires " +
        //                        pPrmInfo.Length + ".");
        //                    break;
        //                }

        //                this.LuaVM.RegisterFunction(fName, type, mInfo);
        //            }
        //        }
        //    }
        //}
    }
}

