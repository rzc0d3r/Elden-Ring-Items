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
        public bool CrimsonSeedTalismanIsActive = false;
        public bool CeruleanSeedTalismanIsActive = false;
        public float HealingPotionMultiplier = 1f;
        public float ManaPotionMultiplier = 1f;
        public float ManaReduce = 0f;

        public override void ResetEffects()
        {
             WeaponImbueBlackFlame = false;
             HealingPotionMultiplier = 1f;
             ManaPotionMultiplier = 1f;
             CrimsonSeedTalismanIsActive = false;
             CeruleanSeedTalismanIsActive = false;
             ManaReduce = 0f;
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

        public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
        {
            healValue = (int)(healValue * HealingPotionMultiplier);
        }

        public override void GetHealMana(Item item, bool quickHeal, ref int healValue)
        {
            healValue = (int)(healValue * ManaPotionMultiplier);
        }

        public override void ModifyManaCost(Item item, ref float reduce, ref float mult)
        {
            reduce -= ManaReduce;
        }
    }
}