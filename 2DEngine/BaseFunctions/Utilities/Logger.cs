using System;
using System.IO;

namespace BaseFunctions.Utilities
{
    public class Logger
    {
        #region Embedded Types
        
        public enum FileTarget
        {
            mainLog = 0,
            networkLog, //1
            graphicsLog, //2
            errorLog, //3
            COUNT //4
        }

        #endregion //Embedded Types

        #region Private Member Variables

        static private string mLoggingDirectory = "../../../Logs";

        static private string[] mFileNames = { "main.log",
                                               "net.log",
                                               "graphics.log",
                                               "error.log" };

        //create the correct number of file streams based on how many there are in the enum
        static private StreamWriter[] mFileStreams = new StreamWriter[(int)FileTarget.COUNT];

        static private bool mInit = false;

        #endregion //Private Member Variables

        /// <summary>
        /// This function will make sure that all of the files have been created,
        /// and that the streams have been opened in append mode.
        /// </summary>
        public static void Initialize()
        {
            if (!mInit) //redundant for safety.
            {
                string lRealLogPath = Path.GetDirectoryName(mLoggingDirectory);
                //make sure that the logging directory exists.
                if (!Directory.Exists(lRealLogPath))
                {
                    Directory.Delete(lRealLogPath, true);
                    Directory.CreateDirectory(lRealLogPath);
                }

                //make sure that each file stream is initialized properly.
                for (int i = 0; i < (int)FileTarget.COUNT; i++)
                {
                    mFileStreams[i] = new StreamWriter(mFileNames[i], true);
                    //write a header for this session.
                    mFileStreams[i].WriteLine("\n\nStart of {0}, {1}, {2}", mFileNames[i], DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());
                }
                mInit = true;
            }
        }

        public static void Write(string aStr, FileTarget aTarget)
        {
            if (!mInit) Initialize();
            mFileStreams[(int)aTarget].Write(aStr);
        }

        public static void WriteLine(string aStr, FileTarget aTarget)
        {
            if (!mInit) Initialize();
            mFileStreams[(int)aTarget].WriteLine(aStr);
        }

        //TODO >
        //add a destructor or something so that the streams will close?
        //check documentation to see if when the streams are collected
        //by the gc if they are closed automatically.
    }
}
