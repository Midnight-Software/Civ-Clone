using System.Collections;
using System.Collections.Generic;

using SFML.Graphics;
using SFML.Window;

namespace BaseFunctions
{
    public class ObjectList : IEnumerable
    {
        List<I2DObject> objects;

        public ObjectList()
        {
            objects = new List<I2DObject>();
        }

        public void add(I2DObject o)
        {
            objects.Add(o);
        }

        public void update(long elapsed)
        {
            update(new Vector2f(0, 0), elapsed);
        }

        public void update(Vector2f offset, long elapsed)
        {
            foreach (I2DObject o in objects)
                o.update(offset, elapsed);
        }

        public void draw(RenderTarget rw)
        {
            foreach (I2DObject o in objects)
                o.draw(rw, 0);
        }

        public IEnumerator GetEnumerator()
        {
            foreach (I2DObject i2D in objects)
                yield return i2D;
        }
    }
}
