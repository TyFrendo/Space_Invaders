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
        public override void LoadContent(ContentManager content)
        {
            pos_y = 200 * Constants._SIZE;
            width = 1 * Constants._SIZE;
            height = 3 * Constants._SIZE;
            tex_name = "Bullet";

            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            pos_y -= 3 * Constants._SIZE;

            base.Update(gameTime);
        }
    }
}
