﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Diagnostics;




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
        SoundEffect SfxWin;
        SoundEffect SfxLose;
        

        ParticleSystem.Emitter flyEmitter = null;
        Texture2D flyTexture = null;

        m_ParticleSystem.m_Emitter moneyEmitter = null;
        Texture2D moneyTexture = null;

        Dinosaur_sprite sprite;
        People_Running sprite2;
        public int timer;

        //Menu 
        private Texture2D StartButton;
        private Vector2 StartButtonPosition;
        private Texture2D ExitButton;
        private Vector2 ExitButtonPosition;

        //LevelSelect
        private Texture2D Level1Button;
        private Vector2 Level1ButtonPosition;

        private Texture2D Level2Button;
        private Vector2 Level2ButtonPosition;
        private bool Level2Unlocked;

        private Texture2D Level3Button;
        private Vector2 Level3ButtonPosition;
        private bool Level3Unlocked;

        PlayerObject player;
        MobileObject money;
        List<MobileObject> Missles;
        List<MobileObject> MisslesToRemove;

        List<MobileObject> walls;
        const int NUM_WALLS = 50;
        int wallPosition;
        Texture2D wallTexture;

        MobileObject background1;
        MobileObject background2;
        MobileObject background3;
        public int ScrollCount;
        GameObject enemy;

        public Texture2D LoseScreen;
        public Vector2 LosePos;

        public Vector2 centre;

        //Mouse
        MouseState mouseState;
        MouseState previousMouseState;
        //Mouse Pointer Texture/Position
        public Texture2D MousePointer;
        public Vector2 MousePointerPosition;


        //GameStates
        public enum GameState
        {
            LEVELSELECT,
            MENU,
            PREP,
            GAME1,
            GAME2,
            GAME3,
            LOSE,
            WIN
        }
        GameState gameState;

        Random RandNum;
        Random randomPos = new Random();

        //Songs
        public Song MenuMusic;
        public Song Level1Song;
        public Song Level2Song;
        public Song Level3Song;



        public Texture2D menuBackground;
        //public Texture2D menuDino;
        public Vector2 menuBackgroundPos;
        //public Vector2 menuDinoPos;

        public Texture2D newmoney;

        TimeSpan lastShot;
        TimeSpan shotCoolDown;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //graphics.IsFullScreen = true;
            
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

            player.UpdateBounds();
            player.position = new Vector2(graphics.PreferredBackBufferWidth / 2, 400);
            player.size = new Vector2(265,139);
            player.origin = new Vector2();
            player.rotation = 0f;
            player.scale = 0.5f;
            player.speed = 1;
            player.jumping = false;
            player.jumpspeed = 0;
            player.startY = player.position.Y;
            player.health = 500;

            money = new MobileObject();
            money.UpdateBounds();
            money.position = player.position;
            money.velocity = new Vector2(5, 0);
            money.size = new Vector2(100,50);
            money.origin = new Vector2();
            money.rotation = 1f;
            money.rotationDelta = 0;
            money.scale = 1f;
            money.speed = 5;
            Missles = new List<MobileObject>();
            MisslesToRemove = new List<MobileObject>();
            RandNum = new Random();

            lastShot = new TimeSpan(0, 0, 0, 0, 0);
            shotCoolDown = new TimeSpan(0, 0, 0, 0, 250);

            enemy = new GameObject();
            enemy.UpdateBounds();
            enemy.position = new Vector2(5, 300); //player.groundHeight
            enemy.size = new Vector2(140,196);
            enemy.origin = new Vector2();
            enemy.rotation = 0f;
            enemy.scale = 1f;

            background1 = new MobileObject();
            background1.position = new Vector2(0, 0);
            background2 = new MobileObject();
            background2.position = new Vector2(0, 0);
            background3 = new MobileObject();
            background3.position = new Vector2(0, 0);

            LosePos = new Vector2(0, 0);
            timer = 0;

            centre = new Vector2(player.position.X - player.origin.X, player.position.Y - player.origin.Y);

            //Default State
            gameState = GameState.MENU;
            menuBackgroundPos = new Vector2(0, 0);
            //menuDinoPos = new Vector2(250, graphics.PreferredBackBufferHeight / 2);

            //Menu Button Int
            StartButtonPosition = new Vector2((graphics.PreferredBackBufferWidth / 2) - 50, 200);
            ExitButtonPosition = new Vector2((graphics.PreferredBackBufferWidth / 2) - 50, 250);

            //LevelSelect Int
            Level1ButtonPosition = new Vector2(100, graphics.PreferredBackBufferHeight / 2);
            Level2ButtonPosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            Level2Unlocked = true;
            Level3ButtonPosition = new Vector2(600, graphics.PreferredBackBufferHeight / 2);
            Level3Unlocked = true;

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
            newmoney = Content.Load<Texture2D>("Money Final");
            wallTexture = Content.Load<Texture2D>("Hydrent");
            background1.texture = Content.Load<Texture2D>("Poncey_neighbourhood");
            background2.texture = Content.Load<Texture2D>("Slums Final");
            background3.texture = Content.Load<Texture2D>("Fields Final");
            menuBackground = Content.Load<Texture2D>("Background_Final");
            LoseScreen = Content.Load<Texture2D>("Lose_screen");
            
            StartButton = Content.Load<Texture2D>("Start");
            ExitButton = Content.Load<Texture2D>("Exit");
            MousePointer = Content.Load<Texture2D>("Pointer");

            Level1Button = Content.Load<Texture2D>("poncey_button");
            Level2Button = Content.Load<Texture2D>("slums_button_+_red_cross");
            Level3Button = Content.Load<Texture2D>("fields_button_+_red_cross");

            sprite = new Dinosaur_sprite(Content.Load<Texture2D>("Dinosaur_Trex"), 1, 265, 139);
            sprite.Position = player.position;

            sprite2 = new People_Running(Content.Load<Texture2D>("PEOPLE AGAIN"), 1, 140, 196);
            sprite2.Position2 = enemy.position;

            //Music
            MenuMusic = Content.Load<Song>("The_Show_Must_Be_Go");
            MediaPlayer.Play(MenuMusic);

            Level1Song = Content.Load<Song>("Gaslamp_Funworks");
            Level2Song = Content.Load<Song>("Nano_Hoedown");
            Level3Song = Content.Load<Song>("The_Builder");

            SfxWin = Content.Load<SoundEffect>("New_Win_Sound");
            SfxLose = Content.Load<SoundEffect>("New_Fail_Sound");

            flyTexture = Content.Load<Texture2D>("Fly");
            flyEmitter = new ParticleSystem.Emitter(flyTexture, new Vector2(graphics.PreferredBackBufferWidth / 2, 320));

            moneyTexture = Content.Load<Texture2D>("mini money");
            moneyEmitter = new m_ParticleSystem.m_Emitter(moneyTexture, centre);

            MediaPlayer.IsRepeating = true;


            player.origin = new Vector2(player.texture.Width / 2, player.texture.Height / 2);
            player.SetSize(new Vector2(player.texture.Width, player.texture.Height));
            player.UpdateBounds();

            enemy.origin = new Vector2(enemy.texture.Width / 2, enemy.texture.Height / 2);
            enemy.SetSize(new Vector2(enemy.texture.Width, enemy.texture.Height));
            enemy.UpdateBounds();

            walls = new List<MobileObject>();
            wallPosition = graphics.PreferredBackBufferWidth;
            //for (int i = 0; i < NUM_WALLS; ++i)
            //{
            //    CreateWall();
            //}
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
            Die();
            centre = new Vector2(player.position.X + player.origin.X, player.position.Y + player.origin.Y);

            while (walls.Count < NUM_WALLS)
            {
              
                    CreateWall();
                Debug.WriteLine("added wall, current walls =" + walls.Count);
               
            }
            

            if (gameState == GameState.MENU)
            {
                Updatelevels();
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

            if (gameState == GameState.LEVELSELECT)
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
                sprite2.Position2 = enemy.position;
                moneyEmitter.m_position = centre;

                moneyEmitter.Update(gameTime);
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
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    //wall.texture = Content.Load<Texture2D>("TempWall");
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
                
                foreach (MobileObject wall in walls)
                    wall.UpdateBounds();
                player.UpdateBounds();
                enemy.UpdateBounds();
                CheckWallCollisions();
                CheckEnemyCollisions();
                RemoveMoney();
                CheckMoneyCollisions();

            }



            //GameState 2 Update-------------------------------------------------------------------
            if (gameState == GameState.GAME2)
            {
                sprite.HandleSpriteMovement(gameTime);
                sprite.Position = player.position;
                sprite2.HandleSpriteMovement(gameTime);
                sprite2.Position2 = enemy.position;
                moneyEmitter.m_position = centre;

                moneyEmitter.Update(gameTime);
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

                foreach (MobileObject wall in walls)
                    wall.UpdateBounds();
                player.UpdateBounds();
                enemy.UpdateBounds();
                CheckWallCollisions();
                CheckEnemyCollisions();
                RemoveMoney();
                CheckMoneyCollisions();
            }



            //GameState 3 Update-----------------------------------------------------------------
            if (gameState == GameState.GAME3)
            {
                sprite.HandleSpriteMovement(gameTime);
                sprite.Position = player.position;
                sprite2.HandleSpriteMovement(gameTime);
                sprite2.Position2 = enemy.position;
                moneyEmitter.m_position = centre;

                moneyEmitter.Update(gameTime);
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

                foreach (MobileObject wall in walls)
                    wall.UpdateBounds();
                player.UpdateBounds();
                enemy.UpdateBounds();
                CheckWallCollisions();
                CheckEnemyCollisions();
                RemoveMoney();
                CheckMoneyCollisions();
            }

            if (gameState == GameState.LOSE)
            {
                timer++;
                if (timer == 300)
                {
                    gameState = GameState.MENU;
                    MediaPlayer.Play(MenuMusic);
                    timer = 0;

                }
                flyEmitter.position = new Vector2(graphics.PreferredBackBufferWidth / 2, 320);

                flyEmitter.Update(gameTime);

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
                //spriteBatch.Draw(menuDino, menuDinoPos);
                spriteBatch.Draw(StartButton, StartButtonPosition, Color.White);
                spriteBatch.Draw(ExitButton, ExitButtonPosition, Color.White);
                //Mouse Pointer
                spriteBatch.Draw(MousePointer, MousePointerPosition);
            }


            if (gameState == GameState.LEVELSELECT)
            {
                spriteBatch.Draw(menuBackground, menuBackgroundPos);
                spriteBatch.Draw(Level1Button, Level1ButtonPosition);
                spriteBatch.Draw(Level2Button, Level2ButtonPosition);
                spriteBatch.Draw(Level3Button, Level3ButtonPosition);

                //Mouse Pointer
                spriteBatch.Draw(MousePointer, MousePointerPosition);
            }



            if (gameState == GameState.GAME1)
            {
                spriteBatch.Draw(background1.texture, background1.position);
                moneyEmitter.Draw(spriteBatch);
                spriteBatch.Draw(sprite.Texture, sprite.Position, sprite.SourceRect,
                Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.FlipHorizontally, 0);

                spriteBatch.Draw(sprite2.Texture, sprite2.Position2, sprite2.SourceRect,
                        Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                //ShowCollisionBox();
                //spriteBatch.Draw(money.texture, money.position);
                //spriteBatch.Draw(player.texture, player.position,scale:new Vector2(0.5f, 0.5f), effects:SpriteEffects.FlipHorizontally);
                //spriteBatch.Draw(enemy.texture, enemy.position); 
                DrawWalls();
             
                //spriteBatch.Draw(wall.texture, wall.position, scale: new Vector2(1.0f, 1.0f));
                
                
                DrawMoney();
                DrawHealth();
                
            }

            if (gameState == GameState.GAME2)
            {
                spriteBatch.Draw(background2.texture, background2.position);
                moneyEmitter.Draw(spriteBatch);
                spriteBatch.Draw(sprite.Texture, sprite.Position, sprite.SourceRect,
                Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.FlipHorizontally, 0);

                spriteBatch.Draw(sprite2.Texture, sprite2.Position2, sprite2.SourceRect,
                        Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                //ShowCollisionBox();
                //spriteBatch.Draw(money.texture, money.position);
                //spriteBatch.Draw(player.texture, player.position);
                //spriteBatch.Draw(enemy.texture, enemy.position);
                //spriteBatch.Draw(wall.texture, wall.position);
                DrawWalls();
                
                DrawMoney();
                DrawHealth();
            }

            if (gameState == GameState.GAME3)
            {
                spriteBatch.Draw(background3.texture, background3.position);
                moneyEmitter.Draw(spriteBatch);
                spriteBatch.Draw(sprite.Texture, sprite.Position, sprite.SourceRect,
                Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.FlipHorizontally, 0);

                spriteBatch.Draw(sprite2.Texture, sprite2.Position2, sprite2.SourceRect,
                        Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                //spriteBatch.Draw(money.texture, money.position);
                //spriteBatch.Draw(player.texture, player.position);
                //spriteBatch.Draw(enemy.texture, enemy.position);
                //spriteBatch.Draw(wall.texture, wall.position);
                DrawWalls();
                DrawMoney();
                DrawHealth();
            }

            if (gameState == GameState.LOSE)
            {
                spriteBatch.Draw(LoseScreen, LosePos);
                flyEmitter.Draw(spriteBatch);
            }


            

            spriteBatch.End();
            base.Draw(gameTime);
        }

        //Into the voids
        //
        //
        //
        //
        //

        public void Scrolling1()
        {
            background1.position.X -= 3;
            player.position.X -= 1;

            if (background1.position.X < -4950)
            {
                background1.position.X = 1;

                if (ScrollCount == 1)
                {
                    //End Level
                    SfxWin.Play();
                    Level2Unlocked = true;
                    Reset();
                    gameState = GameState.MENU;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(MenuMusic);
                    ScrollCount = 0;
                }
                else
                {
                    ScrollCount++;
                }         
            }

            foreach (MobileObject wall in walls)
            {
                wall.position.X -= 3;
                wall.UpdateBounds();
            }
        }

        public void Scrolling2()
        {
            background2.position.X -= 3;
            player.position.X -= 1;

            if (background2.position.X < -5180)
            {
                background2.position.X = 1;
                if (ScrollCount == 1)
                {
                    //End Level
                    SfxWin.Play();
                    Level3Unlocked = true;
                    Reset();
                    gameState = GameState.MENU;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(MenuMusic);
                    ScrollCount = 0;
                }
                else
                {
                    ScrollCount++;
                }
            }

            foreach (MobileObject wall in walls)
            {
                wall.position.X -= 3;
                wall.UpdateBounds();
            }
        }

        public void Scrolling3()
        {  
                background3.position.X -= 3;
                player.position.X -= 1;

            if (background3.position.X < -5180)
            {
                background3.position.X = 1;
                if (ScrollCount == 1)
                {
                    //End Level
                    //Final Win Screen
                    SfxWin.Play();
                    Reset();
                    gameState = GameState.MENU;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(MenuMusic);
                    ScrollCount = 0;
                }
                else
                {
                    ScrollCount++;
                }
            }

            foreach (MobileObject wall in walls)
            {
                wall.position.X -= 3;
                wall.UpdateBounds();
            }
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
                    gameState = GameState.LEVELSELECT; //FIRST LEVEL
                    
                }
                else if (mouseClickRect.Intersects(exitButtonRect))
                {
                    Exit();
                }

            }

            if (gameState == GameState.LEVELSELECT)
            {
                Rectangle Level1Rect = new Rectangle((int)Level1ButtonPosition.X,
                                                 (int)Level1ButtonPosition.Y, 177, 132);
                Rectangle Level2Rect = new Rectangle((int)Level2ButtonPosition.X,
                                                (int)Level2ButtonPosition.Y, 175, 131);
                Rectangle Level3Rect = new Rectangle((int)Level3ButtonPosition.X,
                                                (int)Level3ButtonPosition.Y, 177, 132);

                if (mouseClickRect.Intersects(Level1Rect))
                {
                    gameState = GameState.GAME1;
                    wallTexture = Content.Load<Texture2D>("Hydrent");
                    Reset();
                    MediaPlayer.Stop();
                    MediaPlayer.Play(Level1Song);
                }
                else if (mouseClickRect.Intersects(Level2Rect) && (Level2Unlocked == true))
                {
                    gameState = GameState.GAME2;
                    wallTexture = Content.Load<Texture2D>("BAG_AGAIN_!!!!!");
                    Reset();
                    MediaPlayer.Stop();
                    MediaPlayer.Play(Level2Song);
                }
                else if (mouseClickRect.Intersects(Level3Rect) && (Level3Unlocked == true))
                {
                    gameState = GameState.GAME3;
                    wallTexture = Content.Load<Texture2D>("barricade_cow");
                    Reset();
                    MediaPlayer.Stop();
                    MediaPlayer.Play(Level3Song);
                }

            }
        }

        public void CreateMoney(GameTime gameTime)
        {
            TimeSpan timeSincelastShot = gameTime.TotalGameTime - lastShot;

            if (timeSincelastShot > shotCoolDown && player.health > 0)
            {
                MobileObject money = new MobileObject();
                money.position = player.position;
                money.texture = newmoney;
                money.size = new Vector2(100, 50);
                money.scale = 1f;
                money.rotation = 0;
                money.rotationDelta = 0;
                money.velocity = new Vector2(-17, 0);
                money.origin = new Vector2(money.texture.Width / 2, money.texture.Height / 2);
                money.UpdateBounds();

                Missles.Add(money);

                lastShot = gameTime.TotalGameTime;
                player.health -= 5;
            }       
        }

        public void UpdateMoney()
        {
            foreach (MobileObject money in Missles)
            {
                money.position += money.velocity;
                money.rotation -= 0.3f;
                money.UpdateBounds();
                //Debug.WriteLine("Update Position" + money.position);
            }
        }

        public void DrawMoney()
        {
            foreach (GameObject money in Missles)
            {
                spriteBatch.Draw(money.texture, money.position,null,null,money.origin, money.rotation);

                //Texture2D RectangleTexture = new Texture2D(GraphicsDevice, 1, 1);
                //RectangleTexture.SetData(new Color[] { Color.Yellow });
                //Rectangle MoneyBox = new Rectangle((int)money.aabb.min.X, (int)money.aabb.min.Y, (int)(money.size.X * money.scale), (int)(money.size.Y * money.scale));
                //spriteBatch.Draw(RectangleTexture, MoneyBox, Color.Yellow);
                //Debug.WriteLine("draw position" + money.position);
            }
        }

        public void RemoveMoney()
        {
            foreach (MobileObject money in Missles)
            {
                if (money.position.X < -100)
                {
                    MisslesToRemove.Add(money);
                }
            }

            foreach (MobileObject money in MisslesToRemove)
            {
                Missles.Remove(money);
            }
        }

        public void DrawHealth()
        {
            Texture2D health = new Texture2D(GraphicsDevice, 1, 1);
            health.SetData(new Color[] { Color.Red });
            Rectangle healthBar = new Rectangle((int)10, (int)20, (int)player.health, 20);
            Rectangle healthBarBackground = new Rectangle((int)5, (int)15, (int)510, 30);
            spriteBatch.Draw(health, healthBarBackground, Color.Black);
            spriteBatch.Draw(health, healthBar, Color.Red);
        }
  
        public void CheckWallCollisions()
        {
            if (player.health < 500)
            {
                player.health += 0.1f;
            }
            foreach (MobileObject wall in walls)
            {
                if (player.checkWallCollisions(wall))
                {
                    player.health -= 1;
                }
            }
        }

        public void CheckEnemyCollisions()
        {
            if (player.checkEnemyCollisions(enemy))
            {
                player.health -= 500;
            }
        }

        public void CheckMoneyCollisions()
        {
            enemy.position.X += 1;
            foreach (MobileObject Money in Missles)
            {
                if (money.checkEnemyMoneyCollisions(money) && enemy.position.X > 1)
                {
                    enemy.position.X--;
                    enemy.UpdateBounds();
                }
                  
            }
        }

        public void ShowCollisionBox()
        {
            Texture2D RectangleTexture = new Texture2D(GraphicsDevice, 1, 1);
            RectangleTexture.SetData(new Color[] { Color.Yellow });
            Rectangle PlayerBox = new Rectangle((int)player.aabb.min.X, (int)player.aabb.min.Y, (int)(player.size.X * player.scale), (int)(player.size.Y * player.scale));
            //Rectangle WallBox = new Rectangle((int)wall.aabb.min.X, (int)wall.aabb.min.Y, (int)(wall.size.X * wall.scale), (int)(wall.size.Y * wall.scale));
            Rectangle EnemyBox = new Rectangle((int)enemy.aabb.min.X, (int)enemy.aabb.min.Y, (int)(enemy.size.X * enemy.scale), (int)(enemy.size.Y * enemy.scale));
            
            spriteBatch.Draw(RectangleTexture, PlayerBox, Color.Yellow);
            //spriteBatch.Draw(RectangleTexture, WallBox, Color.Yellow);
            spriteBatch.Draw(RectangleTexture, EnemyBox, Color.Yellow);
           

        }

        public void Updatelevels()
        {
            if (Level2Unlocked == true)
            {
                Level2Button = Content.Load<Texture2D>("slums_button");
            }
            if (Level3Unlocked == true)
            {
                Level3Button = Content.Load<Texture2D>("fields_button");
            }
        }

        public void CreateWall()
        {
            MobileObject wall;

            wall = new MobileObject();
            wall.UpdateBounds();
            
            wall.randomX = randomPos.Next(265,530);
            wall.startX = wallPosition;
            wall.position = new Vector2(wall.startX, 380);
            wall.size = new Vector2(44, 50);
            wall.origin = new Vector2();
            wall.rotation = 0f;
            wall.scale = 1f;
            wall.speed = 2;
            wall.texture = wallTexture;

            wall.origin = new Vector2(wall.texture.Width / 2, wall.texture.Height / 2);
            wall.SetSize(new Vector2(wall.texture.Width, wall.texture.Height));
            wall.UpdateBounds();

            walls.Add(wall);

            wallPosition += wall.randomX;
        }




        public void DrawWalls()
        {
            foreach (MobileObject wall in walls)
            {
                spriteBatch.Draw(wall.texture, wall.position, scale: new Vector2(1.0f, 1.0f));
            }       
        }

        public void Reset()
        {
            walls.Clear();
            wallPosition = graphics.PreferredBackBufferWidth;
            player.position = new Vector2(graphics.PreferredBackBufferWidth / 2, 400);
            player.UpdateBounds();
            enemy.position = new Vector2(5, 300);
            enemy.UpdateBounds();
            player.health = 500;

        }
         public void Die()
        {
            if (player.health < 0)
            {
                SfxLose.Play();
                gameState = GameState.LOSE;
                MediaPlayer.Stop();
                Reset();
            }
        }

    }
}
