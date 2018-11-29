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
    class Alien : GameObject
    {
        public ContentManager content;
        int dir;

        public Texture2D tex1, tex2;

        int state;

        public override void LoadContent(ContentManager content)
        {
            this.content = content;
            dir = 1;
            state = 0;

            tex1 = content.Load<Texture2D>(tex_name + " 1");
            tex2 = content.Load<Texture2D>(tex_name + " 2");

            hit_box = new Rectangle(pos_x, pos_y, width, height);
            texture = tex1;
        }

        public override void Update(GameTime gameTime)
        {
            pos_x += 2 * Constants._SIZE * dir;

            Change();

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public void Down(GameTime gameTime)
        {
            pos_y += 8 * Constants._SIZE;
            dir *= -1;

            Change();

            base.Update(gameTime);
        }

        public void Change()
        {
            if (state == 1)
            {
                texture = tex1;
                state = 0;
            }
            else
            {
                texture = tex2;
                state = 1;
            }
        }

        public Bullet GetBullet()
        {
            Alien_Bullet bullet = new Alien_Bullet();
            bullet.LoadContent(content);
            bullet.pos_x = pos_x + width / 2;
            bullet.pos_y = pos_y + height;
            return bullet;
        }
    }
}
