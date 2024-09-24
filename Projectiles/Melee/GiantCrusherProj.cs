using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

namespace EldenRingItems.Projectiles.Melee
{
    public class GiantCrusherProj : ModProjectile
    {
        public override string Texture => "EldenRingItems/Content/Items/Weapons/Melee/GiantCrusher";
        public static readonly SoundStyle hitSound = new("EldenRingItems/Sounds/GiantCrusherProjHit") { Volume = 0.2f };
        public bool returnProj = false;
        public bool projHadHit = false;

        public override void SetDefaults()
        {
            Projectile.width = 120;
            Projectile.height = 120;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.extraUpdates = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 120;
            Projectile.tileCollide = false;
            Projectile.penetrate = 3;
            Projectile.alpha = 100;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 direction = player.Center - Projectile.Center;
            float distance = direction.Length();

            if (distance > 850f && !returnProj) // maximum distance a projectile can travel
                returnProj = true;
            else if (distance < 80f && returnProj)
                Projectile.Kill();
            else if (returnProj)
            {
                direction.Normalize();
                direction *= 17f;
                Projectile.velocity = direction;
            }

            Lighting.AddLight(Projectile.Center, 0.322f, 0.082f, 0.027f);

            Projectile.rotation += 0.25f * Projectile.direction;
            Projectile.alpha -= 2;
            
            // Stone Dust
            if (Main.rand.NextBool(5)) // only spawn 20% of the time
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Stone, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            float numberOfDusts = 36f;
            float rotFactor = 360f / numberOfDusts;
            for (int i = 0; i < numberOfDusts; i++)
            {
                float rot = MathHelper.ToRadians(i * rotFactor);
                Vector2 offset = new Vector2(3.6f, 0).RotatedBy(rot * Main.rand.NextFloat(1.1f, 4.1f));
                Vector2 velOffset = new Vector2(3f, 0).RotatedBy(rot * Main.rand.NextFloat(1.1f, 4.1f));
                Dust dust = Dust.NewDustPerfect(Projectile.Center + offset, DustID.Stone, new Vector2(velOffset.X, velOffset.Y));
                dust.noGravity = true;
                dust.velocity = velOffset;
                dust.scale = Main.rand.NextFloat(1f, 2f);
            }
            if (!projHadHit) // fixes a bug with repeated playback of the hit sound
            {
                SoundEngine.PlaySound(hitSound, Projectile.Center);
                projHadHit = true;
            }
            Projectile.ai[1] = target.whoAmI;
        }
    }
}
