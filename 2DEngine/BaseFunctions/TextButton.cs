using SFML.Graphics;
using SFML.Window;

namespace BaseFunctions
{
    /// <summary>
    /// This class encapsulates an object that represents a button with variable text
    /// </summary>
    public class TextButton : Button
    {
        RenderTexture rt;
        Text t;
        Font f;
        Vector2f pos;
        Color[] c = new Color[3];

        public IntRect Bounds { get; private set; }

        public TextButton(string text, IntRect bounds, Vector2f scale)
            : base()
        {
            pos = new Vector2f(bounds.Left, bounds.Top);
            rt = new RenderTexture((uint)bounds.Width, (uint)bounds.Height);
            t = new Text(text, GraphicsTools.GetFont(GraphicsTools.Fonts.Techsyna));
            t.Scale = scale;
            spr = new Sprite(rt.Texture);
            initColors(Color.Cyan, Color.Blue, Color.Red);
            name = text;
            Bounds = bounds;
        }

        private void initColors(Color click, Color hover, Color idle)
        {
            c[0] = click;
            c[1] = hover;
            c[2] = idle;
            init = true;
        }

        public override void draw(RenderTarget rw, long elapsed)
        {
            rt.Clear(c[(int)state]);
            rt.Draw(t);
            rt.Display();
            spr.Texture = rt.Texture;
            spr.Position = pos;
            rw.Draw(spr);
        }
    }
}
