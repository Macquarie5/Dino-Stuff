﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Final_Project_Forgot_USB
{
    class ParticleSystem
    {
        public class Particle
        {
            public Vector2 position;
            public Vector2 size;

            public Vector2 velocity;
            public Vector2 acceleration;

            public float rotation;
            public float life;

            public Color color;
            public float alpha;
        }

        public class Emitter
        {
            List<Particle> m_particles;
            float m_elapsedEmittionTime;

            public Vector2 position;
            public Vector2 emissionSize;
            public float emissionRate;

            public float minLife, maxLife;
            public float minSize, maxSize;
            public Vector2 minVelocity, maxVelocity;
            public float gravity;
            public float wind;
            public float transparency;

            public Texture2D texture;

            public Emitter(Texture2D particleTexture, Vector2 pos)
            {
                // CHANGE THESE VALUES TO EFFECT HOW THE EMITTER EMITTS PARTICLES
                //-----------------------------------------------------------------
                position = pos;      // starting position of the emitter
                emissionRate = 10.0f;
                minLife = 3.5f;
                maxLife = 4.5f;
                minSize = 8.0f;
                maxSize = 32.0f;
                minVelocity.Y = -50.0f;
                maxVelocity.Y = 50.0f;
                minVelocity.X = -50.0f;
                maxVelocity.X = 50.0f;
                gravity = 0.0f;
                wind = 0.0f;
                transparency = 0.75f;
                //-----------------------------------------------------------------
             

                emissionSize = new Vector2(5.0f, 5.0f);

                texture = particleTexture;

                m_particles = new List<Particle>();
                m_elapsedEmittionTime = 0.0f;
            }

            public void Update(GameTime gameTime)
            {
                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
                m_elapsedEmittionTime += dt;

                while (m_elapsedEmittionTime > (1.0f / emissionRate))
                {
                    SpawnParticle();
                    m_elapsedEmittionTime -= (1.0f / emissionRate);
                }

                for (int i = m_particles.Count - 1; i >= 0; --i)
                {
                    Particle p = m_particles[i];

                    p.life -= dt;
                    if (p.life <= 0.0f) m_particles.RemoveAt(i);

                    p.acceleration.Y += gravity * dt;
                    p.acceleration.X += wind * dt;

                    p.velocity += p.acceleration * dt;
                    p.position.X += p.velocity.X * dt;
                    p.position.Y -= p.velocity.Y * dt;

                    if (p.life <= 1.0f)
                        p.alpha = p.life * transparency;
                }
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                Vector2 textureSize = new Vector2(texture.Width, texture.Height);
                Vector2 origin = textureSize / 2.0f;

                foreach (Particle p in m_particles)
                {
                    Vector2 scale = p.size / textureSize;
                    //spriteBatch.Begin();
                    spriteBatch.Draw(texture, p.position, null, p.color * p.alpha, p.rotation, origin, scale, SpriteEffects.None, 0.0f);
                    Debug.WriteLine("Boom");
                    //spriteBatch.End();
                }
            }

            void SpawnParticle()
            {
                Particle p = new Particle();

                p.life = fRand(minLife, maxLife);
                p.rotation = 0.0f;
                p.color = Color.White;
                p.acceleration = new Vector2(wind, -gravity);
                p.velocity = new Vector2(fRand(minVelocity.X, maxVelocity.X), fRand(minVelocity.Y, maxVelocity.Y));
                p.position = new Vector2(fRand(-emissionSize.X, emissionSize.X) + position.X,
                                              fRand(-emissionSize.Y, emissionSize.Y) + position.Y);
                p.size = new Vector2(fRand(minSize, maxSize));
                p.alpha = transparency;

                m_particles.Add(p);
            }

            Random rand = new Random();
            float fRand(float start, float end)
            {
                float num = (float)rand.NextDouble();
                return start + (end - start) * num;
            }


            // STATIC FUNCTIONS TO BUILD EMITTERS WITH SOME PRESET SETTINGS
            //-------------------------------------------------------------
            public static Emitter CreateBurstEmitter(Texture2D particleTexture, Vector2 pos)
            {
                Emitter e = new Emitter(particleTexture, pos);
                return e;
            }

            public static Emitter CreateFireEmitter(Texture2D particleTexture, Vector2 pos)
            {
                Emitter e = new Emitter(particleTexture, pos);
                e.gravity = 0.0f;

                e.minLife = 0.25f;
                e.maxLife = 2.0f;

                e.minVelocity = new Vector2(0.0f, 0.0f);
                e.maxVelocity = new Vector2(0.0f, 100.0f);

                e.emissionRate = 1000.0f;

                e.emissionSize = new Vector2(10.0f, 1.0f);
                e.transparency = 0.15f;

                return e;
            }

            public static Emitter CreateFlyingStarsEmitter(Texture2D particleTexture, Vector2 pos)
            {
                Emitter e = new Emitter(particleTexture, pos);
                e.emissionSize = new Vector2(300, 0);
                e.emissionRate = 100.0f;
                e.minLife = 2.0f;
                e.maxLife = 7.0f;
                e.transparency = 0.20f;
                e.minVelocity.X = 0.0f;
                e.maxVelocity.X = 0.0f;
                e.minVelocity.Y = 75.0f;
                e.maxVelocity.Y = 100.0f;
                e.transparency = 0.5f;
                return e;
            }
        }
    }
}
