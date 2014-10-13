using SFML.Graphics;
using SFML.Window;

namespace BaseFunctions
{
    public interface I2DObject
    {
        void update(long elapsed);
        void update(Vector2f offset, long elapsed);
        void draw(RenderTarget rt, long elapsed);
    }
}
