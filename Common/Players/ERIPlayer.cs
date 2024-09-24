using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

using EldenRingItems.Content.Buffs.StatDebuff;

namespace EldenRingItems.Common.Players
{
    public class ERIPlayer : ModPlayer
    {
        public bool Blessed = false;
        public bool WeaponImbueBlackFlame = false;
        
        public override void ResetEffects()
        {
             WeaponImbueBlackFlame = false;
        }

        public override void OnRespawn()
        {
            Blessed = false;
        }

        public override void MeleeEffects(Item item, Rectangle hitbox)
        {
            if (WeaponImbueBlackFlame)
            {
                if (item.DamageType.CountsAsClass<MeleeDamageClass>() && !item.noMelee && !item.noUseGraphic) {
                    if (Main.rand.NextBool(2))
                        Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Wraith, Scale: Main.rand.NextFloat(0.5f, 0.8f));
                }
            }
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        { 
            if (WeaponImbueBlackFlame && item.DamageType.CountsAsClass<MeleeDamageClass>())
                target.AddBuff(ModContent.BuffType<BlackFlameDebuff>(), 60 * 3); // 3 seconds
        }
    }
}