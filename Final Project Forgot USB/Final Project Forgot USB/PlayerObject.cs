using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Final_Project_Forgot_USB
{
    class PlayerObject : MobileObject
    {

        
        public bool jumping;
        public float startY;
        public float jumpspeed;



        public bool CollisionCheck(GameObject other)
        {
            bool playerCollision = AABBCollisionCheck(other);
            if (playerCollision)
            {
                return true;
            }

            return false;
        }

        


    }
}
