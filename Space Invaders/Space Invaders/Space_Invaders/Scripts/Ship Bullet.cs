using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Space_Invaders.Scripts
{
    class Ship_Bullet : Bullet
    {
        public override void LoadContent(ContentManager content)
        {
            pos_y = 223 * Constants._SIZE;
            width = 1 * Constants._SIZE;
            height = 3 * Constants._SIZE;
            tex_name = "Bullet";

            type = 0;
            dir = 1;

            base.LoadContent(content);
        }
    }
}
