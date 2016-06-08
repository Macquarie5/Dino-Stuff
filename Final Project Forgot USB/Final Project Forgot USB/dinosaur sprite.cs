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
        public class Dinosaur_sprite
        {
            KeyboardState currentKBState;
            KeyboardState previousKBState;

            Texture2D spriteTexture;
            float timer = 0f;
            float interval = 200f;
            int currentFrame = 0;
            int spriteWidth = 87;
            int spriteHeight = 105;
            int spriteSpeed = 3;
            Rectangle sourceRect;
            Vector2 position;
            Vector2 origin;
            private Texture2D texture2D;
            bool moving = true;


            public Vector2 Position

            {
                get { return position; }
                set { position = value; }
            }

            public Vector2 Origin

            {
                get { return origin; }
                set { origin = value; }
            }

            public Texture2D Texture

            {
                get { return spriteTexture; }
                set { spriteTexture = value; }
            }

            public Rectangle SourceRect

            {
                get { return sourceRect; }
                set { sourceRect = value; }
            }

            public Dinosaur_sprite(Texture2D texture, int currentFrame, int spriteWidth, int spriteHeight)

            {
                this.spriteTexture = texture;
                this.currentFrame = currentFrame;
                this.spriteWidth = spriteWidth;
                this.spriteHeight = spriteHeight;
            }

            public void HandleSpriteMovement(GameTime gameTime)
            {
                previousKBState = currentKBState;
                currentKBState = Keyboard.GetState();
                sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);

            if (currentKBState.GetPressedKeys().Length == 0)
            {
                if (currentFrame > 4 && currentFrame < 0)
                {
                    currentFrame = 1;
                }
                if (currentFrame > 8 && currentFrame < 4)
                {
                    currentFrame = 1;
                }
                if (currentFrame > 12 && currentFrame < 8)
                {
                    currentFrame = 1;
                }
                if (currentFrame > 15 && currentFrame < 12)
                {
                    currentFrame = 1;
                }
            }


            //this controls the players speed
            {
                    spriteSpeed = 0; //KEEP ZERO
                    interval = 20; //LOWER TO MAKE FASTER
                }

                //this controls the players direction
                if (moving == true)

                {

                    AnimateRight(gameTime);
                    if (position.X < 780)
                    {
                        position.X += spriteSpeed;
                    }
                }

                if (currentKBState.IsKeyDown(Keys.Left) == true)
                {

                    AnimateLeft(gameTime);
                    if (position.X > 20)
                    {
                        position.X -= spriteSpeed;
                    }
                }


                origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
            }



            public void AnimateRight(GameTime gameTime)

            {
                if (currentKBState != previousKBState)
                {
                    currentFrame = 9;
                }

                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (timer > interval)
                {
                    currentFrame++;

                    if (currentFrame > 15)
                    {
                        currentFrame = 1;
                    }
                    timer = 0f;
                }
            }


            public void AnimateLeft(GameTime gameTime)
            {
                if (currentKBState != previousKBState)
                {
                    currentFrame = 5;
                }

                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (timer > interval)
                {
                    currentFrame++;

                    if (currentFrame > 15)
                    {
                        currentFrame = 1;
                    }
                    timer = 0f;
                }
            }









        }
    }

