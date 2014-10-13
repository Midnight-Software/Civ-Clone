using SFML.Graphics;
using SFML.Window;

namespace BaseFunctions
{
    public interface IScreen : IState
    {
        void AddButton(Button inBut);
        void AddButton(Texture inTex, Vector2f inPos);
        void AddButton(string path, Vector2f inPos);
        void AddObject(I2DObject o);
    }
}
