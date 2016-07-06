using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final_Project_Forgot_USB
{
    class People_Running
    {
        KeyboardState currentKBState;
        KeyboardState previousKBState;

        Texture2D spriteTexture2;
        float timer2 = 0f;
        float interval2 = 200f;
        int currentFrame2 = 0;
        int spriteWidth2 = 140;
        int spriteHeight2 = 196;
        int spriteSpeed2 = 3;
        Rectangle sourceRect2;
        Vector2 position2;
        Vector2 origin2;
        private Texture2D texture2D2;
        bool moving = true;


        public Vector2 Position2

        {
            get { return position2; }
            set { position2 = value; }
        }

        public Vector2 Origin

        {
            get { return origin2; }
            set { origin2 = value; }
        }

        public Texture2D Texture

        {
            get { return spriteTexture2; }
            set { spriteTexture2 = value; }
        }

        public Rectangle SourceRect

        {
            get { return sourceRect2; }
            set { sourceRect2 = value; }
        }

        public People_Running(Texture2D texture, int currentFrame, int spriteWidth, int spriteHeight)

        {
            this.spriteTexture2 = texture;
            this.currentFrame2 = currentFrame;
            this.spriteWidth2 = spriteWidth;
            this.spriteHeight2 = spriteHeight;
        }

        public void HandleSpriteMovement(GameTime gameTime)
        {
            previousKBState = currentKBState;
            currentKBState = Keyboard.GetState();
            sourceRect2 = new Rectangle(currentFrame2 * spriteWidth2, 0, spriteWidth2, spriteHeight2);

            if (currentKBState.GetPressedKeys().Length == 0)
            {
                if (currentFrame2 > 6 && currentFrame2 < 0)
                {
                    currentFrame2 = 1;
                }
                if (currentFrame2 > 8 && currentFrame2 < 4)
                {
                    currentFrame2 = 1;
                }
                if (currentFrame2 > 12 && currentFrame2 < 8)
                {
                    currentFrame2 = 1;
                }
                if (currentFrame2 > 15 && currentFrame2 < 12)
                {
                    currentFrame2 = 1;
                }
            }


            //this controls the players speed
            {
                spriteSpeed2 = 0;
                interval2 = 75;
            }

            //this controls the players direction
            if (moving == true)

            {

                AnimateRight(gameTime);
                if (position2.X < 780)
                {
                    position2.X += spriteSpeed2;
                }
            }

            if (currentKBState.IsKeyDown(Keys.Left) == true)
            {

                AnimateLeft(gameTime);
                if (position2.X > 20)
                {
                    position2.X -= spriteSpeed2;
                }
            }


            origin2 = new Vector2(sourceRect2.Width / 2, sourceRect2.Height / 2);
        }



        public void AnimateRight(GameTime gameTime)

        {
            if (currentKBState != previousKBState)
            {
                currentFrame2 = 1;
            }

            timer2 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer2 > interval2)
            {
                currentFrame2++;

                if (currentFrame2 > 5)
                {
                    currentFrame2 = 1;
                }
                timer2 = 0f;
            }
        }


        public void AnimateLeft(GameTime gameTime)
        {
            if (currentKBState != previousKBState)
            {
                currentFrame2 = 1;
            }

            timer2 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer2 > interval2)
            {
                currentFrame2++;

                if (currentFrame2 > 5)
                {
                    currentFrame2 = 1;
                }
                timer2 = 0f;
            }
        }
    }
}
