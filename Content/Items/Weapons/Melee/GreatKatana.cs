using System;

using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.Localization;

using Microsoft.Xna.Framework;

using EldenRingItems.Content.Items.Materials.SomberSmithingStones;

namespace EldenRingItems.Content.Items.Weapons.Melee
{
    public class GreatKatana : ModItem
    {
        public override string Texture => "EldenRingItems/Content/Items/Weapons/Melee/GreatKatana";
        public int BaseDamage { get; set; } = 35;

        SoundStyle IsChargedSound = new SoundStyle("EldenRingItems/Sounds/GreatKatana/ChargeSound");

        bool CriticalHit = false;
        public int CriticalHitConsequent { get; set; } = 12;
        public float DamageMultiplier { get; set; } = 1.1f;

        public override LocalizedText DisplayName => base.DisplayName.WithFormatArgs("");
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(Double.Round((1.0/CriticalHitConsequent)*100, 2), DamageMultiplier);

        public override void SetDefaults()
        {
            Item.width = 86;
            Item.height = 86;
            Item.DamageType = DamageClass.Melee;
            Item.damage = BaseDamage;
            Item.knockBack = 6f;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 12, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.scale = 1.5f;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Pot, Scale: Main.rand.NextFloat(0.3f, 0.8f));
        }

        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            if (CriticalHit)
                crit = 100;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (CriticalHit)
                damage *= DamageMultiplier;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(CriticalHit)
            {
                SoundEngine.PlaySound(SoundID.Item152);
                CriticalHit = false;
            }
            if (Main.rand.NextBool(CriticalHitConsequent))
                CriticalHit = true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.Katana);
            r.AddIngredient(ItemID.Muramasa);
            r.AddIngredient(ItemID.CobaltBar, 8);
            r.AddTile(TileID.Anvils);
            r.Register();

            Recipe r2 = CreateRecipe();
            r2.AddIngredient(ItemID.Katana);
            r2.AddIngredient(ItemID.Muramasa);
            r2.AddIngredient(ItemID.PalladiumBar, 8);
            r2.AddTile(TileID.Anvils);
            r2.Register();
        }
    }

    #region Upgrade
    public abstract class UpgradedGreatKatana : GreatKatana
    {
        public int UpgradeLevel { get; set; }
        public Recipe recipe { get; set; }
        public override LocalizedText Tooltip => ModContent.GetModItem(ModContent.ItemType<GreatKatana>()).Tooltip.WithFormatArgs(Double.Round((1.0/(CriticalHitConsequent))*100, 2), Double.Round(DamageMultiplier, 2));
        public override LocalizedText DisplayName => ModContent.GetModItem(ModContent.ItemType<GreatKatana>()).DisplayName.WithFormatArgs($" +{UpgradeLevel}");

        protected UpgradedGreatKatana(int upgradeLevel)
        {
            UpgradeLevel = upgradeLevel;
            CriticalHitConsequent -= upgradeLevel;
            DamageMultiplier += UpgradeLevel / 25f;
        }

        public override void SetDefaults()
        {
            if (UpgradeLevel <= 4)
                BaseDamage += UpgradeLevel * 3;
            else
                BaseDamage += UpgradeLevel * 8;
            base.SetDefaults();
        }

        public override void AddRecipes()
        {
            recipe = CreateRecipe();
            recipe.AddIngredient(SSSUtils.GetSSSByLevel(UpgradeLevel));
            recipe.AddTile(SSSUtils.GetTileByLevel(UpgradeLevel));
        }
    }

    public class GreatKatana1 : UpgradedGreatKatana
    {
        public GreatKatana1() : base(1) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<GreatKatana>());
            recipe.Register();
        }
    }

    public class GreatKatana2 : UpgradedGreatKatana
    {
        public GreatKatana2() : base(2) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<GreatKatana1>());
            recipe.Register();
        }
    }

    public class GreatKatana3 : UpgradedGreatKatana
    {
        public GreatKatana3() : base(3) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<GreatKatana2>());
            recipe.Register();
        }
    }
    public class GreatKatana4 : UpgradedGreatKatana
    {
        public GreatKatana4() : base(4) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<GreatKatana3>());
            recipe.Register();
        }
    }

    public class GreatKatana5 : UpgradedGreatKatana
    {
        public GreatKatana5() : base(5) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<GreatKatana4>());
            recipe.Register();
        }
    }

    public class GreatKatana6 : UpgradedGreatKatana
    {
        public GreatKatana6() : base(6) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<GreatKatana5>());
            recipe.Register();
        }
    }

    public class GreatKatana7 : UpgradedGreatKatana
    {
        public GreatKatana7() : base(7) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<GreatKatana6>());
            recipe.Register();
        }
    }

    public class GreatKatana8 : UpgradedGreatKatana
    {
        public GreatKatana8() : base(8) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<GreatKatana7>());
            recipe.Register();
        }
    }

    public class GreatKatana9 : UpgradedGreatKatana
    {
        public GreatKatana9() : base(9) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<GreatKatana8>());
            recipe.Register();
        }
    }

    public class GreatKatana10 : UpgradedGreatKatana
    {
        public GreatKatana10() : base(10) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<GreatKatana9>());
            recipe.Register();
        }
    }
    #endregion
}
