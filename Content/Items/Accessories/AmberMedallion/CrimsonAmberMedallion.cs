using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

using EldenRingItems.Content.Items.Materials.SomberSmithingStones;

namespace EldenRingItems.Content.Items.Accessories.AmberMedallion
{
    public class CrimsonAmberMedallion : ModItem
    {
        public float LifeBonusMultiplier { get; set; } = 0.05f;

        public override LocalizedText DisplayName => base.DisplayName.WithFormatArgs("");
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs((int)(LifeBonusMultiplier * 100));

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 46;
            Item.value = Item.buyPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += (int)(player.statLifeMax * LifeBonusMultiplier);
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (equippedItem.ModItem is CrimsonAmberMedallion && incomingItem.ModItem is CrimsonAmberMedallion)
                return false;
            return true;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            for (int i = 3; i < 9; i++)
                if (player.armor[i].ModItem is CrimsonAmberMedallion)
                    return false;
            return true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.GoldBar, 8);
            r.AddIngredient(ItemID.Chain);
            r.AddIngredient(ItemID.Ruby, 4);
            r.AddIngredient(ItemID.LifeCrystal);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }

    #region Upgrade
    public abstract class UpgradedCrimsonAmberMedallion : CrimsonAmberMedallion
    {
        public int UpgradeLevel { get; set; }
        public override LocalizedText Tooltip => ModContent.GetModItem(ModContent.ItemType<CrimsonAmberMedallion>()).Tooltip.WithFormatArgs((int)(LifeBonusMultiplier * 100));
        public override LocalizedText DisplayName => ModContent.GetModItem(ModContent.ItemType<CrimsonAmberMedallion>()).DisplayName.WithFormatArgs($" +{UpgradeLevel}");

        protected UpgradedCrimsonAmberMedallion(int upgradeLevel)
        {
            UpgradeLevel = upgradeLevel;
            LifeBonusMultiplier += UpgradeLevel * 0.05f;
        }
    }

    public class CrimsonAmberMedallion1 : UpgradedCrimsonAmberMedallion
    {
        public CrimsonAmberMedallion1() : base(1) { }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Orange;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<CrimsonAmberMedallion>());
            r.AddIngredient(SSSUtils.GetSSSByLevel(4));
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }

    public class CrimsonAmberMedallion2 : UpgradedCrimsonAmberMedallion
    {
        public CrimsonAmberMedallion2() : base(2) { }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.LightRed;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<CrimsonAmberMedallion1>());
            r.AddIngredient(SSSUtils.GetSSSByLevel(6));
            r.AddTile(SSSUtils.GetTileByLevel(6));
            r.Register();
        }
    }

    public class CrimsonAmberMedallion3 : UpgradedCrimsonAmberMedallion
    {
        public CrimsonAmberMedallion3() : base(3) { }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Pink;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<CrimsonAmberMedallion2>());
            r.AddIngredient(SSSUtils.GetSSSByLevel(8));
            r.AddTile(SSSUtils.GetTileByLevel(8));
            r.Register();
        }
    }
    #endregion
}
