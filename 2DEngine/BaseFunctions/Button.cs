using SFML.Graphics;
using SFML.Window; //Vector2i

namespace BaseFunctions
{
    //TODO : Make the state part of this use a StateMachine of some sort
    public class Button : BaseClickable
    {
        public Button()
            : base()
        { }

        /// <summary>
        /// Initializes the button, assuming "*.png", "*_Click.png" and "*_Hover.png"
        /// </summary>
        /// <param name="path"></param>
        public Button(string path)
            : base(path)
        {
        }

        public Button(string path, Vector2f inPos, string inName = "")
            : base(path, inPos, inName)
        { 
        }
    }
}
