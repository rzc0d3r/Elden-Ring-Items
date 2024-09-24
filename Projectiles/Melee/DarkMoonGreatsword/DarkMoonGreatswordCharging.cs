using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.GameContent;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using EldenRingItems.Content.Items.Weapons.Melee;

namespace EldenRingItems.Projectiles.Melee.DarkMoonGreatsword
{
    public class DarkMoonGreatswordCharging : ModProjectile
    {
        public override string Texture => "EldenRingItems/Content/Items/Weapons/Melee/DarkMoonGreatsword";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 92;
            Projectile.height = 91;
            Projectile.aiStyle = 1;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = false;
            Projectile.friendly = false;
            AIType = ProjectileID.Bullet;
        }

        public float movementFactor
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (owner.HeldItem.ModItem is DarkMoonGreatsword0)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
                Projectile.position = owner.position;
                Projectile.position.Y -= 120;
                Projectile.position.X -= 35;

                if (Projectile.spriteDirection == -1)
                    Projectile.rotation -= MathHelper.ToRadians(90f);

                Projectile.velocity *= 0.90f;
                if (Projectile.timeLeft < 120 && Projectile.timeLeft > 100)
                    Dust.NewDust(new Vector2(Projectile.Center.X - 3, Projectile.Center.Y), 0, 0, DustID.FireworkFountain_Blue, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-9, 9), 150, default, 0.2f);
                if (Projectile.timeLeft < 100 && Projectile.timeLeft > 80)
                    Dust.NewDust(new Vector2(Projectile.Center.X - 3, Projectile.Center.Y), 0, 0, DustID.FireworkFountain_Blue, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-9, 9), 150, default, 0.6f);
                if (Projectile.timeLeft < 80 && Projectile.timeLeft > 60)
                    Dust.NewDust(new Vector2(Projectile.Center.X - 3, Projectile.Center.Y), 0, 0, DustID.FireworkFountain_Blue, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-9, 9), 150, default, 1.1f);
                if (Projectile.timeLeft < 60 && Projectile.timeLeft > 40)
                {
                    for (int d = 0; d < 3; d++)
                        Dust.NewDust(new Vector2(Projectile.Center.X - 3, Projectile.Center.Y), 0, 0, DustID.FireworkFountain_Blue, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-14, 14), 150, default, 1.5f);
                }

                if (Projectile.timeLeft == 25)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0, -0.01f, ModContent.ProjectileType<DarkMoonGreatswordCharged>(), 0, 0, owner.whoAmI, 0f);
                    for (int d = 0; d < 14; d++)
                        Dust.NewDust(new Vector2(Projectile.Center.X - 3, Projectile.Center.Y), 0, 0, DustID.PurificationPowder, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-15, 15), 150, default, 1.5f);
                    for (int d = 0; d < 16; d++)
                        Dust.NewDust(new Vector2(Projectile.Center.X - 3, Projectile.Center.Y), 0, 0, DustID.FireworkFountain_Blue, 0f + Main.rand.Next(-16, 16), 0f, 150, default, 1.5f);
                    for (int d = 0; d < 16; d++)
                        Dust.NewDust(new Vector2(Projectile.Center.X - 3, Projectile.Center.Y), 0, 0, DustID.PurificationPowder, 0f + Main.rand.Next(-6, 6), 0f, 150, default, 1.5f);
                    for (int d = 0; d < 10; d++)
                        Dust.NewDust(new Vector2(Projectile.Center.X - 3, Projectile.Center.Y), 0, 0, DustID.FireworkFountain_Blue, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-23, 23), 150, default, 1.5f);
                    Projectile.Kill();
                }
            }
            else
                Projectile.Kill();
        }

        public override bool PreDraw(ref Color lightColor) // Redraw the projectile with the color not influenced by light
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Width() * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw((Texture2D)TextureAssets.Projectile[Projectile.type], drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }
            return true;
        }
    }

    public class DarkMoonGreatswordCharged : DarkMoonGreatswordCharging
    {
        public override string Texture => "EldenRingItems/Projectiles/Melee/DarkMoonGreatsword/DarkMoonGreatswordCharged";
        
        SoundStyle ChargingSound = new SoundStyle("EldenRingItems/Sounds/cs_c2010.649");

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            if (owner.HeldItem.ModItem is DarkMoonGreatsword0 dmg0)
            {
                Projectile.position = owner.position;
                Projectile.position.Y -= 120;
                Projectile.position.X -= 35;

                Projectile.rotation = MathHelper.ToRadians(45f) + MathHelper.ToRadians(-90f);
                if (Projectile.spriteDirection == -1)
                    Projectile.rotation -= MathHelper.ToRadians(90f);
                Projectile.velocity *= 0.1f;

                if (Projectile.timeLeft == 115)
                {
                    dmg0.Charging = false;
                    dmg0.IsCharged = true;
                    ChargingSound.Volume = 0.6f;
                    SoundEngine.PlaySound(ChargingSound);
                }

                if (Projectile.timeLeft <= 108)
                    Projectile.alpha += 29;
                if (Projectile.alpha >= 255)
                    Projectile.Kill();
            }
            else
                Projectile.Kill();
        }
    }
}