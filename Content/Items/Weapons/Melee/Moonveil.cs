using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;

using Microsoft.Xna.Framework;

using EldenRingItems.Projectiles.Melee;
using EldenRingItems.Content.Items.Materials.SomberSmithingStones;

namespace EldenRingItems.Content.Items.Weapons.Melee
{
    public class Moonveil : ModItem
    {
        public override string Texture => "EldenRingItems/Content/Items/Weapons/Melee/Moonveil";
        public int BaseDamage { get; set; } = 60;

        SoundStyle IsChargedSound = new SoundStyle("EldenRingItems/Sounds/cs_c2010.649");

        bool IsCharged = false;
        const int MAX_CHARGED_ATTACKS = 1;
        int MadeChargedAttacks = 0;
        const int MANA_FOR_CHARGE = 60;

        public override LocalizedText DisplayName => base.DisplayName.WithFormatArgs("");
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MANA_FOR_CHARGE);

        public override void SetDefaults()
        {
            Item.width = 75;
            Item.height = 75;
            Item.DamageType = DamageClass.Melee;
            Item.damage = BaseDamage;
            Item.knockBack = 5f;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.shoot = ModContent.ProjectileType<MoonveilSlash>();
            Item.shootSpeed = 12f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (IsCharged)
            {
                if (MadeChargedAttacks == MAX_CHARGED_ATTACKS - 1)
                {
                    Item.UseSound = SoundID.Item1;
                    MadeChargedAttacks = 0;
                    IsCharged = false;
                }
                else
                    MadeChargedAttacks++;
                return true;
            }
            return false;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (IsCharged)
                damage *= 3;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (Main.mouseRight && !IsCharged)
            {
                if (player.CheckMana(MANA_FOR_CHARGE, true))
                {
                    IsChargedSound.Volume = 0.45f;
                    SoundEngine.PlaySound(IsChargedSound);
                    IsCharged = true;
                }
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.BlueTorch, Scale: Main.rand.NextFloat(0.5f, 1.2f));
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!target.HasBuff(BuffID.Frostburn))
                target.AddBuff(BuffID.Frostburn, 60 * 15); // 15 seconds
        }
    }

    #region Upgrade
    public abstract class UpgradedMoonveil : Moonveil
    {
        public int UpgradeLevel { get; set; }
        public Recipe recipe { get; set; }
        public override LocalizedText Tooltip => ModContent.GetModItem(ModContent.ItemType<Moonveil>()).Tooltip;
        public override LocalizedText DisplayName => ModContent.GetModItem(ModContent.ItemType<Moonveil>()).DisplayName.WithFormatArgs($" +{UpgradeLevel}");

        protected UpgradedMoonveil(int upgradeLevel)
        {
            UpgradeLevel = upgradeLevel;
        }

        public override void SetDefaults()
        {
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

    public class Moonveil1 : UpgradedMoonveil
    {
        public Moonveil1() : base(1) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<Moonveil>());
            recipe.Register();
        }
    }

    public class Moonveil2 : UpgradedMoonveil
    {
        public Moonveil2() : base(2) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<Moonveil1>());
            recipe.Register();
        }
    }

    public class Moonveil3 : UpgradedMoonveil
    {
        public Moonveil3() : base(3) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<Moonveil2>());
            recipe.Register();
        }
    }
    public class Moonveil4 : UpgradedMoonveil
    {
        public Moonveil4() : base(4) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<Moonveil3>());
            recipe.Register();
        }
    }

    public class Moonveil5 : UpgradedMoonveil
    {
        public Moonveil5() : base(5) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<Moonveil4>());
            recipe.Register();
        }
    }

    public class Moonveil6 : UpgradedMoonveil
    {
        public Moonveil6() : base(6) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<Moonveil5>());
            recipe.Register();
        }
    }

    public class Moonveil7 : UpgradedMoonveil
    {
        public Moonveil7() : base(7) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<Moonveil6>());
            recipe.Register();
        }
    }

    public class Moonveil8 : UpgradedMoonveil
    {
        public Moonveil8() : base(8) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<Moonveil7>());
            recipe.Register();
        }
    }

    public class Moonveil9 : UpgradedMoonveil
    {
        public Moonveil9() : base(9) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<Moonveil8>());
            recipe.Register();
        }
    }

    public class Moonveil10 : UpgradedMoonveil
    {
        public Moonveil10() : base(10) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<Moonveil9>());
            recipe.Register();
        }
    }
    #endregion
}