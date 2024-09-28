using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

using EldenRingItems.Common.Players;
using EldenRingItems.Content.Items.Materials.SomberSmithingStones;

namespace EldenRingItems.Content.Items.Accessories.SeedTalisman
{
    public class CeruleanSeedTalisman : ModItem
    {
        public float ManaPotionMultiplier { get; set; } = 0.2f;

        public override LocalizedText DisplayName => base.DisplayName.WithFormatArgs("");
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs((int)(ManaPotionMultiplier*100));

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.value = Item.buyPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ERIPlayer eri_player = player.GetModPlayer<ERIPlayer>();
            eri_player.CeruleanSeedTalismanIsActive = true;
            eri_player.ManaPotionMultiplier += ManaPotionMultiplier;
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (equippedItem.ModItem is CeruleanSeedTalisman && incomingItem.ModItem is CeruleanSeedTalisman)
                return false;
            return true;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            for (int i=3; i < 9; i++)
                if (player.armor[i].ModItem is CeruleanSeedTalisman)
                    return false;
            return true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.GoldBar, 8);
            r.AddIngredient(ItemID.Chain);
            r.AddIngredient(ItemID.MoonglowSeeds, 5);
            r.AddIngredient(ItemID.ManaCrystal);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }

    #region Upgrade
    public abstract class UpgradedCeruleanSeedTalisman : CeruleanSeedTalisman
    {
        public int UpgradeLevel { get; set; }
        public override LocalizedText Tooltip => ModContent.GetModItem(ModContent.ItemType<CeruleanSeedTalisman>()).Tooltip.WithFormatArgs((int)(ManaPotionMultiplier*100));
        public override LocalizedText DisplayName => ModContent.GetModItem(ModContent.ItemType<CeruleanSeedTalisman>()).DisplayName.WithFormatArgs($" +{UpgradeLevel}");

        protected UpgradedCeruleanSeedTalisman(int upgradeLevel)
        {
            UpgradeLevel = upgradeLevel;
        }
    }

    public class CeruleanSeedTalisman1 : UpgradedCeruleanSeedTalisman
    {
        public CeruleanSeedTalisman1() : base(1) {
            ManaPotionMultiplier = 0.3f;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 58;
            Item.height = 62;
            Item.rare = ItemRarityID.Pink;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<CeruleanSeedTalisman>());
            r.AddIngredient(SSSUtils.GetSSSByLevel(8));
            r.AddTile(SSSUtils.GetTileByLevel(8));
            r.Register();
        }
    }
    #endregion
}