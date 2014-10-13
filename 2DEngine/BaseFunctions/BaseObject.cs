using System.Collections.Generic;

using SFML.Graphics;
using SFML.Window;

namespace BaseFunctions
{
    public class BaseObject : I2DObject
    {
        protected bool init = false;
        long animationTime = 20;
        long timePassed;
        int active, numFrames;

        protected Sprite spr;
        protected Texture[] tex;

        protected virtual void InitializeTextures(Vector2f size, Color c)
        {
            try
            {
                spr = new Sprite();
                tex = new Texture[1];
                Image mer = new Image((uint)size.X, (uint)size.Y, c);
                tex[0] = new Texture(mer);
                spr.Texture = tex[0];
                init = true;
            }
            catch
            {
                throw new GraphicsTools.AssetException("INTERNAL ERROR");
            }
        }

        //TODO >
        //none of this method works for the animation shit
        protected virtual void InitializeTextures(string path, Vector2f pos, int num)
        {
            try
            {
                tex = new Texture[num];
                if (num != 1)
                {
                    throw new System.NotImplementedException();
                    for (int i = 0; i < num; i++)
                    {
                        if (!(GraphicsTools.textureDictionary.TryGetValue(path, out tex[i])))
                            throw new System.NotImplementedException();

                    }
                }
                else
                {
                    if (!(GraphicsTools.textureDictionary.TryGetValue(path, out tex[0])))
                        throw new System.NotImplementedException();
                }
                spr = new Sprite(tex[0]);
                //spr.Scale = new Vector2f(5, 5);
                spr.Position = pos;
                init = true;
            }
            catch
            {
                throw new GraphicsTools.AssetException();
            }
        }

        public BaseObject(Vector2f size)
        {
            InitializeTextures(size, Color.Red);
        }

        public BaseObject(Vector2f size, Color c)
        {
            InitializeTextures(size, c);
        }

        public BaseObject(string path, Vector2f pos, int num)
        {
            if (path == string.Empty) return;
            InitializeTextures(path, pos, num);
            numFrames = num;
        }

        public virtual void update(long elapsed)
        {
            timePassed += elapsed;
            if (timePassed > animationTime)
            {
                active++;
                if (active == numFrames)
                    active = 0;
                spr.Texture = tex[active];
                timePassed = 0;
            }
        }

        public virtual void update(Vector2f offset, long elapsed)
        {
            throw new GraphicsTools.NotImplementedException("MER?MER.");
        }           

        public virtual void draw(RenderTarget rt, long elapsed)
        {
            if (!init) return;
            rt.Draw(spr);
        }
    }
}
