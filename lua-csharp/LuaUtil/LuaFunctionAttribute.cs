using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainNamespace.LuaUtil
{
    class LuaFunctionAttribute : Attribute
    {
        public string FunctionName { get; }
        public string FunctionDescription { get; }
        public string[] FunctionParametres { get; } = null;

        public LuaFunctionAttribute(string functionName, string functionDescription)
        {
            this.FunctionName = functionName;
            this.FunctionDescription = functionDescription;
        }

        public LuaFunctionAttribute(string fName, string fDescription, string[] fParametres)
        {
            this.FunctionName = fName;
            this.FunctionDescription = fDescription;
            this.FunctionParametres = fParametres;
        }

    }
}
