using System; //EventArgs

using SFML.Graphics;
using SFML.Window;

using BaseFunctions;

namespace _2DEngine
{
    class GameScreen : BaseScreen
    {
        Tabs tabs;
        Label generalDisplayMenu;
        int turnCounter = 0;

        public GameScreen()
            : base()
        {
            Vector2i activeResolution = GraphicsTools.ActiveResolution;
            Texture black = new Texture((uint)activeResolution.X, (uint)activeResolution.Y);
            Texture white = new Texture((uint)activeResolution.X, (uint)activeResolution.Y);
            Image img = new Image((uint)activeResolution.X, (uint)activeResolution.Y, Color.Black);
            Image img2 = new Image((uint)activeResolution.X, (uint)activeResolution.Y, Color.White);
            black.Update(img);
            white.Update(img2);
            base.initTex(black);

            HexGrid grid = new HexGrid(7);
            grid[0, 0].InitializeAssets("platform");

            //I am leaving this here as an example of manually justified text.
            //grid[0, 0].SetHoverText("THIS IS A TEST \nOF THE EMERG-\nENCY BROAD-\nCAST SYSTEM", new Vector2f(200, 100));

            //AddObject(grid);

            //this is an example of how to create an event handler for clicking a tile.
            //grid[0, 0].clicked += GameStateMachine_clicked;

            TextButton butMap = new TextButton("MAP", new IntRect(0, 0, 115, 50), GraphicsTools.Vectors2f.One);
            AddButton(butMap);

            TextButton butColonies = new TextButton("COLONIES", new IntRect(butMap.Bounds.Left + butMap.Bounds.Width + 2, 0, 180, 50), new Vector2f(.75f, 1f));
            AddButton(butColonies);

            TextButton butFleets = new TextButton("FLEETS", new IntRect(butColonies.Bounds.Left + butColonies.Bounds.Width + 2, 0, 200, 50), GraphicsTools.Vectors2f.One);
            AddButton(butFleets);

            TextButton butTasks = new TextButton("TASKS", new IntRect(butFleets.Bounds.Left + butFleets.Bounds.Width + 2, 0, 170, 50), GraphicsTools.Vectors2f.One);
            AddButton(butTasks);

            TextButton butResearch = new TextButton("RESEARCH", new IntRect(butTasks.Bounds.Left + butTasks.Bounds.Width + 2, 0, 200, 50), new Vector2f(.75f, 1f));
            AddButton(butResearch);

            TextButton butMenu = new TextButton("MENU", new IntRect(butResearch.Bounds.Left + butResearch.Bounds.Width + 2, 0, 140, 50), GraphicsTools.Vectors2f.One);
            AddButton(butMenu);
           
            Vector2i size = new Vector2i(1366, 768 - 50);

            Texture mapTex = new Texture((uint)size.X, (uint)size.Y);
            Image mapImg = new Image((uint)size.X, (uint)size.Y, Color.Cyan);
            mapTex.Update(mapImg);
            BaseScreen mapScreen = new BaseScreen(mapTex);
            tabs = new Tabs(new IntRect(0, 50, size.X, size.Y));
            tabs.AddTab("MAP", butMap, mapScreen);
            mapScreen.AddObject(grid);
            
            Texture coloniesTex = new Texture((uint)size.X, (uint)size.Y);
            Image coloniesImg = new Image((uint)size.X, (uint)size.Y, Color.Green);
            coloniesTex.Update(coloniesImg);
            BaseScreen coloniesScreen = new BaseScreen(coloniesTex);
            tabs.AddTab("COLONIES", butColonies, coloniesScreen);

            Texture fleetsTex = new Texture((uint)size.X, (uint)size.Y);
            Image fleetsImg = new Image((uint)size.X, (uint)size.Y, Color.Red);
            fleetsTex.Update(fleetsImg);
            BaseScreen fleetsScreen = new BaseScreen(fleetsTex);
            tabs.AddTab("FLEETS", butFleets, fleetsScreen);

            Texture tasksTex = new Texture((uint)size.X, (uint)size.Y);
            Image tasksImg = new Image((uint)size.X, (uint)size.Y, Color.Magenta);
            tasksTex.Update(tasksImg);
            BaseScreen tasksScreen = new BaseScreen(tasksTex);
            tabs.AddTab("TASKS", butTasks, tasksScreen);

            Texture researchTex = new Texture((uint)size.X, (uint)size.Y);
            Image researchImg = new Image((uint)size.X, (uint)size.Y, Color.White);
            researchTex.Update(researchImg);
            BaseScreen researchScreen = new BaseScreen(researchTex);
            tabs.AddTab("RESEARCH", butResearch, researchScreen);

            Texture menuTex = new Texture((uint)size.X, (uint)size.Y);
            Image menuImg = new Image((uint)size.X, (uint)size.Y, Color.Yellow);
            menuTex.Update(menuImg);
            BaseScreen menuScreen = new BaseScreen(menuTex);

            TextButton closeBut = new TextButton("CLOSE", new IntRect(20, 20, 200, 50), GraphicsTools.Vectors2f.One);
            closeBut.clicked += handleButtons;
            menuScreen.AddButton(closeBut);
            tabs.AddTab("MENU", butMenu, menuScreen);

            generalDisplayMenu = new Label(new Vector2f(350,50), new Vector2f(1366-350, 0));
            AddObject(generalDisplayMenu);

            //PopupMenu planetMenu = new PopupMenu();
            //planetMenu.AddButton(new TextButton("HI HELLO", new IntRect(20, 20, 200, 50), GraphicsTools.Vectors2f.One));
            //planetMenu.Active = true;
            //grid[0, 0].AddBoundObject(planetMenu);

            TextButton nextTurnButton = new TextButton("NEXT TURN", new IntRect(730, 600, 290, 50), GraphicsTools.Vectors2f.One);
            nextTurnButton.clicked += handleButtons;
            AddButton(nextTurnButton);
        }

        private void handleButtons(object sender, EventArgs e)
        {
            switch (((Button)sender).name)
            {
                case "NEXT TURN":
                    turnCounter++;
                    break;
                case "CLOSE":
                    GraphicsTools.rw.Close();
                    break;
                //case "Options":
                //    active = 1;
                //    break;
                //case "Start":
                //    active = 2;
                //    break;
                //default:
                //    active = 0;
                //    break;
            }
        }

        void GameStateMachine_clicked(object sender, EventArgs e)
        {
            //event handler code goes here....
        }

        public override void update(Vector2f offset, long elapsed)
        {
            base.update(offset, elapsed);
            tabs.update(new Vector2f(0, 50), elapsed);
            //generalDisplayMenu.DisplayedText = string.Format("Miscelaneous Information:\n Current Turn: {0}", turnCounter.ToString());
        }

        public override void draw(RenderTarget rw, long elapsed)
        {
            if (!init) return;
            rw.Draw(spr);
            tabs.draw(rw, elapsed);
            buttons.draw(rw);
            objects.draw(rw);
        }
    }
}