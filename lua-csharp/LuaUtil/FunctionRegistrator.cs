using System;
using System.Reflection;
using NLua;

namespace MainNamespace.LuaUtil
{
    class FunctionRegistrator
    { 
        public void RegisterFunctions(object target, Lua luaVM)
        {
            if (luaVM == null)
                return;

            Type type = target.GetType();

            foreach (var mInfo in type.GetMethods())
            {
                foreach (var attr in Attribute.GetCustomAttributes(mInfo))
                {
                    if (attr.GetType() == typeof(LuaFunctionAttribute))
                    {
                        LuaFunctionAttribute func = (LuaFunctionAttribute)attr;

                        string fName = func.FunctionName;
                        string fDesck = func.FunctionDescription;
                        string[] parametres = func.FunctionParametres;

                        ParameterInfo[] pPrmInfo = mInfo.GetParameters();

                        if (parametres != null && parametres.Length != pPrmInfo.Length)
                        {
                            Console.WriteLine("Function " + mInfo.Name + " (exported as " +
                                fName + ") argument number mismatch. Declared " +
                                parametres.Length + " but requires " +
                                pPrmInfo.Length + ".");
                            break;
                        }

                        luaVM.RegisterFunction(fName, target, mInfo);
                    }
                }
            }
        }
    }

}

