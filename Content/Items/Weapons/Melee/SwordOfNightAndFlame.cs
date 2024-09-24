using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

using Microsoft.Xna.Framework;

using EldenRingItems.Content.Items.Materials.SomberSmithingStones;

namespace EldenRingItems.Content.Items.Weapons.Melee
{
    public class SwordOfNightAndFlame : ModItem
    {
        public override string Texture => "EldenRingItems/Content/Items/Weapons/Melee/SwordOfNightAndFlame";
        public override LocalizedText DisplayName => base.DisplayName.WithFormatArgs("");
        public int BaseDamage { get; set; } = 32;
        bool FlameAttack = true;

        public override void SetDefaults()
        {
            Item.width = 92;
            Item.height = 92;
            Item.DamageType = DamageClass.Melee;
            Item.damage = BaseDamage;
            Item.knockBack = 4f;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.scale = 1.1f;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (!Main.rand.NextBool(2))
                return;
            if (Main.rand.NextBool(10))
                FlameAttack = !FlameAttack;
            if (FlameAttack)
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Torch, Scale: Main.rand.NextFloat(0.85f, 1.3f));
            else
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.IceTorch, Scale: Main.rand.NextFloat(0.7f, 1.1f));
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (FlameAttack)
            {
                if (!target.HasBuff(BuffID.OnFire))
                    target.AddBuff(BuffID.OnFire, 60 * 15);
            }
            else
            {
                if (!target.HasBuff(BuffID.Frostburn))
                    target.AddBuff(BuffID.Frostburn, 60 * 15); // 15 seconds
            }
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.HellstoneBar, 20);
            r.AddIngredient(ItemID.SoulofNight, 3);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }

    #region Upgrade
    public abstract class UpgradedSwordOfNightAndFlame : SwordOfNightAndFlame
    {
        public int UpgradeLevel { get; set; }
        public Recipe recipe { get; set; }
        public override LocalizedText Tooltip => ModContent.GetModItem(ModContent.ItemType<SwordOfNightAndFlame>()).Tooltip;
        public override LocalizedText DisplayName => ModContent.GetModItem(ModContent.ItemType<SwordOfNightAndFlame>()).DisplayName.WithFormatArgs($" +{UpgradeLevel}");

        protected UpgradedSwordOfNightAndFlame(int upgradeLevel)
        {
            UpgradeLevel = upgradeLevel;
        }

        public override void SetDefaults()
        {
            if (UpgradeLevel <= 4) // Pre-Hardmode
                BaseDamage += UpgradeLevel * 3;
            else
                BaseDamage += UpgradeLevel * 7;
            base.SetDefaults();
        }

        public override void AddRecipes()
        {
            recipe = CreateRecipe();
            recipe.AddIngredient(SSSUtils.GetSSSByLevel(UpgradeLevel));
            recipe.AddTile(SSSUtils.GetTileByLevel(UpgradeLevel));
        }
    }

    public class SwordOfNightAndFlame1 : UpgradedSwordOfNightAndFlame
    {
        public SwordOfNightAndFlame1() : base(1) { }

        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<SwordOfNightAndFlame>());
            recipe.Register();
        }
    }

    public class SwordOfNightAndFlame2 : UpgradedSwordOfNightAndFlame
    {
        public SwordOfNightAndFlame2() : base(2) { }

        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<SwordOfNightAndFlame1>());
            recipe.Register();
        }
    }

    public class SwordOfNightAndFlame3 : UpgradedSwordOfNightAndFlame
    {
        public SwordOfNightAndFlame3() : base(3) { }

        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<SwordOfNightAndFlame2>());
            recipe.Register();
        }
    }

    public class SwordOfNightAndFlame4 : UpgradedSwordOfNightAndFlame
    {
        public SwordOfNightAndFlame4() : base(4) { }

        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<SwordOfNightAndFlame3>());
            recipe.Register();
        }
    }

    public class SwordOfNightAndFlame5 : UpgradedSwordOfNightAndFlame
    {
        public SwordOfNightAndFlame5() : base(5) { }

        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<SwordOfNightAndFlame4>());
            recipe.Register();
        }
    }

    public class SwordOfNightAndFlame6 : UpgradedSwordOfNightAndFlame
    {
        public SwordOfNightAndFlame6() : base(6) { }

        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<SwordOfNightAndFlame5>());
            recipe.Register();
        }
    }

    public class SwordOfNightAndFlame7 : UpgradedSwordOfNightAndFlame
    {
        public SwordOfNightAndFlame7() : base(7) { }

        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<SwordOfNightAndFlame6>());
            recipe.Register();
        }
    }

    public class SwordOfNightAndFlame8 : UpgradedSwordOfNightAndFlame
    {
        public SwordOfNightAndFlame8() : base(8) { }

        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<SwordOfNightAndFlame7>());
            recipe.Register();
        }
    }

    public class SwordOfNightAndFlame9 : UpgradedSwordOfNightAndFlame
    {
        public SwordOfNightAndFlame9() : base(9) { }

        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<SwordOfNightAndFlame8>());
            recipe.Register();
        }
    }

    public class SwordOfNightAndFlame10 : UpgradedSwordOfNightAndFlame
    {
        public SwordOfNightAndFlame10() : base(10) { }

        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<SwordOfNightAndFlame9>());
            recipe.Register();
        }
    }
    #endregion
}