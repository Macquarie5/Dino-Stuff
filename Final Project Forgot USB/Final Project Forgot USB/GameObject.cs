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

        public virtual void UpdateBounds()
        {
            /// Note: this should be called whenever the object position,
            /// size, or scale are changed
            Vector2 extents = size * scale;
            aabb = new AABB(position + extents/2, extents);
        }
        protected bool AABBCollisionCheck(GameObject pOther)
        {
            return aabb.CollsionCheck(pOther.aabb);
        }


        public virtual void SetSize(Vector2 Size)
        {
            size = Size;
            origin = new Vector2
                (size.X / 2 * scale,
                size.Y / 2 * scale);
        }


    }
}
