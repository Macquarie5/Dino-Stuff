﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final_Project_Forgot_USB
{
    class PlayerObject : MobileObject
    {

        
        public bool jumping;
        public float startY;
        public float jumpspeed;
        public float health;



        public bool CollisionCheck(GameObject other)
        {
            bool playerCollision = AABBCollisionCheck(other);
            if (playerCollision)
            {
                return true;
            }

            return false;
        }

        public bool checkWallCollisions(MobileObject wall)
        {
            bool result = AABBCollisionCheck(wall);

            if (result)
            {

            }

            return result;
        }

        public bool checkEnemyCollisions(GameObject enemy)
        {
            bool result = AABBCollisionCheck(enemy);

            if (result)
            {

            }

            return result;
        }


    }
}
