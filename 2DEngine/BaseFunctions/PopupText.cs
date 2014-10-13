using SFML.Graphics;
using SFML.Window;

namespace BaseFunctions
{
    public class PopupText : BaseObject
    {
        public void SetTextScale(Vector2f scale)
        {
            text.Scale = scale;
        }

        private string hoverText;
        public string HoverText 
        {
            get
            {
                return hoverText;
            }
            set
            {
                hoverText = value;
                text.DisplayedString = hoverText;
                //TODO >
                // This TODO is meant to remind me that currently all text must be manually justified.
                //wrapText(200, text);
            }
        }
        private Text text;

        public Vector2f TextPos
        {
            get
            {
                return text.Position;
            }
            set
            {
                text.Position = value;
            }
        }

        public bool Active { get; set; }

        public PopupText(string inText)
            : base(new Vector2f(400, 400))
        {
            text = new Text("", GraphicsTools.GetFont(GraphicsTools.Fonts.Techsyna));
            HoverText = inText;
            text.Scale = new Vector2f(.5f, .5f);
            Active = false;
        }

        public PopupText(string inText, IntRect bounds, Color c, bool inActive = false)
            : base(GraphicsTools.Vectors2f.ExtractSize_IntRect(bounds), c)
        {
            text = new Text("", GraphicsTools.GetFont(GraphicsTools.Fonts.Techsyna));
            HoverText = inText;
            text.Scale = new Vector2f(.5f, .5f);
            Active = inActive;
        }

        private void wrapText(long length, Text input)
        {
            for (uint charPos = 0; charPos < input.DisplayedString.Length; charPos++)
            {
                if (input.FindCharacterPos(charPos).X >= length / input.Scale.X)
                    input.DisplayedString = input.DisplayedString.Insert((int)charPos, "\n");
            }
        }

        public void setBounds(Vector2f size)
        {
            InitializeTextures(size, Color.Red);
        }

        public override void update(long elapsed)
        {
            if (!Active) return;
            if (HoverText == string.Empty) return;
            Vector2i save = GraphicsTools.rw.Position;
            spr.Position = GraphicsTools.Vectors2f.Convert2i(Mouse.GetPosition() - new Vector2i(50, 50));
            text.Position = spr.Position;
        }

        public override void draw(RenderTarget rw, long elapsed)
        {
            if (!Active) return;
            if (HoverText == string.Empty) return;
            base.draw(rw, elapsed);
            rw.Draw(text);
        }
    }
}
