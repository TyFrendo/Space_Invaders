using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Space_Invaders.Scripts
{
    class Small_Alien : Alien
    {
        public override void LoadContent(ContentManager content)
        {
            pos_x = 29 * Constants._SIZE;
            pos_y = 56 * Constants._SIZE;
            width = 8 * Constants._SIZE;
            height = 8 * Constants._SIZE;
            tex_name = "Small Alien";

            base.LoadContent(content);
        }
    }
}
