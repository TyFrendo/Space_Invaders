using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Space_Invaders.Scripts
{
    class Alien_Bullet : Bullet
    {
        public override void LoadContent(ContentManager content)
        {
            width = 1 * Constants._SIZE;
            height = 3 * Constants._SIZE;
            tex_name = "Bullet";

            type = 1;
            dir = -2 / 3f;

            base.LoadContent(content);
        }
    }
}
