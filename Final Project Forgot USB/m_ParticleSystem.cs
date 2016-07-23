using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Final_Project_Forgot_USB
{
    class m_ParticleSystem
    {
        public class m_Particle
        {
            public Vector2 m_position;
            public Vector2 m_size;

            public Vector2 m_velocity;
            public Vector2 m_acceleration;

            public float m_rotation;
            public float m_life;

            public Color m_color;
            public float m_alpha;
        }

        public class m_Emitter
        {
            List<m_Particle> m_particles;
            float m_moneyElapsedEmittionTime;

            public Vector2 m_position;
            public Vector2 m_emissionSize;
            public float m_emissionRate;

            public float m_minLife, m_maxLife;
            public float m_minSize, m_maxSize;
            public Vector2 m_minVelocity, m_maxVelocity;
            public float m_gravity;
            public float m_wind;
            public float m_transparency;

            public Texture2D m_texture;

            public m_Emitter(Texture2D m_particleTexture, Vector2 m_pos)
            {
                // CHANGE THESE VALUES TO EFFECT HOW THE EMITTER EMITTS PARTICLES
                //-----------------------------------------------------------------
                m_position = m_pos;      // starting position of the emitter
                m_emissionRate = 1000.0f;
                m_minLife = 0.5f;
                m_maxLife = 2.0f;
                m_minSize = 1.0f;
                m_maxSize = 5.0f;
                m_minVelocity.Y = -50.0f;
                m_maxVelocity.Y = 50.0f;
                m_minVelocity.X = -50.0f;
                m_maxVelocity.X = 50.0f;
                m_gravity = -200.0f;
                m_wind = -500.0f;
                m_transparency = 0.5f;
                //-----------------------------------------------------------------

                m_emissionSize = new Vector2(5.0f, 5.0f);

                m_texture = m_particleTexture;

                m_particles = new List<m_Particle>();
                m_moneyElapsedEmittionTime = 0.0f;
            }

            public void Update(GameTime gameTime)
            {
                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
                m_moneyElapsedEmittionTime += dt;

                while (m_moneyElapsedEmittionTime > (1.0f / m_emissionRate))
                {
                    SpawnParticle();
                    m_moneyElapsedEmittionTime -= (1.0f / m_emissionRate);
                }

                for (int i = m_particles.Count - 1; i >= 0; --i)
                {
                    m_Particle p = m_particles[i];

                    p.m_life -= dt;
                    if (p.m_life <= 0.0f) m_particles.RemoveAt(i);

                    p.m_acceleration.Y += m_gravity * dt;
                    p.m_acceleration.X += m_wind * dt;

                    p.m_velocity += p.m_acceleration * dt;
                    p.m_position.X += p.m_velocity.X * dt;
                    p.m_position.Y -= p.m_velocity.Y * dt;

                    if (p.m_life <= 1.0f)
                        p.m_alpha = p.m_life * m_transparency;
                }
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                Vector2 textureSize = new Vector2(m_texture.Width, m_texture.Height);
                Vector2 origin = textureSize / 2.0f;

                foreach (m_Particle p in m_particles)
                {
                    Vector2 scale = p.m_size / textureSize;

                    //spriteBatch.Begin();

                    spriteBatch.Draw(m_texture, p.m_position, null, p.m_color * p.m_alpha, p.m_rotation, origin, scale, SpriteEffects.None, 0.0f);

                    //spriteBatch.End();


                }
            }

            void SpawnParticle()
            {
                m_Particle p = new m_Particle();

                p.m_life = fRand(m_minLife, m_maxLife);
                p.m_rotation = 0.0f;
                p.m_color = Color.Green;
                p.m_acceleration = new Vector2(m_wind, -m_gravity);
                p.m_velocity = new Vector2(fRand(m_minVelocity.X, m_maxVelocity.X), fRand(m_minVelocity.Y, m_maxVelocity.Y));
                p.m_position = new Vector2(fRand(-m_emissionSize.X, m_emissionSize.X) + m_position.X,
                                              fRand(-m_emissionSize.Y, m_emissionSize.Y) + m_position.Y);
                p.m_size = new Vector2(fRand(m_minSize, m_maxSize));
                p.m_alpha = m_transparency;

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
            public static m_Emitter CreateBurstEmitter(Texture2D particleTexture, Vector2 pos)
            {
                m_Emitter e = new m_Emitter(particleTexture, pos);
                return e;
            }

            public static m_Emitter CreateFireEmitter(Texture2D particleTexture, Vector2 pos)
            {
                m_Emitter e = new m_Emitter(particleTexture, pos);
                e.m_gravity = 0.0f;

                e.m_minLife = 0.25f;
                e.m_maxLife = 2.0f;

                e.m_minVelocity = new Vector2(0.0f, 0.0f);
                e.m_maxVelocity = new Vector2(0.0f, 100.0f);

                e.m_emissionRate = 1000.0f;

                e.m_emissionSize = new Vector2(10.0f, 1.0f);
                e.m_transparency = 0.15f;

                return e;
            }

            //public static m_Emitter CreateFlyingStarsEmitter(Texture2D particleTexture, Vector2 pos)
            //{
            //    m_Emitter e = new m_Emitter(particleTexture, pos);
            //    e.m_emissionSize = new Vector2(300, 0);
            //    e.m_emissionRate = 100.0f;
            //    e.m_minLife = 2.0f;
            //    e.m_maxLife = 7.0f;
            //    e.m_transparency = 0.20f;
            //    e.m_minVelocity.X = 0.0f;
            //    e.m_maxVelocity.X = 0.0f;
            //    e.m_minVelocity.Y = 75.0f;
            //    e.m_maxVelocity.Y = 100.0f;
            //    e.m_transparency = 0.5f;
            //    return e;
            //}
        }
    }
}
