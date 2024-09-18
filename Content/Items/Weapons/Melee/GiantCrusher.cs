using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using EldenRingItems.Projectiles.Melee;
using Terraria.Localization;
using EldenRingItems.Content.Items.Materials.SomberSmithingStones;

namespace EldenRingItems.Content.Items.Weapons.Melee
{
    public class GiantCrusher : ModItem
    {
        public override string Texture => "EldenRingItems/Content/Items/Weapons/Melee/GiantCrusher";
        public override LocalizedText DisplayName => base.DisplayName.WithFormatArgs("");
        public int BaseDamage { get; set; } = 80;

        public override void SetDefaults()
        {
            Item.width = 120;
            Item.height = 120;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.damage = BaseDamage;
            Item.knockBack = 9f;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.shoot = ModContent.ProjectileType<GiantCrusherProj>();
            Item.shootSpeed = 17f;
        }
    }

    #region Upgrade
    public abstract class UpgradedGiantCrusher : GiantCrusher
    {
        public int UpgradeLevel { get; set; }
        public Recipe recipe { get; set; }
        public override LocalizedText Tooltip => ModContent.GetModItem(ModContent.ItemType<GiantCrusher>()).Tooltip;
        public override LocalizedText DisplayName => ModContent.GetModItem(ModContent.ItemType<GiantCrusher>()).DisplayName.WithFormatArgs($" +{UpgradeLevel}");

        protected UpgradedGiantCrusher(int upgradeLevel)
        {
            UpgradeLevel = upgradeLevel;
        }

        public override void SetDefaults()
        {
            BaseDamage += UpgradeLevel * 12;
            base.SetDefaults();
        }

        public override void AddRecipes()
        {
            recipe = CreateRecipe();
            recipe.AddIngredient(SSSUtils.GetSSSByLevel(UpgradeLevel));
            recipe.AddTile(SSSUtils.GetTileByLevel(UpgradeLevel));
        }
    }

    public class GiantCrusher1 : UpgradedGiantCrusher
    {
        public GiantCrusher1() : base(1) { }

        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<GiantCrusher>());
            recipe.Register();
        }
    }

    public class GiantCrusher2 : UpgradedGiantCrusher
    {
        public GiantCrusher2() : base(2) { }

        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<GiantCrusher1>());
            recipe.Register();
        }
    }

    public class GiantCrusher3 : UpgradedGiantCrusher
    {
        public GiantCrusher3() : base(3) { }

        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<GiantCrusher2>());
            recipe.Register();
        }
    }

    public class GiantCrusher4 : UpgradedGiantCrusher
    {
        public GiantCrusher4() : base(4) { }

        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<GiantCrusher3>());
            recipe.Register();
        }
    }

    public class GiantCrusher5 : UpgradedGiantCrusher
    {
        public GiantCrusher5() : base(5) { }

        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<GiantCrusher4>());
            recipe.Register();
        }
    }

    public class GiantCrusher6 : UpgradedGiantCrusher
    {
        public GiantCrusher6() : base(6) { }

        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<GiantCrusher5>());
            recipe.Register();
        }
    }

    public class GiantCrusher7 : UpgradedGiantCrusher
    {
        public GiantCrusher7() : base(7) { }

        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<GiantCrusher6>());
            recipe.Register();
        }
    }

    public class GiantCrusher8 : UpgradedGiantCrusher
    {
        public GiantCrusher8() : base(8) { }

        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<GiantCrusher7>());
            recipe.Register();
        }
    }

    public class GiantCrusher9 : UpgradedGiantCrusher
    {
        public GiantCrusher9() : base(9) { }

        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<GiantCrusher8>());
            recipe.Register();
        }
    }

    public class GiantCrusher10 : UpgradedGiantCrusher
    {
        public GiantCrusher10() : base(10) { }

        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<GiantCrusher9>());
            recipe.Register();
        }
    }
    #endregion
}