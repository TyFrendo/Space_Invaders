using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Space_Invaders.Scripts
{
    class Ship : GameObject
    {
        KeyboardState oldKeyState;
        public Ship_Bullet bullet;

        ContentManager content;

        public bool fired;

        public override void LoadContent(ContentManager content)
        {
            this.content = content;

            pos_x = 200 * Constants._SIZE;
            pos_y = 226 * Constants._SIZE;
            width = 13 * Constants._SIZE;
            height = 8 * Constants._SIZE;
            tex_name = "Ship";

            fired = false;

            oldKeyState = Keyboard.GetState();

            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState newKeyState = Keyboard.GetState();

            if (newKeyState.IsKeyDown(Keys.Right) && !newKeyState.IsKeyDown(Keys.Left) && pos_x < Constants._WIDTH * Constants._SIZE - width)
                pos_x += Constants._SIZE;
            if (newKeyState.IsKeyDown(Keys.Left) && !newKeyState.IsKeyDown(Keys.Right) && pos_x > 0)
                pos_x -= Constants._SIZE;

            if (!fired && bullet != new Bullet())
            {
                bullet = new Ship_Bullet();
                if (newKeyState.IsKeyDown(Keys.Space))
                {

                    bullet.pos_x = pos_x + (width - Constants._SIZE) / 2;
                    bullet.LoadContent(content);
                    fired = true;
                }
            }

            oldKeyState = newKeyState;

            if (bullet.pos_y < -bullet.height)
                fired = false;

            base.Update(gameTime);
        }
    }
}
