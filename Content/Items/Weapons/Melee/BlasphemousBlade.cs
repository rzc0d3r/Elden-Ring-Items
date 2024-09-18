using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using EldenRingItems.Projectiles.Melee;
using Terraria.Audio;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;
using Terraria.Localization;
using EldenRingItems.Content.Items.Materials.SomberSmithingStones;

namespace EldenRingItems.Content.Items.Weapons.Melee
{
    public class BlasphemousBlade : ModItem
    {
        public override string Texture => "EldenRingItems/Content/Items/Weapons/Melee/BlasphemousBlade";

        const int MAX_CHARGED_ATTACKS = 5;
        const int MIN_HEAL = 10;
        const int MAX_HEAL = 25;

        public override LocalizedText DisplayName => base.DisplayName.WithFormatArgs("");
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MIN_HEAL*MAX_CHARGED_ATTACKS, MAX_HEAL*MAX_CHARGED_ATTACKS, MAX_CHARGED_ATTACKS);

        float HoldingCount = 0;
        bool BladeIsCharged = false;
        int MadeChargedAttacks = 0;
        int HurtCount = 0;

        public int BaseDamage { get; set; } = 55;
        float BaseKnockBack = 5f;
        float BaseShootSpeed = 14f;

        SoundStyle BladeIsChargedSound = new SoundStyle("EldenRingItems/Sounds/cs_c2010.832");
        SoundStyle BladeIsDichargedSound = new SoundStyle("EldenRingItems/Sounds/cs_c2010.833");
        SoundStyle DefaultShootSound = SoundID.Item60;
        SoundStyle ChargedShootSound = SoundID.Item73;

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
            HurtCount = 0;
            MadeChargedAttacks = 0;
            BladeIsCharged = false;
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
                MadeChargedAttacks++;
                if (MadeChargedAttacks == MAX_CHARGED_ATTACKS)
                {
                    BladeIsDichargedSound.Volume = 1f;
                    SoundEngine.PlaySound(BladeIsDichargedSound);
                    ResetBladeBonus();
                    return true;
                }
                SoundEngine.PlaySound(ChargedShootSound);
            }
            else
            {
                DefaultShootSound.Volume = 0.8f;
                SoundEngine.PlaySound(DefaultShootSound);
            }
            return true;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (Main.mouseRight)
            {
                if (HurtCount < 7)
                {
                    HoldingCount++;
                    if (HoldingCount == 12) // every 12 mouseRight-holding events processed
                    {
                        HoldingCount = 0;
                        HurtCount++;
                    }
                }
                else if (HurtCount == 7 && !BladeIsCharged)
                {
                    // Changing blade properties
                    BladeIsChargedSound.Volume = 0.65f;
                    SoundEngine.PlaySound(BladeIsChargedSound); 
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

    #region Upgrade
    public abstract class UpgradedBlasphemousBlade : BlasphemousBlade
    {
        public int UpgradeLevel { get; set; }
        public Recipe recipe { get; set; }
        public override LocalizedText Tooltip => ModContent.GetModItem(ModContent.ItemType<BlasphemousBlade>()).Tooltip;
        public override LocalizedText DisplayName => ModContent.GetModItem(ModContent.ItemType<BlasphemousBlade>()).DisplayName.WithFormatArgs($" +{UpgradeLevel}");
        
        protected UpgradedBlasphemousBlade(int upgradeLevel)
        {
            UpgradeLevel = upgradeLevel;
        }

        public override void SetDefaults()
        {
            if (UpgradeLevel <= 5)
                BaseDamage += UpgradeLevel * 3;
            else
                BaseDamage += UpgradeLevel * 10;
            base.SetDefaults();
        }

        public override void AddRecipes()
        {
            recipe = CreateRecipe();
            recipe.AddIngredient(SSSUtils.GetSSSByLevel(UpgradeLevel));
            recipe.AddTile(SSSUtils.GetTileByLevel(UpgradeLevel));
        }
    }

    public class BlasphemousBlade1 : UpgradedBlasphemousBlade
    {
        public BlasphemousBlade1() : base(1) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<BlasphemousBlade>());
            recipe.Register();
        }
    }

    public class BlasphemousBlade2 : UpgradedBlasphemousBlade
    {
        public BlasphemousBlade2() : base(2) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<BlasphemousBlade1>());
            recipe.Register();
        }
    }

    public class BlasphemousBlade3 : UpgradedBlasphemousBlade
    {
        public BlasphemousBlade3() : base(3) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<BlasphemousBlade2>());
            recipe.Register();
        }
    }

    public class BlasphemousBlade4 : UpgradedBlasphemousBlade
    {
        public BlasphemousBlade4() : base(4) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<BlasphemousBlade3>());
            recipe.Register();
        }
    }

    public class BlasphemousBlade5 : UpgradedBlasphemousBlade
    {
        public BlasphemousBlade5() : base(5) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<BlasphemousBlade4>());
            recipe.Register();
        }
    }

    public class BlasphemousBlade6 : UpgradedBlasphemousBlade
    {
        public BlasphemousBlade6() : base(6) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<BlasphemousBlade5>());
            recipe.Register();
        }
    }

    public class BlasphemousBlade7 : UpgradedBlasphemousBlade
    {
        public BlasphemousBlade7() : base(7) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<BlasphemousBlade6>());
            recipe.Register();
        }
    }

    public class BlasphemousBlade8 : UpgradedBlasphemousBlade
    {
        public BlasphemousBlade8() : base(8) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<BlasphemousBlade7>());
            recipe.Register();
        }
    }

    public class BlasphemousBlade9 : UpgradedBlasphemousBlade
    {
        public BlasphemousBlade9() : base(9) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<BlasphemousBlade8>());
            recipe.Register();
        }
    }

    public class BlasphemousBlade10 : UpgradedBlasphemousBlade
    {
        public BlasphemousBlade10() : base(10) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<BlasphemousBlade9>());
            recipe.Register();
        }
    }
    #endregion
}
