using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace Final_Project_Forgot_USB
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Song Song;

        Dinosaur_sprite sprite;
        People_Running sprite2;

        //Menu 
        private Texture2D StartButton;
        private Vector2 StartButtonPosition;
        private Texture2D ExitButton;
        private Vector2 ExitButtonPosition;

        PlayerObject player;
        MobileObject money;
        List<MobileObject> Missles;
        MobileObject wall;
        MobileObject background1;
        MobileObject background2;
        MobileObject background3;
        GameObject enemy;

        //Mouse
        MouseState mouseState;
        MouseState previousMouseState;
        //Mouse Pointer Texture/Position
        public Texture2D MousePointer;
        public Vector2 MousePointerPosition;


        //GameStates
        public enum GameState
        {
            SPLASH,
            MENU,
            PREP,
            GAME1,
            GAME2,
            GAME3,
        }
        GameState gameState;

        Random RandNum;

        //Songs
        public Song MenuMusic;
        public Song Level1;
        public Song Level2;
        public Song Level3;

        public Texture2D menuBackground;
        public Texture2D menuDino;
        public Vector2 menuBackgroundPos;
        public Vector2 menuDinoPos;

        public Texture2D newmoney;

        TimeSpan lastShot;
        TimeSpan shotCoolDown;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            player = new PlayerObject();

            player.position = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            player.size = new Vector2();
            player.origin = new Vector2();
            player.rotation = 0f;
            player.scale = 1f;
            player.speed = 1;
            player.jumping = false;
            player.jumpspeed = 0;
            player.startY = player.position.Y;

            money = new MobileObject();
            money.position = player.position;
            money.velocity = new Vector2(5, 0);
            money.size = new Vector2();
            money.origin = new Vector2();
            money.rotation = 1f;
            money.rotationDelta = 0;
            money.scale = 1f;
            money.speed = 5;
            Missles = new List<MobileObject>();
            RandNum = new Random();

            lastShot = new TimeSpan(0, 0, 0, 0, 0);
            shotCoolDown = new TimeSpan(0, 0, 0, 0, 250);
            
            wall = new MobileObject();
            wall.position = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight / 3);
            wall.size = new Vector2();
            wall.origin = new Vector2();
            wall.rotation = 0f;
            wall.scale = 1f;
            wall.speed = 2;

            enemy = new GameObject();
            enemy.position = new Vector2(5, graphics.PreferredBackBufferHeight / 2); //player.groundHeight
            enemy.size = new Vector2();
            enemy.origin = new Vector2();
            enemy.rotation = 0f;
            enemy.scale = 1f;

            background1 = new MobileObject();
            background1.position = new Vector2(0, 0);
            background2 = new MobileObject();
            background2.position = new Vector2(0, 0);
            background3 = new MobileObject();
            background3.position = new Vector2(0, 0);




            //Default State
            gameState = GameState.MENU;
            menuBackgroundPos = new Vector2(0, 0);
            menuDinoPos = new Vector2(250, graphics.PreferredBackBufferHeight / 2);

            //Menu Button Int
            StartButtonPosition = new Vector2((graphics.PreferredBackBufferWidth / 2) - 50, 200);
            ExitButtonPosition = new Vector2((graphics.PreferredBackBufferWidth / 2) - 50, 250);

            //Mouse Click
            mouseState = Mouse.GetState();
            previousMouseState = mouseState;





            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            player.texture = Content.Load<Texture2D>("Dinosaur_Trex Single");
            enemy.texture = Content.Load<Texture2D>("TempEnemy");
            money.texture = Content.Load<Texture2D>("TempMoney");
            newmoney = Content.Load<Texture2D>("money_wad_finished");
            wall.texture = Content.Load<Texture2D>("TempWall");
            background1.texture = Content.Load<Texture2D>("Ponce New");
            background2.texture = Content.Load<Texture2D>("Slums New");
            background3.texture = Content.Load<Texture2D>("FIELDS");
            menuBackground = Content.Load<Texture2D>("!!!MENU_COVER");
            menuDino = Content.Load<Texture2D>("Menu trex");
            StartButton = Content.Load<Texture2D>("Start");
            ExitButton = Content.Load<Texture2D>("Exit");
            MousePointer = Content.Load<Texture2D>("Pointer");

            sprite = new Dinosaur_sprite(Content.Load<Texture2D>("Dinosaur_Trex"), 1, 265, 139);
            sprite.Position = player.position;

            sprite2 = new People_Running(Content.Load<Texture2D>("PEOPLE AGAIN"), 1, 164, 200);
            sprite2.Position2 = enemy.position;

            //Music
            //MenuMusic = Content.Load<Song>("");
            //Level1 = Content.Load<Song>("");
            //Level2 = Content.Load<Song>("");
            //Level3 = Content.Load<Song>("");


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            

            if (gameState == GameState.MENU)
            {
                //Mouse Click
                MousePointerPosition = new Vector2((mouseState.X - 10), (mouseState.Y - 10));
                mouseState = Mouse.GetState();
                if (previousMouseState.LeftButton == ButtonState.Pressed &&
                    mouseState.LeftButton == ButtonState.Released)
                {
                    MouseClicked(mouseState.X, mouseState.Y);
                }
                previousMouseState = mouseState;
                base.Update(gameTime);
            }



            //GameState 1 Update------------------------------------------------------
            if (gameState == GameState.GAME1)
            {
                sprite.HandleSpriteMovement(gameTime);
                sprite.Position = player.position;
                sprite2.HandleSpriteMovement(gameTime);

                Scrolling1();
                player.Update(gameTime, player);
                //Jumping
                if (player.jumping)
                {
                    player.position.Y += player.jumpspeed;//Making it go up
                    player.jumpspeed += 1;
                    if (player.position.Y >= player.startY)
                    //If it's farther than ground
                    {
                        player.position.Y = player.startY;
                        player.jumping = false;
                    }
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        player.jumping = true;
                        player.jumpspeed = -20;//Change for jump hight
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {

                }
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    player.position.X -= 5;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    player.position.X += 6;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    CreateMoney(gameTime);
                }
                UpdateMoney();
            }



            //GameState 2 Update-------------------------------------------------------------------
            if (gameState == GameState.GAME2)
            {
                sprite.HandleSpriteMovement(gameTime);
                sprite.Position = player.position;
                sprite2.HandleSpriteMovement(gameTime);
                
                Scrolling2();
                player.Update(gameTime, player);
                //Jumping
                if (player.jumping)
                {
                    player.position.Y += player.jumpspeed;//Making it go up
                    player.jumpspeed += 1;
                    if (player.position.Y >= player.startY)
                    //If it's farther than ground
                    {
                        player.position.Y = player.startY;
                        player.jumping = false;
                    }
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        player.jumping = true;
                        player.jumpspeed = -20;//Change for jump hight
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {

                }
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    player.position.X -= 5;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    player.position.X += 6;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    CreateMoney(gameTime);
                }
                UpdateMoney();
            }



            //GameState 3 Update-----------------------------------------------------------------
            if (gameState == GameState.GAME3)
            {
                sprite.HandleSpriteMovement(gameTime);
                sprite.Position = player.position;
                sprite2.HandleSpriteMovement(gameTime);
                
                Scrolling3();
                player.Update(gameTime, player);
                //Jumping
                if (player.jumping)
                {
                    player.position.Y += player.jumpspeed;//Making it go up
                    player.jumpspeed += 1;
                    if (player.position.Y >= player.startY)
                    //If it's farther than ground
                    {
                        player.position.Y = player.startY;
                        player.jumping = false;
                    }
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        player.jumping = true;
                        player.jumpspeed = -20;//Change for jump hight
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {

                }
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    player.position.X -= 5;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    player.position.X += 6;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Space))// && money.isThrown == false)
                {
                    CreateMoney(gameTime);
                }
                UpdateMoney();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if (gameState == GameState.MENU)
            {
                spriteBatch.Draw(menuBackground, menuBackgroundPos);
                spriteBatch.Draw(menuDino, menuDinoPos);
                spriteBatch.Draw(StartButton, StartButtonPosition, Color.White);
                spriteBatch.Draw(ExitButton, ExitButtonPosition, Color.White);
                //Mouse Pointer
                spriteBatch.Draw(MousePointer, MousePointerPosition);
            }



            if (gameState == GameState.GAME1)
            {
                spriteBatch.Draw(background1.texture, background1.position);
                //spriteBatch.Draw(money.texture, money.position);
                spriteBatch.Draw(player.texture, player.position,scale:new Vector2(0.5f, 0.5f), effects:SpriteEffects.FlipHorizontally);
                //spriteBatch.Draw(enemy.texture, enemy.position); 
                //spriteBatch.Draw(wall.texture, wall.position);
                DrawMoney();
            }

            if (gameState == GameState.GAME2)
            {
                spriteBatch.Draw(background2.texture, background2.position);
                //spriteBatch.Draw(money.texture, money.position);
                //spriteBatch.Draw(player.texture, player.position);
                //spriteBatch.Draw(enemy.texture, enemy.position);
                //spriteBatch.Draw(wall.texture, wall.position);
                DrawMoney();
            }

            if (gameState == GameState.GAME3)
            {
                spriteBatch.Draw(background3.texture, background3.position);
                //spriteBatch.Draw(money.texture, money.position);
                //spriteBatch.Draw(player.texture, player.position);
                //spriteBatch.Draw(enemy.texture, enemy.position);
                //spriteBatch.Draw(wall.texture, wall.position);
                DrawMoney();
            }


            spriteBatch.Draw(sprite.Texture, sprite.Position, sprite.SourceRect,
                Color.White, 0f, sprite.Origin, 0.5f, SpriteEffects.FlipHorizontally, 0);

            spriteBatch.Draw(sprite2.Texture, sprite2.Position2, sprite2.SourceRect,
                    Color.White, 0f, sprite2.Origin, 1.0f, SpriteEffects.None, 0);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        //Into the voids
        //
        //

        public void Scrolling1()
        {
            background1.position.X -= 2;
            player.position.X -= 2;
        }

        public void Scrolling2()
        {
            background2.position.X -= 2;
            player.position.X -= 2;
        }

        public void Scrolling3()
        {  
                background3.position.X -= 2;
                player.position.X -= 2;    
        }

        //Mouse Click
        private void MouseClicked(int x, int y)
        {

            Rectangle mouseClickRect = new Rectangle(x, y, 10, 10);

            if (gameState == GameState.MENU)
            {
                Rectangle startButtonRect = new Rectangle((int)StartButtonPosition.X,
                                                 (int)StartButtonPosition.Y, 100, 20);
                Rectangle exitButtonRect = new Rectangle((int)ExitButtonPosition.X,
                                                (int)ExitButtonPosition.Y, 100, 20);

                if (mouseClickRect.Intersects(startButtonRect))
                {
                    gameState = GameState.GAME1; //FIRST LEVEL
                    
                }
                else if (mouseClickRect.Intersects(exitButtonRect))
                {
                    Exit();
                }

            }
        }

        public void CreateMoney(GameTime gameTime)
        {
            TimeSpan timeSincelastShot = gameTime.TotalGameTime - lastShot;

            if (timeSincelastShot > shotCoolDown)
            {
                MobileObject money = new MobileObject();
                money.position = player.position;
                money.texture = newmoney;
                money.rotation = RandNum.Next(-100, 100);
                money.rotationDelta = RandNum.Next(-100, 100);
                money.velocity = new Vector2(17, 0);

                Missles.Add(money);

                lastShot = gameTime.TotalGameTime;
            }       
        }

        public void UpdateMoney()
        {
            foreach (MobileObject money in Missles)
            {
                money.position += money.velocity;
                money.rotation += 0.1f;
            }
        }

        public void DrawMoney()
        {
            foreach (GameObject money in Missles)
            {
                spriteBatch.Draw(money.texture, money.position,null,null,money.origin, money.rotation);
            }
        }


  


      




    }
}
