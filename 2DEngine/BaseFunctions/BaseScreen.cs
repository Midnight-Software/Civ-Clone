using System;   // NotImplementedException
using System.IO;// File Operations

using SFML.Graphics;
using SFML.Window; //vector

namespace BaseFunctions
{
    public class BaseScreen : IScreen
    {
        protected bool init = false;

        Texture tex;
        protected Sprite spr;

        protected ButtonList buttons;
        protected ObjectList objects;

        public BaseScreen()
        {
            // removed this so that classes that inherit this can call the no argument constructor.
            //throw new GraphicsTools.AssetException("MUST ASSIGN BACKGROUND TEXTURE");
            buttons = new ButtonList();
            objects = new ObjectList();            
        }
        
        protected void initTex(string path)
        {
            try
            {
                if (!(GraphicsTools.textureDictionary.TryGetValue(path, out tex))) 
                    throw new NotImplementedException();
                spr = new Sprite(tex);
            }
            catch
            {
                throw new GraphicsTools.AssetException("Failed to initialize background.");
            }
            init = true;
        }

        protected void initTex(Texture inTex)
        {
            try
            {
                tex = inTex;
                spr = new Sprite(tex);
            }
            catch
            {
                throw new GraphicsTools.AssetException("Failed to initialize background.");
            }
            init = true;
        }

        public BaseScreen(string path)
            : this()
        {
            initTex(path);
        }

        public BaseScreen(Texture inTex)
            : this()
        {
            initTex(inTex);
        }

        public void AddButton(Button inBut)
        {
            buttons.add(inBut);
        }

        public void AddButton(Texture inTex, Vector2f inPos)
        {
            buttons.add(inTex, inPos);
        }

        public void AddButton(string path, Vector2f inPos)
        {
            buttons.add(path, inPos);
        }

        public void AddObject(I2DObject o)
        {
            objects.add(o);
        }

        public virtual void update(Vector2f offset, long elapsed)
        {
            buttons.update(offset);
            objects.update(offset, elapsed);
        }

        public virtual void draw(RenderTarget rt, long elapsed)
        {
            if (!init) return;
            rt.Draw(spr);
            buttons.draw(rt);
            objects.draw(rt);
        }
    }
}
