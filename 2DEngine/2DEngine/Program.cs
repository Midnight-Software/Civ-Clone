using BaseFunctions.Utilities;

namespace _2DEngine
{
    //NOTE: These libraries require a 64 bit processor.
    class Program
    {
        static void Main(string[] args)
        {
            Logger.WriteLine("Starting up...", Logger.FileTarget.mainLog);
            
            SpaceGame sg = new SpaceGame();
            sg.Run();

            Logger.WriteLine("Shutting down...\n", Logger.FileTarget.mainLog);
        }
    }
}
