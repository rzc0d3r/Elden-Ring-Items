using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using EldenRingItems.Projectiles.Melee;
using Terraria.Audio;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;

namespace EldenRingItems.Content.Items.Weapons.Melee
{
    public class BlasphemousBlade : ModItem
    {
        // MouseRight holding bonus
        float HoldingDamageBonus = 1f;
        float HoldingCount = 0;
        bool BladeIsCharged = false;
        int MadeChargedAttacks = 0;
        bool BonusLimit = false;

        // Blade default properties (without holding bonus)
        int BaseDamage = 55;
        float BaseKnockBack = 5f;
        float BaseShootSpeed = 14f;

        SoundStyle BladeMaxChargeSound = new SoundStyle("EldenRingItems/Sounds/TogethaAsFamilee");
        SoundStyle BladeStartChargingSound = new SoundStyle("EldenRingItems/Sounds/DemonshadeEnrage"); 
        SoundStyle BladeShootSound = SoundID.Item60;
        
        //public override void SetStaticDefaults()
        //{
        //    // 20 - Number of ticks after which the frame will change (3.5 frames per 60FPS); 8 - Quantity of frames in a blade texture
        //    Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(17, 8));
        //    // Allows animation rendering not only in the inventory, but also in the world
        //    ItemID.Sets.AnimatesAsSoul[Type] = true;
        //}

        public override void SetDefaults()
        {
            Item.width = 92;
            Item.height = 92;
            Item.DamageType = DamageClass.Melee;
            Item.damage = BaseDamage;
            Item.knockBack = BaseKnockBack;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 75, 0, 0);
            Item.rare = ItemRarityID.LightRed;

            Item.shoot = ModContent.ProjectileType<BloodSlash>();
            Item.shootSpeed = BaseShootSpeed;
        
        }
        void ResetBladeBonus()
        {
            HoldingCount = 0;
            MadeChargedAttacks = 0;
            HoldingDamageBonus = 1f;
            BladeIsCharged = false;
            BonusLimit = false;
            Item.shoot = ModContent.ProjectileType<BloodSlash>();
            Item.damage = BaseDamage;
            Item.knockBack = BaseKnockBack;
            Item.shootSpeed = BaseShootSpeed;
            Item.useTime = 30;
            Item.useAnimation = 30;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (BladeIsCharged)
            {
                if (MadeChargedAttacks > 4) // 5 attacks (0, 1, 2, 3, 4)
                {
                    ResetBladeBonus();
                    return false;
                }
                else
                {
                    MadeChargedAttacks++;
                    SoundEngine.PlaySound(SoundID.Item73, position);
                    player.Heal(Main.rand.Next(15, 31));
                }
            }
            else
            {
                BladeShootSound.Volume = 0.5f;
                SoundEngine.PlaySound(BladeShootSound, position);
            }
            return true;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (Main.mouseRight)
            {
                if (HoldingDamageBonus < 3f)
                {
                    HoldingCount++;
                    HoldingDamageBonus += 0.003f;
                    Item.damage = Convert.ToInt32(BaseDamage * (HoldingDamageBonus/1.7));
                    if (HoldingCount == 25) // Take away player's health every 25 mouseRight-holding events processed
                    {
                        player.Hurt(PlayerDeathReason.ByCustomReason(""), Main.rand.Next(10, 21), player.direction, armorPenetration: 1000);
                        HoldingCount = 0;
                    }
                }
                else
                    if (!BonusLimit) // Will only play the blade max charge sound 1 time
                    {
                        BonusLimit = true;
                        BladeMaxChargeSound.Volume = 0.8f;
                        SoundEngine.PlaySound(BladeMaxChargeSound, player.position);
                    }
                if (HoldingDamageBonus >= 2f && !BladeIsCharged) // The blade is charged when the damage multiplier >= 2x
                {
                    // Changing blade properties
                    SoundEngine.PlaySound(BladeStartChargingSound, player.position); 
                    BladeIsCharged = true;
                    Item.shoot = ProjectileID.InfernoFriendlyBlast;
                    Item.shootSpeed = 20f;
                    Item.knockBack = 9f;
                    Item.useTime = 16;
                    Item.useAnimation = 16;
                }
            }
        }
    }
}
