using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ModLoader;

using ReLogic.Content;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EldenRingItems.MainMenu
{
    public class ERIMainMenu : ModMenu
    {
        public class Particle
        {
            public int Time;
            public int Lifetime;
            public int IdentityIndex;
            public float Scale;
            public float Depth;
            public Color DrawColor;
            public Vector2 Velocity;
            public Vector2 Center;

            public Particle(int lifetime, int identity, float depth, Color color, Vector2 startingPosition, Vector2 startingVelocity)
            {
                Lifetime = lifetime;
                IdentityIndex = identity;
                Depth = depth;
                DrawColor = color;
                Center = startingPosition;
                Velocity = startingVelocity;
            }
        }

        public static List<Particle> Particles
        {
            get;
            internal set;
        } = new();

        public override string DisplayName => "Elden Ring Style";

        public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>("EldenRingItems/MainMenu/Logo");

        //public override int Music => 

        // Before drawing the logo, draw the entire EldenRingItems background. This way, the typical parallax background is skipped entirely.
        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("EldenRingItems/MainMenu/MenuBackground").Value;

            // Calculate the draw position offset and scale in the event that someone is using a non-16:9 monitor
            Vector2 drawOffset = Vector2.Zero;
            float xScale = (float)Main.screenWidth / texture.Width;
            float yScale = (float)Main.screenHeight / texture.Height;
            float scale = xScale;

            // if someone's monitor isn't in wacky dimensions, no calculations need to be performed at all
            if (xScale != yScale)
            {
                // If someone's monitor is tall, it needs to be shifted to the left so that it's still centered on screen
                // Additionally the Y scale is used so that it still covers the entire screen
                if (yScale > xScale)
                {
                    scale = yScale;
                    drawOffset.X -= (texture.Width * scale - Main.screenWidth) * 0.5f;
                }
                else
                    // The opposite is true if someone's monitor is widescreen
                    drawOffset.Y -= (texture.Height * scale - Main.screenHeight) * 0.5f;
            }

            spriteBatch.Draw(texture, drawOffset, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            static Color selectParticleColor()
            {
                if (Main.rand.NextBool(4))
                    return Color.Lerp(Color.Goldenrod, Color.LightGoldenrodYellow, Main.rand.NextFloat());

                return Color.Lerp(Color.DarkGoldenrod, Color.Yellow, Main.rand.NextFloat(0.9f));
            }

            // Randomly add Particles
            for (int i = 0; i < 5; i++)
            {
                if (Main.rand.NextBool(3))
                {
                    int lifetime = Main.rand.Next(200, 300);
                    float depth = Main.rand.NextFloat(1.8f, 5f);
                    Vector2 startingPosition = new Vector2(Main.screenWidth * Main.rand.NextFloat(-0.1f, 1.1f), Main.screenHeight * 1.05f);
                    Vector2 startingVelocity = -Vector2.UnitY.RotatedBy(Main.rand.NextFloat(-0.9f, 0.9f)) * 4f;
                    Color particleColor = selectParticleColor();
                    Particles.Add(new Particle(lifetime, Particles.Count, depth, particleColor, startingPosition, startingVelocity));
                }
            }

            // Update all Particles
            for (int i = 0; i < Particles.Count; i++)
            {
                Particles[i].Scale = Utils.GetLerpValue(Particles[i].Lifetime, Particles[i].Lifetime / 3, Particles[i].Time, true);
                Particles[i].Scale *= MathHelper.Lerp(0.6f, 0.9f, Particles[i].IdentityIndex % 6f / 6f);
                if (Particles[i].IdentityIndex % 13 == 12)
                    Particles[i].Scale *= 2f;

                float flySpeed = MathHelper.Lerp(3.2f, 14f, Particles[i].IdentityIndex % 21f / 21f);
                Vector2 idealVelocity = -Vector2.UnitY.RotatedBy(MathHelper.Lerp(-0.44f, 0.44f, (float)Math.Sin(Particles[i].Time / 16f + Particles[i].IdentityIndex) * 0.5f + 0.5f));
                idealVelocity = (idealVelocity + Vector2.UnitX).SafeNormalize(Vector2.UnitY) * flySpeed;

                float movementInterpolant = MathHelper.Lerp(0.01f, 0.08f, Utils.GetLerpValue(45f, 145f, Particles[i].Time, true));
                Particles[i].Velocity = Vector2.Lerp(Particles[i].Velocity, idealVelocity, movementInterpolant);

                Particles[i].Time++;
                Particles[i].Center += Particles[i].Velocity;
            }

            // Clear away all dead Particles
            Particles.RemoveAll(c => c.Time >= c.Lifetime);

            // Draw particles
            Texture2D particleTexture = ModContent.Request<Texture2D>("EldenRingItems/MainMenu/Particle").Value;
            for (int i = 0; i < Particles.Count; i++)
            {
                Vector2 drawPosition = Particles[i].Center;
                spriteBatch.Draw(particleTexture, drawPosition, null, Particles[i].DrawColor, 0f, particleTexture.Size() * 0.5f, Particles[i].Scale, 0, 0f);
            }

            // Set the logo draw color to be white and the time to be noon
            // This is because there is not a day/night cycle in this menu, and changing colors would look bad
            drawColor = Color.White;
            Main.time = 27000;
            Main.dayTime = true;

            // Draw the logo using a different spritebatch blending setting so it doesn't have a horrible yellow glow
            Vector2 drawPos = new Vector2(Main.screenWidth / 2f, 100f);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
            spriteBatch.Draw(Logo.Value, drawPos, null, drawColor, logoRotation, Logo.Value.Size() * 0.5f, logoScale, SpriteEffects.None, 0f);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
            return false;
        }
    }
}
