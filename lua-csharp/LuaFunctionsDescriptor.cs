using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lua_csharp
{
    class LuaFunctionsDescriptor
    {

        public string FunctionName { get; }
        public string FunctionDescription { get; }
        public List<string> FunctionParameters { get; }
        public List<string> FunctionParamDocs { get; }
        public string FunctionDocString { get; }

        public LuaFunctionsDescriptor(string fName, string fDescription, List<string> fParameters, 
            List<string> fParamDocs)
        {
            FunctionName = fName;
            FunctionDescription = fDescription;
            FunctionParameters = fParameters;
            FunctionParamDocs = fParamDocs;

            string strFuncHeader = fName + "(%params%) - " + fDescription;
            string strFuncBody = "\n\n";
            string strFuncParams = "";

            bool first = true;

            for(int i = 0; i < fParameters.Count; ++i)
            {
                if(!first)
                {
                    strFuncParams += ", ";

                    
                }
            }
        }

    }
}
