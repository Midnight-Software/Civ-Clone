using System;

using SFML.Window;
using SFML.Graphics;

namespace BaseFunctions
{
    public class Tile : BaseClickable
    {
        public class IndexerArgs : EventArgs
        {
            public int i, j;

            public IndexerArgs(int a, int b)
            {
                i = a;
                j = b;
            }
        }

        IndexerArgs args;
        public delegate void TileClickedEventHandler(object o, IndexerArgs i);
        public event TileClickedEventHandler tileClicked;

        protected override void OnClicked(EventArgs e)
        {
            IndexerArgs iE = (IndexerArgs)e;
            if (tileClicked != null)
                tileClicked(this, iE);
        }

        SpaceObjectList objects;
        Fleet fleets;
        ObjectList boundObjects;
        
        protected PopupText hoverInfo;
        private string HoverText
        {
            get
            {
                return hoverInfo.HoverText;
            }
            set
            {
                hoverInfo.HoverText = value;
            }
        }

        public void SetHoverTextBoxSize(Vector2f size)
        {
            hoverInfo.setBounds(size);
        }

        public void SetHoverTextSize(Vector2f scale)
        {
            hoverInfo.SetTextScale(scale);
        }

        public void SetHoverText(string words)
        {
            HoverText = words;
        }

        public void SetHoverText(string words, Vector2f size)
        {
            SetHoverText(words);
            SetHoverTextBoxSize(size);
        }

        public void AddBoundObject(I2DObject b)
        {
            boundObjects.add(b);
        }

        private struct Slope 
        {
            public int rise, run;
            public Slope(int a, int b)
            {
                rise = a;
                run = b;
            }
            public float ratio()
            {
                return (float)rise / run;
            }
        }

        private Slope slope;

        public override void InitializeAssets(string path, string helpText = "")
        {
            EventArgs tempE = clickEventArgs;
            base.InitializeAssets(path, helpText);

            hoverInfo = new PopupText(helpText);
            hoverInfo.SetTextScale(new Vector2f(.4f, .5f));
            clickEventArgs = tempE;
        }        

        public Tile(string path, Vector2f pos, TileClickedEventHandler c, IndexerArgs i, params I2DObject[] boundObjs)
            : base(path, pos)
        {
            slope = new Slope(16, 30);
            fleets = new Fleet();
            objects = new SpaceObjectList();
            boundObjects = new ObjectList();
            foreach (I2DObject i2D in boundObjs)
                boundObjects.add(i2D);
            hoverInfo = new PopupText("");
            tileClicked += c;
            clickEventArgs = i;
        }
        
        //TODO : override it to change collision
        public override bool checkMouse(Vector2f offset)
        {
            Vector2i mp = Mouse.GetPosition(GraphicsTools.rw);
            Vector2f topLeft = spr.Position + offset;
            FloatRect bounds = spr.GetLocalBounds();

            if (mp.X >= topLeft.X &&
                mp.X <= (topLeft.X + bounds.Width) &&
                mp.Y >= topLeft.Y &&
                mp.Y <= (topLeft.Y + bounds.Height))
            { 
                //This if statement is meant to filter out the obvious wrong cases
                
                //must be below top left and top right lines
                if (
                    (mp.Y >= (topLeft.Y + slope.rise) - slope.ratio() * (mp.X - topLeft.X)) && //top left
                    (mp.Y >= slope.ratio() * (mp.X - topLeft.X) + (topLeft.Y - slope.rise))  //top right
                   )
                   if(
                      (mp.Y <= slope.ratio() * (mp.X - topLeft.X) + (topLeft.Y + bounds.Height - slope.rise)) && //bottom left
                      (mp.Y <= (topLeft.Y + bounds.Height + slope.rise) - slope.ratio() * (mp.X - topLeft.X)) //bottom right
                     )
                {
                    return true;
                }
            }

            return false;
        }

        public void setActive()
        {
            if (!init) return;
            if (state == State.Idle || state == State.Selected)
                hoverInfo.Active = false;
            else
                hoverInfo.Active = true;
        }

        public override void update(Vector2f offset, long elapsed)
        {
            base.update(offset, elapsed);
            setActive();
            SetHoverText(string.Format("Tile Information:\n" +
                                       "  - Fleets:{0}\n" + 
                                       "  -Objects:{1}",
                                       "No fleets currently",   //Fleets
                                       "No objects currently"));//Objects
            hoverInfo.update(elapsed);
            if (state == State.Selected)
                updateBoundObjects();
        }

        public override void draw(RenderTarget rw, long elapsed)
        {
            base.draw(rw, elapsed);
        }

        public void updateBoundObjects()
        { 
            foreach(I2DObject i2D in boundObjects)
                if (i2D is PopupMenu)
                {
                    ((PopupMenu)i2D).HoverText = "Tile has been selected.";
                }
        }

        public virtual void drawBoundObjects(RenderTarget rw, long elapsed)
        {
            //Hover info is seperate because 
            // a) All tiles have it.
            // b) It has special update code.
            hoverInfo.draw(rw, elapsed);
            if (state == State.Selected)
                boundObjects.draw(rw);
        }
    }
}
