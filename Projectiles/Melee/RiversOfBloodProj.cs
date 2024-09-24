using System;

using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EldenRingItems.Projectiles.Melee
{
    public class RiversOfBloodProj : ModProjectile
    {
        SoundStyle HitSound = new SoundStyle("EldenRingItems/Sounds/RiversOfBlood/HitOrganic");
        
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 8;
        }

        bool Slashing = false;

        public override void SetDefaults()
        {
            Projectile.width = 136;
            Projectile.height = 185;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.alpha = 0;
            Projectile.frameCounter = 0;
            Projectile.timeLeft = 30;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
            Projectile.scale = 1.15f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.frameCounter <= 1)
                return false;
            Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            Rectangle frame = texture.Frame(verticalFrames: Main.projFrames[Type], frameY: Projectile.frame);
            Vector2 origin = frame.Size() * 0.5f;
            SpriteEffects spriteEffects = Projectile.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + (Projectile.velocity * 0.3f) + new Vector2(0, -32).RotatedBy(Projectile.rotation), frame, Color.White, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
            return false;
        }

        public override void AI()
        {
            if (Main.mouseLeft)
                Slashing = true;
            else if (Main.mouseLeftRelease)
                Slashing = false;

            if (Slashing)
            {
                Projectile.frameCounter++;
                if (Projectile.frameCounter % 3 == 0)
                    Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Type];

                Vector2 playerRotatedPoint = Main.player[Projectile.owner].RotatedRelativePoint(Main.player[Projectile.owner].MountedCenter, true);

                float velocityAngle = Projectile.velocity.ToRotation();
                Projectile.rotation = velocityAngle + (Projectile.direction == -1).ToInt() * MathHelper.Pi;
                float velocityAngle2 = Projectile.velocity.ToRotation();
                Projectile.direction = (Math.Cos(velocityAngle2) > 0).ToDirectionInt();

                float offset = 50f * Projectile.scale;
                Projectile.Center = playerRotatedPoint + velocityAngle2.ToRotationVector2() * offset;

                int dust = Dust.NewDust(new Vector2(Projectile.Hitbox.X, Projectile.Hitbox.Y), Projectile.width, Projectile.height, DustID.Blood, Scale: Main.rand.NextFloat(0.5f, 1.5f));
            }
            else
                Projectile.Kill();
        }

        public override bool? CanDamage()
        {
            if (Slashing)
                return true;
            else
                return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            HitSound.Volume = Main.rand.NextFloat(0.2f, 0.45f);
            HitSound.Pitch = Main.rand.NextFloat(-0.1f, 0.1f);
            SoundEngine.PlaySound(HitSound);
        }
    }
}