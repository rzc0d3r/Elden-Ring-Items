using System;

using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

namespace EldenRingItems.Projectiles.Melee
{
    public class MoonveilSlash : ModProjectile
    {
        public SoundStyle hitSound = new SoundStyle("EldenRingItems/Sounds/cs_c3320_13");

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 36;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + MathHelper.PiOver2;
            Dust d1 = Dust.NewDustPerfect(Projectile.Top, DustID.BlueTorch, Scale: Main.rand.NextFloat(0.5f, 1.2f));
            Dust d2 = Dust.NewDustPerfect(Projectile.Center, DustID.BlueTorch, Scale: Main.rand.NextFloat(0.5f, 1.2f));
            Dust d3 = Dust.NewDustPerfect(Projectile.Bottom, DustID.BlueTorch, Scale: Main.rand.NextFloat(0.5f, 1.2f));
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!target.HasBuff(BuffID.Frostburn))
                target.AddBuff(BuffID.Frostburn, 60 * 15); // 15 seconds
            Player player = Main.player[Projectile.owner];
            float numberOfDusts = 24f;
            float rotFactor = 360f / numberOfDusts;
            for (int i = 0; i < numberOfDusts; i++)
            {
                float rot = MathHelper.ToRadians(i * rotFactor);
                Vector2 offset = new Vector2(18f, 0).RotatedBy(rot * Main.rand.NextFloat(1.1f, 4.1f));
                Vector2 velOffset = new Vector2(12f, 0).RotatedBy(rot * Main.rand.NextFloat(1.1f, 4.1f));
                Dust dust = Dust.NewDustPerfect(Projectile.Center + offset, DustID.Electric, new Vector2(velOffset.X, velOffset.Y));
                dust.noGravity = true;
                dust.velocity = velOffset;
                dust.scale = Main.rand.NextFloat(0.8f, 1.5f);
            }
            Projectile.Kill();
        }
    }
}
