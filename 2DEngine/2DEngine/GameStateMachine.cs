using System;

using SFML.Graphics;
using SFML.Window;

using BaseFunctions;

namespace _2DEngine
{
    class GameStateMachine : StateMachine
    {
        public GameStateMachine()
            : base()
        {
            //active = 0
            BaseScreen main = new BaseScreen("title");
            states.Add(main);
            TextButton butStart = new TextButton("START", new IntRect(600, 0, 200, 50), GraphicsTools.Vectors2f.One);
            TextButton butOptions = new TextButton("OPTIONS", new IntRect(600, 100, 230, 50), GraphicsTools.Vectors2f.One);
            TextButton butTest = new TextButton("TEST AREA", new IntRect(600, 200, 270, 50), GraphicsTools.Vectors2f.One);
            TextButton butExit = new TextButton("EXIT", new IntRect(600, 300, 230, 50), GraphicsTools.Vectors2f.One);
            main.AddButton(butStart);
            main.AddButton(butOptions);
            main.AddButton(butTest);
            main.AddButton(butExit);
            butStart.clicked += new Button.ClickedEventHandler(handleButtons);
            butOptions.clicked += new Button.ClickedEventHandler(handleButtons);
            butTest.clicked += new Button.ClickedEventHandler(handleButtons);
            butExit.clicked += new Button.ClickedEventHandler(handleButtons);

            //active = 1
            BaseScreen options = new BaseScreen("options");
            states.Add(options);
            TextButton butReturn = new TextButton("RETURN", new IntRect(100, 400, 230, 50), GraphicsTools.Vectors2f.One);
            options.AddButton(butReturn);
            butReturn.clicked += new Button.ClickedEventHandler(handleButtons);

            //active = 2
            GameScreen inGame = new GameScreen();
            states.Add(inGame);

            //active = 3
            BaseScreen testScreen = new BaseScreen("test");
            testScreen.AddButton(butReturn);
            testScreen.AddObject(new TextBox());
            states.Add(testScreen);
        }

        private void handleButtons(object sender, EventArgs e)
        {
            switch (((Button)sender).name)
            {
                case "RETURN":
                    active = 0;
                    break;
                case "OPTIONS":
                    active = 1;
                    break;
                case "START":
                    active = 2;
                    break;
                case "TEST AREA":
                    active = 3;
                    break;
                case "EXIT":
                    GraphicsTools.rw.Close();
                    break;
                default:
                    active = 0;
                    break;
            }
        }

        public override void draw(RenderWindow rw, long elapsed)
        {
            rw.Clear();
            base.draw(rw, elapsed);
            rw.Display();
        }
    }
}
