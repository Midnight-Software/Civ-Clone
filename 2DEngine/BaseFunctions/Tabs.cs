using System;
using System.Collections.Generic;

using SFML.Graphics;
using SFML.Window;

namespace BaseFunctions
{
    public class Tabs
    {
        StateMachine tabs;
        ButtonList buts;
        Dictionary<string, int> buttonLookup;

        public IntRect bounds { get; set; }
        private RenderTexture _rt;
        private Sprite spr;

        public Tabs(IntRect inBounds)
        {
            tabs = new StateMachine();
            buts = new ButtonList();
            buttonLookup = new Dictionary<string, int>();
            bounds = inBounds;
            _rt = new RenderTexture((uint)bounds.Width, (uint)bounds.Height);
            spr = new Sprite(_rt.Texture);
        }

        public void AddTab(string tabName, Button inBut, BaseScreen tabScreen)
        {
            buts.add(inBut);
            inBut.clicked += handleButtons;
            buttonLookup.Add(tabName, buts.LastIndex);
            tabs.addState(tabScreen);
        }

        private void handleButtons(object sender, EventArgs e)
        { 
            int index = 0;
            if (buttonLookup.TryGetValue(((Button)sender).name, out index))
                tabs.Active = index;
        }

        public void update(Vector2f offset, long elapsed)
        {
            tabs.update(offset, elapsed);
        }

        public void draw(RenderTarget rt, long elapsed)
        {
            _rt.Clear();
            tabs.drawTex(_rt, elapsed);
            _rt.Display();
            spr.Texture = _rt.Texture;
            spr.Position = new Vector2f(0, 50);
            rt.Draw(spr);
        }
    }
}
