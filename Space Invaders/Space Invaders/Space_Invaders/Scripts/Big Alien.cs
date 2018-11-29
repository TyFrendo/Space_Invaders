using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Space_Invaders.Scripts
{
    class Big_Alien : Alien
    {
        public override void LoadContent(ContentManager content)
        {
            pos_x = 28 * Constants._SIZE;
            pos_y = 104 * Constants._SIZE;
            width = 12 * Constants._SIZE;
            height = 8 * Constants._SIZE;
            tex_name = "Big Alien";

            base.LoadContent(content);
        }
    }
}
