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
using Space_Invaders.Scripts;

namespace Space_Invaders
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Ship ship;

        Shield[] shields;

        List<Alien> aliens;
        List<Bullet> bullets;

        float speed_multiplier, timer, bullet_timer;
        int index;
        bool right, end, down;

        int count;

        Random random;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = Constants._WIDTH * Constants._SIZE,
                PreferredBackBufferHeight = Constants._HEGIHT * Constants._SIZE
            };
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            ship = new Ship();
            aliens = new List<Alien>();
            bullets = new List<Bullet>();
            shields = new Shield[4];
            speed_multiplier = 1f;
            timer = 15f;
            random = new Random();
            bullet_timer = random.Next(200, 400) * 10f;
            index = 0;
            count = -1;
            right = true;
            end = false;
            down = false;
            for (int i = 0; i < 55; i++)
            {
                if (i % 11 == 0)
                    count++;
                if (count == 4)
                {
                    Small_Alien small_alien = new Small_Alien();
                    small_alien.LoadContent(Content);
                    small_alien.pos_x += (8 * Constants._SIZE + small_alien.height) * (i - 44);
                    aliens.Add(small_alien);
                }

                if (count == 3)
                {
                    Normal_Alien normal_alien = new Normal_Alien();
                    normal_alien.LoadContent(Content);
                    normal_alien.pos_x += (8 * Constants._SIZE + normal_alien.height) * (i - 33);
                    aliens.Add(normal_alien);
                }

                if (count == 2)
                {
                    Normal_Alien normal_alien = new Normal_Alien();
                    normal_alien.LoadContent(Content);
                    normal_alien.pos_x += (8 * Constants._SIZE + normal_alien.height) * (i - 22);
                    normal_alien.pos_y += 8 * Constants._SIZE + normal_alien.height;
                    aliens.Add(normal_alien);
                }

                if (count == 1)
                {
                    Big_Alien big_alien = new Big_Alien();
                    big_alien.LoadContent(Content);
                    big_alien.pos_x += (8 * Constants._SIZE + big_alien.height) * (i - 11);
                    aliens.Add(big_alien);
                }

                if (count == 0)
                {
                    Big_Alien big_alien = new Big_Alien();
                    big_alien.LoadContent(Content);
                    big_alien.pos_x += (8 * Constants._SIZE + big_alien.height) * i;
                    big_alien.pos_y += 8 * Constants._SIZE + big_alien.height;
                    aliens.Add(big_alien);
                }
            }

            for (int i = 0; i < 4; i++)
            {
                Shield shield = new Shield();
                shield.LoadContent(GraphicsDevice);
                shield.pos_x += 45 * i * Constants._SIZE;
                shields[i] = shield;
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ship.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ship.Update(gameTime);
            if (ship.fired && (bullets.Count < 1 || bullets[0].type != 0))
                bullets.Insert(0, ship.bullet);

            bullet_timer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (bullet_timer < 0 && aliens.Count > 0)
            {
                bullet_timer = random.Next(100, 200) * 10f;
                int ind = random.Next(0, aliens.Count - 1);
                bullets.Add(aliens[ind].GetBullet());
            }

            for (int i = 0; i < aliens.Count; i++)
            {
                if (bullets.Count > 0 && bullets[0].type == 0)
                {
                    if (bullets[0].hit_box.Intersects(aliens[i].hit_box))
                    {
                        Debug.Print("Before:\ni: " + i + " Count: " + aliens.Count + " Index: " + index);
                        aliens.RemoveAt(i);
                        speed_multiplier *= 1.05f;
                        if (i < index)
                            index--;
                        if (index > aliens.Count - 1)
                            index = 0;
                        if (i > 0)
                            i--;
                        ship.fired = false;
                        bullets[0].des = true;
                        Debug.Print("After:\ni: " + i + " Count: " + aliens.Count + " Index: " + index);

                    }
                }

                if (aliens.Count > 0 && !end)
                    if (right && aliens[i].pos_x > 207 * Constants._SIZE)
                    {
                        end = true;
                        right = false;
                    }
                    else if (!right && (aliens[i].pos_x == 9 * Constants._SIZE))
                    {
                        end = true;
                        right = true;
                    }
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].des)
                    bullets.RemoveAt(i);
                else
                {
                    bullets[i].Update(gameTime);
                    if (bullets[i].type == 1 && bullets[i].hit_box.Intersects(ship.hit_box))
                        ship.pos_y = -ship.height;
                }
            }

            for (int i = 0; i < 4; i++)
                shields[i].Update(bullets);

            timer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds*speed_multiplier;
            if (timer < 0 && aliens.Count > 0)
            {
                if (!down)
                    aliens[index].Update(gameTime);
                else aliens[index].Down(gameTime);
                index++;
                if (index >= aliens.Count)
                {
                    index = 0;
                    if (end)
                    {
                        down = true;
                        end = false;
                    }
                    else
                        down = false;
                }
                timer = 15f;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            ship.Draw(spriteBatch);
            for (int i = 0; i < aliens.Count; i++)
            {
                aliens[i].Draw(spriteBatch);
            }
            for (int i = 0; i < bullets.Count; i++)
                bullets[i].Draw(spriteBatch);
            for (int i = 0; i < 4; i++)
                shields[i].Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
