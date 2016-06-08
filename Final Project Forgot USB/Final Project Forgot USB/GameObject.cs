using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Project_Forgot_USB
{
    class GameObject
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 size;
        public Vector2 origin;

        public float rotation;
        public float scale;

        public bool flipHorosontal;
        public bool flipVertical;

        public AABB aabb;

        protected virtual void UpdateBounds()
        {
            /// Note: this should be called whenever the object position,
            /// size, or scale are changed
            aabb = new AABB(position, size * scale);
        }
        protected bool AABBCollisionCheck(GameObject pOther)
        {
            return aabb.CollsionCheck(pOther.aabb);
        }
    }
}
