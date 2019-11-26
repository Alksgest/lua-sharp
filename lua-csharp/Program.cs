using MainNamespace.LuaWrapper;

namespace MainNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var starter = new LuaStarter())
            {
                starter.Run();
            }
        }
    }

}
