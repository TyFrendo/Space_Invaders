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
    class Shield
    {
        int[,] shield_pixels;
        int[,] explosion1, explosion2;
        Texture2D tex;

        public int pos_x, pos_y;

        public void LoadContent(GraphicsDevice graphicsDevice)
        {
            shield_pixels = new int[,]
            {
                { 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0 },
                { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
                { 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1 }
            };

            explosion1 = new int[,]
            {
                { 1, 0, 1, 0, 1, 0, 1 },
                { 0 ,1, 0, 0, 0, 1, 1 },
                { 1, 0, 0, 0, 0, 0, 1 },
                { 1, 1, 0, 0, 0, 1, 1 },
                { 1, 1, 0, 0, 0, 1, 1 }
            };

            explosion2 = new int[,]
            {
                { 1, 1, 0, 0, 0, 1, 1 },
                { 1, 1, 0, 0, 0, 1, 1 },
                { 1, 0, 0, 0, 0, 0, 1 },
                { 0 ,1, 0, 0, 0, 1, 1 },
                { 1, 0, 1, 0, 1, 0, 1 }
            };

            tex = new Texture2D(graphicsDevice, Constants._SIZE, Constants._SIZE);
            Color[] data = new Color[Constants._SIZE * Constants._SIZE];
            for (int i = 0; i < Constants._SIZE * Constants._SIZE; i++)
                data[i] = Color.White;
            tex.SetData(data);

            pos_x = 32 * Constants._SIZE;
            pos_y = 192 * Constants._SIZE;
        }

        public void Update(List<Bullet> bullets)
        {
            Rectangle rec = new Rectangle
            {
                Width = Constants._SIZE,
                Height = Constants._SIZE
            };
            for (int i = 0; i < bullets.Count; i++)
                for (int y = 0; y < 16; y++)
                {
                    for (int x = 0; x < 22; x++)
                    {
                        if (shield_pixels[y, x] > 0)
                        {
                            rec.X = pos_x + x * Constants._SIZE;
                            rec.Y = pos_y + y * Constants._SIZE;
                            if (bullets[i].hit_box.Intersects(rec))
                            {
                                for (int w = y - 2; w <= y + 2; w++)
                                {
                                    for (int z = x - 3; z <= x + 3; z++)
                                    {
                                        if (w < 0 || w >= 16)
                                            break;
                                        if (z < 0 || z >= 22)
                                            continue;
                                        shield_pixels[w, z] *= ((bullets[i].type == 0) ? explosion1 : explosion2)[w - y + 2, z - x + 3];
                                    }
                                }
                                bullets[i].pos_y = (bullets[i].type == 0) ? -20 : 276*Constants._SIZE;
                                return;
                            }
                        }
                    }
                }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle rec = new Rectangle
            {
                Width = Constants._SIZE,
                Height = Constants._SIZE
            };
            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 22; x++)
                {
                    if (shield_pixels[y, x] > 0)
                    {
                        rec.X = pos_x + x * Constants._SIZE;
                        rec.Y = pos_y + y * Constants._SIZE;
                        spriteBatch.Draw(tex, rec, Color.White);
                    }      
                }
            }
        }
    }
}
