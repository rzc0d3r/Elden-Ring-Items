using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EldenRingItems.Projectiles.Melee
{
    public class BloodSlash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 200;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 1;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.75f, 0f, 0f);

            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + MathHelper.PiOver2;

            if (Projectile.alpha > 0)
                Projectile.alpha -= 17;

            Projectile.localAI[1] += 1f;
            if (Projectile.localAI[1] == 12f)
            {
                Projectile.localAI[1] = 0f;
                for (int l = 0; l < 12; l++)
                {
                    Vector2 dustRotation = Vector2.UnitX * -Projectile.width / 2f;
                    dustRotation += -Vector2.UnitY.RotatedBy(l * MathHelper.Pi / 6f) * new Vector2(8f, 16f);
                    dustRotation = dustRotation.RotatedBy(Projectile.rotation - MathHelper.PiOver2);
                    int rougeDust = Dust.NewDust(Projectile.Center, 0, 0, DustID.RedTorch, 0f, 0f, 160, default, 2f);
                    Main.dust[rougeDust].noGravity = true;
                    Main.dust[rougeDust].position = Projectile.Center + dustRotation;
                    Main.dust[rougeDust].velocity = Projectile.velocity * 0.1f;
                    Main.dust[rougeDust].velocity = Vector2.Normalize(Projectile.Center - Projectile.velocity * 3f - Main.dust[rougeDust].position) * 1.25f;
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDon)
        {
            Player player = Main.player[Projectile.owner];
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.statLife < player.statLifeMax2)
                {
                    int healAmouth = Main.rand.Next(1, 6);
                    player.statLife += healAmouth;
                    player.HealEffect(healAmouth, true);
                }
            }
        }
    }
}
