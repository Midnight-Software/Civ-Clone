namespace BaseFunctions
{
	//TODO >
	// While writing this class, I realized that what I might want to do is to 
	// wrap the SFML classes and structures that I am using, so that I can still
	// name them something like Vector2D, etc. and I wont have to worry about
	// namespace collison. Just a thought.
	public class TextBox : I2DObject
	{
		public string Text { get; private set; }
		public SFML.Window.Vector2f Position { get; private set; }
		
        private uint cursorPosition;
        private bool init = false;

        private SFML.Graphics.Sprite spr;
        private SFML.Graphics.Text tex;
		
		public void initialize()
		{
			cursorPosition = 0;
            spr = new SFML.Graphics.Sprite(GraphicsTools.getColoredTexture(100, 200, SFML.Graphics.Color.Cyan));
            init = true;
		}
		
		public TextBox()
		{
			initialize();
		}
		
		public TextBox(string initialText)
		{
			Text = initialText;
			initialize();
		}
		
		public TextBox(SFML.Window.Vector2f inPosition)
		{
			Position = inPosition;
			initialize();
		}
		
		public TextBox(string initialText, SFML.Window.Vector2f inPosition)
		{
			Text = initialText;
			Position = inPosition;
			initialize();
		}
		
        public delegate void clickHandler();
        public event clickHandler clicked;
        private void raiseClicked()
        {
            if (clicked == null) return;
            clicked();
        }

		public void update()
        {
            SFML.Window.Vector2i mp = SFML.Window.Mouse.GetPosition(GraphicsTools.rw);
            //if the mouse is in the bounds of the textbox:
            // -change the cursor to a text thing
            // -do click checking
            // -if the box is clicked:
            //  =update the current edit position
            
            //TODO >
            // While creating this class, I got to thinking about how to only type in one textbox
            // at a time. OPTIONS:
            //  Have a static variable in GraphicsTools that is a pointer to the active text box.
            //  using this you can ensure that typing only is done into the edit point of the 
            //  active textbox.
            // a question that this method naturally raises is that where do you handle the typing?
            // my instinct is to only call update on the textbox that is active. maybe make this a 
            // screen attribute instead? so that only one text box on a screen can be typed into.
            // in that case there should be events for gain and lose focus. actually, what could 
            // be is that in the screen, you have a setter for the active textbox that will call
            // the lose focus of the old one, assign the new one and call the gain focus of the 
            // new one. i like this idea a lot actually.
        }
		
		public void draw(SFML.Graphics.RenderTarget rt) 
        {
            if (init)
                rt.Draw(spr);
        }
		
		//what functionality does textbox need to provide?
		// -draggable?
		// -editable.
		// -can be contained in other objects.
		// -can contain other objects? (nah, only text)

        public void update(long elapsed)
        {
            update();
        }

        public void update(SFML.Window.Vector2f offset, long elapsed)
        {
            update();
        }

        public void draw(SFML.Graphics.RenderTarget rt, long elapsed)
        {
            draw(rt);
        }
    }
}
