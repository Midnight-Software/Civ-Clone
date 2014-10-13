using System.Collections.Generic; //List

using SFML.Graphics;
using SFML.Window; //Vector

namespace BaseFunctions
{
    public class ButtonList
    {
        List<Button> buttons;
        bool init = false;

        public int LastIndex { get { return buttons.Count - 1; } }

        public ButtonList()
        {
            buttons = new List<Button>();
            init = true;
        }

        public void add(Button inBut)
        {
            buttons.Add(inBut);
        }

        public void add(Texture inTex, Vector2f inPos)
        { 
            //buttons.Add(new Button(inTex, inPos));
        }

        public void add(string path, Vector2f inPos)
        { 
            buttons.Add(new Button(path, inPos));
        }

        public void update(Vector2f offset)
        {
            foreach (Button b in buttons)
            {
                b.update(offset, 0);
            }
        }

        public void draw(RenderTarget rw)
        {
            if (!init) return;
            foreach (Button b in buttons)
                b.draw(rw, 0);
        }
    }
}
