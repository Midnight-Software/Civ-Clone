using System;
using System.Collections.Generic;

using SFML.Graphics;
using SFML.Window;

namespace BaseFunctions
{
    public class StateMachine
    {
        protected List<IState> states;
        public int Active { get { return active; } set { active = value; } }
        protected int active;

        public StateMachine()
        {
            states = new List<IState>();
        }

        public void addState(IState inState)
        {
            states.Add(inState);
        }

        public void update(long elapsed)
        {
            update(new Vector2f(0, 0), elapsed);
        }

        public void update(Vector2f offset, long elapsed)
        {
            states[active].update(offset, elapsed);
        }

        public virtual void drawTex(RenderTarget rt, long elapsed)
        {
            states[active].draw(rt, elapsed);
        }

        public virtual void draw(RenderWindow rw, long elapsed)
        {
            states[active].draw(rw, elapsed);
        }
    }
}
