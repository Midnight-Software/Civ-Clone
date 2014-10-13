using SFML.Graphics;
using SFML.Window;

namespace BaseFunctions
{
    public interface IState
    {
        void update(Vector2f offset, long elapsed);
        void draw(RenderTarget rt, long elapsed);
    }
}
