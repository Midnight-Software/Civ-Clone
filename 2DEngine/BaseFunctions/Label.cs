using SFML.Graphics;
using SFML.Window;

namespace BaseFunctions
{
    public class Label : BaseObject
    {
        private string displayedText;
        public string DisplayedText
        {
            get
            {
                return displayedText;
            }
            set
            {
                displayedText = value;
                text.DisplayedString = displayedText;
                //TODO >
                // This TODO is meant to remind me that currently all text must be manually justified.
                //wrapText(200, text);
            }
        }
        private Text text;

        public Label(Vector2f inSize, Vector2f inPos, string inText = "MERMERMER, MERMERMERMERMER.")
            : base(inSize, Color.Green)
        {
            spr.Position = inPos;
            text = new Text("", GraphicsTools.GetFont(GraphicsTools.Fonts.Techsyna));
            text.Position = inPos;
            DisplayedText = inText;
            text.Scale = new Vector2f(.5f, .5f);
        }

        public override void update(Vector2f offset, long elapsed)
        {
            //to avoid exception
        }

        public override void draw(RenderTarget rw, long elapsed)
        {
            base.draw(rw, elapsed);
            rw.Draw(text);
        }
    }
}
