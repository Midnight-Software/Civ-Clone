using System;

using SFML.Graphics;
using SFML.Window;

namespace BaseFunctions
{
    public class HexGrid : I2DObject
    {
        HexContainer<Tile> tiles;
        PopupMenu boundMenu;

        public Tile this[int i, int j]
        {
            get
            {
                return tiles[i, j];
            }
            private set
            {
                tiles[i, j] = value;
            }
        }

        private Vector2f findOffset(int i, int j)
        {
            //for now assume gap of 3
            //row diff of 48 (16*3) + 3
            //col diff of 64 + 3 every row + 32 (64/2)

            //center is row (int)/ 2 + 1
            //          col (int)/ 2 + 1
            Vector2f centerCoord = new Vector2f(tiles.RowCount() / 2, tiles.RowLength(tiles.RowCount() / 2) / 2);
            Vector2f centerPos = new Vector2f(centerCoord.X * (64 + 3), centerCoord.Y * (48 + 3));

            Vector2f diff = new Vector2f(centerCoord.X - i, centerCoord.Y - j);

            int horiOffset = (int)(diff.X * (64 + 3) - (Math.Abs(diff.Y) * 32));
            int vertOffset = (int)(diff.Y * (48 + 3));

            return new Vector2f(centerPos.X - horiOffset, centerPos.Y - vertOffset);
        }

        Tile.IndexerArgs activeSelected;

        public void internalClickHandler(object o, Tile.IndexerArgs e)
        {
            ((Tile)o).state = BaseClickable.State.Selected;
            if (activeSelected != null)
            {
                //TODO >
                // If I make state a property I could have the setter reset the state
                this[activeSelected.i, activeSelected.j].state = BaseClickable.State.Idle;
                //TODO >
                // I need to check the mouse afterwords.
                this[activeSelected.i, activeSelected.j].setActive();
            }
            activeSelected = e;
            this[activeSelected.i, activeSelected.j].state = BaseClickable.State.Selected;
            this[activeSelected.i, activeSelected.j].setActive();

        }

        //currently arbitrary fields/values
        public HexGrid(int side)
        {
            tiles = new HexContainer<Tile>(side);
            Tile.TileClickedEventHandler c = new Tile.TileClickedEventHandler(internalClickHandler);
            boundMenu = new PopupMenu("This is a text", new IntRect(966,0,400,768-50), Color.Blue, true);
            for (int i = 0; i < tiles.RowCount(); i++)
                for (int j = 0; j < tiles.RowLength(i); j++)
                    tiles[j, i] = new Tile("default", findOffset(j, i), c, new Tile.IndexerArgs(j, i), boundMenu);
        }

        public HexGrid(int[] sides)
        {
            if (sides.Length != 6) throw new GraphicsTools.InvalidDimensionException();
        }

        public void update(Vector2f offset, long elapsed)
        {
            foreach (Tile t in tiles)
                t.update(offset, 0);
        }

        public void update(long elapsed)
        {
            throw new GraphicsTools.NotImplementedException("MER?MER.");
        }
                
        public void draw(RenderTarget rt, long elapsed)
        {
            foreach (Tile t in tiles)
                t.draw(rt, 0);
            foreach (Tile t in tiles)
                t.drawBoundObjects(rt, 0);
            boundMenu.draw(rt, elapsed);
        }
    }
}
