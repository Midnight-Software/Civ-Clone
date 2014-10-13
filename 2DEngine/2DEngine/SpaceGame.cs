#define loggingOFF //switch this to 'logON' for logging.

using System;     //Console.WriteLine
                  //      ".ReadKey
using System.Diagnostics; //StopWatch
using System.IO;          //StreamWriter
//using System.Threading;

using SFML.Audio;
using SFML.Graphics;
using SFML.Window;

using BaseFunctions;

namespace _2DEngine
{
    class SpaceGame
    {
        public void loadTextures()
        {
            //TODO >
            // Make this readonly(?)
            //TODO >
            // Give GraphicsTools a RenderWindow field that is a pointer, so I don't have to pass it around all the time.

            //Backgrounds
            GraphicsTools.textureDictionary.Add("title", new Texture(GraphicsTools.AssetPath("newBackGround.png")));
            //GraphicsTools.textureDictionary.Add("title", new Texture(GraphicsTools.AssetPath("Background.png")));
            GraphicsTools.textureDictionary.Add("options", new Texture(GraphicsTools.AssetPath("OptionsBG.png")));
            GraphicsTools.textureDictionary.Add("test", new Texture(GraphicsTools.AssetPath("TestBG.png")));

            //Tiles
            //Default
            GraphicsTools.textureDictionary.Add("default", new Texture(GraphicsTools.AssetPath("GameScreen\\Tiles\\TileTemplate.png")));
            GraphicsTools.textureDictionary.Add("defaultClick", new Texture(GraphicsTools.AssetPath("GameScreen\\Tiles\\TileTemplate_Click.png")));
            GraphicsTools.textureDictionary.Add("defaultHover", new Texture(GraphicsTools.AssetPath("GameScreen\\Tiles\\TileTemplate_Hover.png")));
            GraphicsTools.textureDictionary.Add("defaultSelected", new Texture(GraphicsTools.AssetPath("GameScreen\\Tiles\\TileTemplate_Selected.png")));
            //Platform
            GraphicsTools.textureDictionary.Add("platform", new Texture(GraphicsTools.AssetPath("GameScreen\\Tiles\\Platform.png")));
            GraphicsTools.textureDictionary.Add("platformClick", new Texture(GraphicsTools.AssetPath("GameScreen\\Tiles\\Platform_Click.png")));
            GraphicsTools.textureDictionary.Add("platformHover", new Texture(GraphicsTools.AssetPath("GameScreen\\Tiles\\Platform_Hover.png")));
            GraphicsTools.textureDictionary.Add("platformSelected", new Texture(GraphicsTools.AssetPath("GameScreen\\Tiles\\Platform_Selected.png")));
        }

        public void Run()
        {
            Vector2i activeResolution = GraphicsTools.ActiveResolution;
            RenderWindow rw = new RenderWindow(new VideoMode((uint)activeResolution.X, (uint)activeResolution.Y), "2DEngine", Styles.Fullscreen);
            rw.Position = new Vector2i(0, 0);
            rw.SetActive(true);
            rw.SetVerticalSyncEnabled(true);
            rw.Closed += rw_Closed;

            GraphicsTools.SetDirectories("../../../../GlobalAssets", "PNG_EXPORT", "Sounds", "Fonts");
            GraphicsTools.Initialize(rw);
            loadTextures();
            //just here for reference.
            //string mer = System.IO.Directory.GetCurrentDirectory();

            GameStateMachine game = new GameStateMachine();

            Stopwatch timer = new Stopwatch();
            timer.Start();
            long elapsed = GraphicsTools.DrawInterval;
#if loggingON
            DateTime tempTime1, tempTime2;
            using (StreamWriter sw = new StreamWriter("LOG.txt"))
#endif
            while (rw.IsOpen())
            {
                rw.DispatchEvents();

                //TODO > 
                // This is borked for the tiles and IDK why. Currently this is disabled so I don't
                // see the flashing tiles and popup menus, they are annoying.
                //
                // I think maybe I could ask Nathan Bean about this, see if he has any idea's for why
                // this is happening. Maybe talk to matt about it and see his opinion.
                //
                // Never mind, now it is happening regardless.
                //
                // The log i created does not show that the draw times are lagging, nor are the update speeds.
                // Maybe it has to do with something someone once told me about: double buffering. I should
                // look into this specifically to see if maybe that is what the root is.

                // Fixed, vertical sync.
                if (elapsed > GraphicsTools.DrawInterval)
                {
                    timer.Restart();
#if loggingON
                    tempTime1 = DateTime.Now;
                    sw.WriteLine("    DRAW START TIME : {0}", tempTime1.Millisecond);
#endif
                    game.update(elapsed);
#if loggingON
                    tempTime2 = DateTime.Now;
                    sw.WriteLine("      DRAW END TIME : {0}", tempTime2.Millisecond);
                    sw.WriteLine("  DRAW ELAPSED TIME : {0}", tempTime2.Millisecond - tempTime1.Millisecond);

                    tempTime1 = DateTime.Now;
                    sw.WriteLine("  UPDATE START TIME : {0}", tempTime1.Millisecond);
#endif
                    game.draw(rw, elapsed);
#if loggingON
                    tempTime2 = DateTime.Now;
                    sw.WriteLine("    UPDATE END TIME : {0}", tempTime2.Millisecond);
                    sw.WriteLine("UPDATE ELAPSED TIME : {0}", tempTime2.Millisecond - tempTime1.Millisecond);
                    sw.WriteLine();
#endif
                }

                    // TODO > 
                    //Thread.Yield();
                    elapsed = timer.ElapsedMilliseconds;
                }

            //Console.WriteLine("\nPlease press any key to exit.");
            //Console.ReadKey();
        }

        static void rw_Closed(object sender, EventArgs e)
        {
            ((RenderWindow)sender).Close();
        }
    }
}