using System; //Exception
using System.Text; //stringbuilder
using System.Collections.Generic; //list

using SFML.Graphics;
using SFML.Window;

namespace BaseFunctions
{
    public class GraphicsTools
    {
        #region Exceptions
        public class AssetException : Exception 
        {
            public AssetException() { }
            public AssetException(string msg) 
                : base(msg) { }
        }

        public class InvalidDimensionException : Exception
        {
            public InvalidDimensionException() { }
            public InvalidDimensionException(string msg) 
                : base(msg) { }
        }

        public class InvalidPathException : Exception
        {
            public InvalidPathException() { }
            public InvalidPathException(string msg)
                : base(msg) { }
        }

        public class NotImplementedException : Exception
        {
            public NotImplementedException() { }
            public NotImplementedException(string msg)
                : base(msg) { }
        }
        #endregion

        //TODO > All of the vectors should be readonly (i would just use getters)
        public class Vectors2f
        {
            //
            private static readonly Vector2f[] vectors = {new Vector2f(0, 0), new Vector2f(1, 1) };
            private static bool init = false;

            public static Vector2f Zero
            {
                get
                {
                    return vectors[0];
                }
            }

            public static Vector2f One 
            {
                get
                {
                    return vectors[1];
                }
            }

            public static Vector2f Convert2i(Vector2i input)
            {
                return new Vector2f(input.X, input.Y);
            }

            public static Vector2f ExtractPos_IntRect(IntRect inRect)
            {
                return new Vector2f(inRect.Left, inRect.Top);
            }

            public static Vector2f ExtractSize_IntRect(IntRect inRect)
            {
                return new Vector2f(inRect.Width, inRect.Height);
            }
        }

        public enum Resolution { _800x600, _1024x768, _1024x700, _1024x640, _1366x768 }
        public enum FileType { Image, Font, Sound }
        public enum Fonts { Techsyna }

        public static Dictionary<string, Texture> textureDictionary;

        //TODO >
        // Currently this returns the pointer which can be changed. What this really should be 
        // doing is using a remote class that wraps this RenderWindow and then you call its 
        // functions to access the RenderWindow.
        public static RenderWindow rw { get; private set; }

        //right now, this cannot be changed.
        private static Resolution resolution = Resolution._1366x768;

        private static bool init = false;
        private static int drawInterval = 20; //This is very slightly faster than 60 FPS.

        //these fields are currently private, only to be accessed by the method.
        //this is so that if they are not initialized, then you get an exception thrown.
        private static string AssetDirectory { get; set; }
        private static string ImageDirectory { get; set; }
        private static string SoundDirectory { get; set; }
        private static string FontDirectory { get; set; }

        private static List<Font> fonts;

        public static void Initialize(RenderWindow rwIn)
        {
            fonts = new List<Font>();
            fonts.Add(new Font(AssetPath("\\" + ((Fonts)0).ToString() + "\\" + ((Fonts)0).ToString() + ".ttf", FileType.Font)));
            textureDictionary = new Dictionary<string, Texture>();
            rw = rwIn;
        }

        public static Font GetFont(Fonts f)
        {
            return fonts[(int)f];
        }

        /// <summary>
        /// Order of the parameters: Main, Image, Sound, Font.
        /// </summary>
        /// <param name="dirs"></param>
        public static void SetDirectories(params string[] dirs)
        {
            //TODO >
            // Fix params (Matt's suggestion)

            if (dirs.Length != 4) throw new InvalidDimensionException("You didn't give enough directory paths");
            if (System.IO.Directory.Exists(dirs[0]))
                AssetDirectory = dirs[0];
            else
                throw new InvalidPathException("Main Asset Directory Path is not Valid.");
            
            if(System.IO.Directory.Exists(AssetDirectory + "\\" + dirs[1]))
                ImageDirectory = AssetDirectory + "\\" + dirs[1];
            else
                throw new InvalidPathException("Image Directory Path is not Valid.");

            if (System.IO.Directory.Exists(AssetDirectory + "\\" + dirs[2]))
                SoundDirectory = AssetDirectory + "\\" + dirs[2];
            else
                throw new InvalidPathException("Sound Directory Path is not Valid.");

            if (System.IO.Directory.Exists(AssetDirectory + "\\" + dirs[3]))
                FontDirectory = AssetDirectory + "\\" + dirs[3];
            else
                throw new InvalidPathException("Font Directory Path is not Valid.");

            init = true;
        }

        public static Vector2i ActiveResolution
        {
            get
            {
                switch (resolution)
                {
                    case Resolution._800x600:
                        return new Vector2i(800, 600);
                    case Resolution._1024x640:
                        return new Vector2i(1024, 640);
                    case Resolution._1024x700:
                        return new Vector2i(1024, 700);
                    case Resolution._1024x768:
                        return new Vector2i(1024, 768);
                    case Resolution._1366x768:
                        return new Vector2i(1366, 768);
                    default:
                        throw new AssetException("Unrecognized Resolution Selected...");
                }
            }
        }

        public static int DrawInterval
        {
            get { return drawInterval; }
        }

        public static string AssetPath(string assetName, FileType type = FileType.Image)
        {
            if (init)
            {
                switch (type)
                {
                    case FileType.Font:
                        return (FontDirectory + "\\" + assetName);
                    case FileType.Image:
                        return (ImageDirectory + "\\" + assetName);
                    case FileType.Sound:
                        return (SoundDirectory + "\\" + assetName);
                    default:
                        throw new GraphicsTools.AssetException("Unknown Asset Type.");
                }
            }
            else
                throw new GraphicsTools.AssetException("AssetDirectory not initialized. Call to SetDirectories first.");
        }

        public static string SubDirectory(string assetName, params string[] directoryNames)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(AssetPath("")); //gets current directory by itself
                                      //+ it will throw exception if it isnt initialized
            foreach (string s in directoryNames)
                sb.Append(s + "\\");
            sb.Append(assetName);
            return (sb.ToString());
        }

        public static Texture getColoredTexture(int width, int height, Color c)
        {
            return new Texture(new Image((uint)width, (uint)height, c));
        }
    }
}

