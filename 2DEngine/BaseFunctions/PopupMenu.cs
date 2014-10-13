using SFML.Graphics;
using SFML.Window;

namespace BaseFunctions
{
    public class PopupMenu : PopupText
    {
        //TODO >
        // If I make ButtonList inherit BaseObject, there could be one loop to
        // draw/update every object.
        ButtonList buts;

        public PopupMenu(string inText, IntRect bounds, Color c, bool inActive = false)
            : base(inText, bounds, c, inActive)
        {
            buts = new ButtonList();
            spr.Position = GraphicsTools.Vectors2f.ExtractPos_IntRect(bounds);
            TextPos = spr.Position;
        }

        public override void update(Vector2f offset, long elapsed)
        {
            buts.update(offset);
        }

        public override void draw(SFML.Graphics.RenderTarget rt, long elapsed)
        {
            base.draw(rt, elapsed);

            buts.draw(rt);
        }

        public void AddButton(Button b)
        {
            buts.add(b);
        }
    }
}
