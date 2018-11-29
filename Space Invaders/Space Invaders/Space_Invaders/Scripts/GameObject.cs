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
    class GameObject
    {
        public Rectangle hit_box;
        public Texture2D texture;

        public int pos_x, pos_y, width, height;
        public string tex_name;

        public virtual void LoadContent(ContentManager content)
        {
            hit_box = new Rectangle(pos_x, pos_y, width, height);
            texture = content.Load<Texture2D>(tex_name);
        }

        public virtual void Update(GameTime gameTime)
        {
            hit_box.X = pos_x;
            hit_box.Y = pos_y;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hit_box, Color.White);
        }
    }
}
