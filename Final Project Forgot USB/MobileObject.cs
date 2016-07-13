using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Project_Forgot_USB
{
    class MobileObject : GameObject
    {
        public float speed;
        public Vector2 velocity;
        public bool isThrown;
        public float rotationDelta;
        public int randomX;
        public int startX;
        

        public virtual void Update(GameTime gameTime, PlayerObject player)
        {
            /// Vertical Movement
            position.Y += velocity.Y;

            /// Horizontal Movement
            position.X += velocity.X * speed;
            //if (player.isOnGround)
            //{
            //    player.velocity.X = 0;
            //}
            

            /// after movement update bounds
            UpdateBounds();

        }

        public bool checkEnemyMoneyCollisions(MobileObject Money)
        {
            bool result = AABBCollisionCheck(Money);

            if (result)
            {

            }

            return result;
        }

    }
}
