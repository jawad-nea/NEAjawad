﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NEAProjectLcokedIn
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D rectangleTexture;

        private float posX;
        private float posY;
        private float velX;
        private float velY;
        private float accelerationY = 20f;
        private const float RectangleWidth = 50f;
        private const float RectangleHeight = 50f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
        }

        private KeyboardState previousKeyboardState, currentKeyboardState;

        protected override void Initialize()
        {
            base.Initialize();
            posX = graphics.PreferredBackBufferWidth / 2 - RectangleWidth / 2;
            posY = graphics.PreferredBackBufferHeight / 2 - RectangleHeight / 2;
            velX = 0f;
            velY = 0f;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            rectangleTexture = new Texture2D(GraphicsDevice, 1, 1);
            rectangleTexture.SetData(new[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            velY += accelerationY * deltaTime;
            posX += velX * deltaTime;
            posY += velY * deltaTime;

            if (currentKeyboardState.IsKeyDown(Keys.W))
                velY -= 50f * deltaTime; // Adjust this value for control sensitivity
            if (currentKeyboardState.IsKeyDown(Keys.S))
                velY += 50f * deltaTime; // Adjust this value for control sensitivity
            if (currentKeyboardState.IsKeyDown(Keys.A))
                velX -= 50f * deltaTime; // Adjust this value for control sensitivity
            if (currentKeyboardState.IsKeyDown(Keys.D))
                velX += 50f * deltaTime; // Adjust this value for control sensitivity

            if (posY + RectangleHeight >= graphics.PreferredBackBufferHeight || posY <= 0)
            {
                velY *= -1;
                if (posY + RectangleHeight > graphics.PreferredBackBufferHeight)
                    posY = graphics.PreferredBackBufferHeight - RectangleHeight;
                if (posY < 0)
                    posY = 0;
            }

            if (posX + RectangleWidth >= graphics.PreferredBackBufferWidth || posX <= 0)
            {
                velX *= -1;
                if (posX + RectangleWidth > graphics.PreferredBackBufferWidth)
                    posX = graphics.PreferredBackBufferWidth - RectangleWidth;
                if (posX < 0)
                    posX = 0;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(rectangleTexture, new Rectangle((int)posX, (int)posY, (int)RectangleWidth, (int)RectangleHeight), Color.Red);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

}
