using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Space_Invaders.Scripts
{
    class Bullet : GameObject
    {
        public int type;
        public float dir;
        public bool des = false;

        public override void Update(GameTime gameTime)
        {
            pos_y -= (int)(3 * Constants._SIZE * dir);

            if (pos_y < -height || pos_y > Constants._HEGIHT * Constants._SIZE * height)
                des = true;

            base.Update(gameTime);
        }
    }
}
