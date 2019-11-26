using MainNamespace.LuaUtil;

namespace MainNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var starter = new LuaStarter())
            {
                starter.RunScripts();
            }
        }
    }
}
