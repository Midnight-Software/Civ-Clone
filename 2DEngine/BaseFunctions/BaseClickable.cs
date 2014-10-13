using System;   //EventArgs
using System.IO;

using SFML.Graphics;
using SFML.Window;

namespace BaseFunctions
{
    public class BaseClickable : I2DObject
    {
        public string name;

        public enum State { Click, Hover, Idle, Selected }

        public State state = State.Idle;
        Texture[] tex = new Texture[4];
        protected Sprite spr;
        protected bool init = false;

        protected EventArgs clickEventArgs;

        public Vector2f Position
        {
            get { return spr.Position; }
            private set { spr.Position = value; }
        }

        public BaseClickable()
        {
            //TODO > Get this consolidated so that the inherited classes behave properly.

            // currently disabled, TextButton needs to use this for the moment.
            //throw new GraphicsTools.AssetException("MUST INITIALIZE ASSETS");
        }

        public virtual void InitializeAssets(string path, string helpText = "")
        {
            //if all the textures dont load
            if (!(GraphicsTools.textureDictionary.TryGetValue(path, out tex[0]) &&
                  GraphicsTools.textureDictionary.TryGetValue(path + "Click", out tex[1]) &&
                  GraphicsTools.textureDictionary.TryGetValue(path + "Hover", out tex[2]) &&
                  GraphicsTools.textureDictionary.TryGetValue(path + "Selected", out tex[3])))
            {
                throw new GraphicsTools.AssetException();
            }
            clickEventArgs = EventArgs.Empty;
        }

        /// <summary>
        /// Initializes the button, assuming "*.png", "*_Click.png" and "*_Hover.png"
        /// </summary>
        /// <param name="path"></param>
        public BaseClickable(string path)
        {
            if (File.Exists(path))
                try
                {
                    InitializeAssets(path);
                    spr = new Sprite(tex[0]);
                    Position = GraphicsTools.Vectors2f.Zero;
                    init = true;
                }
                catch (GraphicsTools.AssetException e)
                {
                    init = false;
                    throw new GraphicsTools.AssetException("Must give valid path name for assets");
                }
        }

        public BaseClickable(string path, Vector2f inPos, string inName = "")
        { 
            try
            {
                InitializeAssets(path);
                spr = new Sprite(tex[0]);
                Position = inPos;
                name = inName;
                init = true;
            }
            catch (GraphicsTools.AssetException e)
            {
                init = false;
                throw new GraphicsTools.AssetException("Must give valid path name for assets");
            }
        }

        public virtual bool checkMouse(Vector2f offset)
        {
            Vector2i mp = SFML.Window.Mouse.GetPosition(GraphicsTools.rw);
            Vector2f topLeft = spr.Position + offset;
            FloatRect bounds = spr.GetLocalBounds();

            if (mp.X >= topLeft.X &&
                mp.X <= (topLeft.X + bounds.Width) &&
                mp.Y >= topLeft.Y &&
                mp.Y <= (topLeft.Y + bounds.Height))
                return true;

            return false;
        }

        public delegate void ClickedEventHandler(object sender, EventArgs e);
        public event ClickedEventHandler clicked;

        protected virtual void OnClicked(EventArgs e)
        {
            if (clicked!= null)
                clicked(this, e);
        }

        private void updateActiveTex()
        {
            //pre condition: init is true
            //post condition: the correct texture is assigned to spr
            switch (state)
            {
                case State.Idle:
                    spr.Texture = tex[0];
                    break;
                case State.Click:
                    spr.Texture = tex[1];
                    break;
                case State.Hover:
                    spr.Texture = tex[2];
                    break;
                case State.Selected:
                    spr.Texture = tex[3];
                    break;
            }
        }

        public virtual void update(long elapsed)       
        {
            update(GraphicsTools.Vectors2f.Zero, elapsed);
        }

        public virtual void update(Vector2f offset, long elapsed)
        {
            if (!init) return;
            if (!(state == State.Selected))
                if (!checkMouse(offset))
                {
                    state = State.Idle;
                }
                else 
                {
                    if (Mouse.IsButtonPressed(Mouse.Button.Left))
                        state = State.Click;
                    else
                    {
                        if (state == State.Click)
                            OnClicked(clickEventArgs);
                        else
                            state = State.Hover;
                    }
                }
            updateActiveTex();
        }
                
        public virtual void draw(RenderTarget rt, long elapsed)
        {
            if (!init) return;
            rt.Draw(spr);
        }
    }
}