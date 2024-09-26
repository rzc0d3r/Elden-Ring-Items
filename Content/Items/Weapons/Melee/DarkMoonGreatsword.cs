using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using EldenRingItems.Projectiles.Melee.DarkMoonGreatsword;
using EldenRingItems.Content.Items.Materials.SomberSmithingStones;

namespace EldenRingItems.Content.Items.Weapons.Melee
{
    public class DarkMoonGreatsword0 : ModItem
    {
        public override string Texture => "EldenRingItems/Content/Items/Weapons/Melee/DarkMoonGreatsword";
        public int BaseDamage { get; set; } = 85;

        SoundStyle ChargedUseSound = new SoundStyle("EldenRingItems/Sounds/cs_c2010.2318");

        public bool Charging { get; set; } = false;
        public bool IsCharged { get; set; } = false;
        const int MAX_CHARGED_ATTACKS = 10;
        int MadeChargedAttacks = 0;
        const int MANA_FOR_CHARGE = 100;

        public override LocalizedText DisplayName => base.DisplayName.WithFormatArgs("");
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MANA_FOR_CHARGE, MAX_CHARGED_ATTACKS);

        public override void SetDefaults()
        {
            Item.width = 92;
            Item.height = 91;
            Item.DamageType = DamageClass.Melee;
            Item.damage = BaseDamage;
            Item.knockBack = 5f;
            Item.useTime = 27;
            Item.useAnimation = 27;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 35, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.shoot = ModContent.ProjectileType<DarkMoonGreatswordSlash>();
            Item.shootSpeed = 12f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (IsCharged && player.altFunctionUse != 2)
            {
                if (MadeChargedAttacks == MAX_CHARGED_ATTACKS - 1)
                {
                    Reset();
                    IsCharged = false;
                }
                else
                    MadeChargedAttacks++;
                return true;
            }
            return false;
        }

        public override bool AltFunctionUse(Player player) => true;

        void Reset(string addtext="")
        {
            Charging = false;
            Item.UseSound = SoundID.Item1;
            MadeChargedAttacks = 0;
        }

        public override void UpdateInventory(Player player)
        {
            if (player.HeldItem.type != Type && !IsCharged && Charging)
                Reset();
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            base.PostDrawInWorld(spriteBatch, lightColor, alphaColor, rotation, scale, whoAmI);
            if (!IsCharged && Charging && Main.LocalPlayer.HeldItem.type != Type)
                Reset();
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
            if (!IsCharged && Charging && Main.LocalPlayer.HeldItem.type != Type)
                Reset();
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && !IsCharged)
            {
                if (!Charging)
                {
                    if (player.CheckMana(MANA_FOR_CHARGE, true))
                    {
                        Charging = true;
                        if (Main.myPlayer == player.whoAmI)
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, 0, -30, ModContent.ProjectileType<DarkMoonGreatswordCharging>(), 0, 0, player.whoAmI, 0f);
                        Item.UseSound = ChargedUseSound;
                        MadeChargedAttacks = 0;
                    }
                }
            }
            else if (!Charging && player.altFunctionUse != 2)
                return true;
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!target.HasBuff(BuffID.Frostburn))
                target.AddBuff(BuffID.Frostburn, 60 * 15); // 15 seconds
        }
    }

    #region Upgrade
    public abstract class UpgradedDarkMoonGreatsword : DarkMoonGreatsword0
    {
        public int UpgradeLevel { get; set; }
        public Recipe recipe { get; set; }
        public override LocalizedText Tooltip => ModContent.GetModItem(ModContent.ItemType<DarkMoonGreatsword0>()).Tooltip;
        public override LocalizedText DisplayName => ModContent.GetModItem(ModContent.ItemType<DarkMoonGreatsword0>()).DisplayName.WithFormatArgs($" +{UpgradeLevel}");

        protected UpgradedDarkMoonGreatsword(int upgradeLevel)
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

    public class DarkMoonGreatsword1 : UpgradedDarkMoonGreatsword
    {
        public DarkMoonGreatsword1() : base(1) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<DarkMoonGreatsword0>());
            recipe.Register();
        }
    }

    public class DarkMoonGreatsword2 : UpgradedDarkMoonGreatsword
    {
        public DarkMoonGreatsword2() : base(2) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<DarkMoonGreatsword1>());
            recipe.Register();
        }
    }

    public class DarkMoonGreatsword3 : UpgradedDarkMoonGreatsword
    {
        public DarkMoonGreatsword3() : base(3) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<DarkMoonGreatsword2>());
            recipe.Register();
        }
    }
    public class DarkMoonGreatsword4 : UpgradedDarkMoonGreatsword
    {
        public DarkMoonGreatsword4() : base(4) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<DarkMoonGreatsword3>());
            recipe.Register();
        }
    }

    public class DarkMoonGreatsword5 : UpgradedDarkMoonGreatsword
    {
        public DarkMoonGreatsword5() : base(5) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<DarkMoonGreatsword4>());
            recipe.Register();
        }
    }
 
    public class DarkMoonGreatsword6 : UpgradedDarkMoonGreatsword
    {
        public DarkMoonGreatsword6() : base(6) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<DarkMoonGreatsword5>());
            recipe.Register();
        }
    }

    public class DarkMoonGreatsword7 : UpgradedDarkMoonGreatsword
    {
        public DarkMoonGreatsword7() : base(7) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<DarkMoonGreatsword6>());
            recipe.Register();
        }
    }

    public class DarkMoonGreatsword8 : UpgradedDarkMoonGreatsword
    {
        public DarkMoonGreatsword8() : base(8) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<DarkMoonGreatsword7>());
            recipe.Register();
        }
    }

    public class DarkMoonGreatsword9 : UpgradedDarkMoonGreatsword
    {
        public DarkMoonGreatsword9() : base(9) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<DarkMoonGreatsword8>());
            recipe.Register();
        }
    }

    public class DarkMoonGreatsword10 : UpgradedDarkMoonGreatsword
    {
        public DarkMoonGreatsword10() : base(10) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<DarkMoonGreatsword9>());
            recipe.Register();
        }
    }
    #endregion
}
