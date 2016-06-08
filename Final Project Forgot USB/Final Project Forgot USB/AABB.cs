﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Final_Project_Forgot_USB
{
    class AABB
    {
        public Vector2 min;
        public Vector2 max;
        public AABB(Vector2 position
        , Vector2 size)
        {
            Vector2 toEdge = size / 2;
            min.X = position.X - toEdge.X;
            min.Y = position.Y - toEdge.Y;
            max.X = position.X + toEdge.X;
            max.Y = position.Y + toEdge.Y;
        }

        static public bool CollsionCheck(AABB pObject1, AABB pObject2)
        {
            return !(pObject1.max.X < pObject2.min.X || pObject2.max.X < pObject1.min.X
                || pObject1.max.Y < pObject2.min.Y || pObject2.max.Y < pObject1.min.Y);
        }

        public bool CollsionCheck(AABB other)
        {
            return AABB.CollsionCheck(this, other);
        }
    }
}
