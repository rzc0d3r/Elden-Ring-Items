using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

using EldenRingItems.Common.Players;
using EldenRingItems.Content.Items.Materials.SomberSmithingStones;

namespace EldenRingItems.Content.Items.Accessories.SeedTalisman
{
    public class CrimsonCeruleanSeedTalisman: ModItem
    {
        public float ManaPotionMultiplier { get; set; } = 0.3f;
        public float HealingPotionMultiplier { get; set; } = 0.3f;

        public override LocalizedText DisplayName => base.DisplayName.WithFormatArgs("");
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs((int)(HealingPotionMultiplier*100), (int)(ManaPotionMultiplier*100));

        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 62;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ERIPlayer eri_player = player.GetModPlayer<ERIPlayer>();
            eri_player.CeruleanSeedTalismanIsActive = true;
            eri_player.CrimsonSeedTalismanIsActive = true;
            eri_player.HealingPotionMultiplier += HealingPotionMultiplier;
            eri_player.ManaPotionMultiplier += ManaPotionMultiplier;
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (equippedItem.ModItem is CrimsonCeruleanSeedTalisman && incomingItem.ModItem is CrimsonCeruleanSeedTalisman)
                return false;
            else if (equippedItem.ModItem is CrimsonSeedTalisman && incomingItem.ModItem is CrimsonCeruleanSeedTalisman)
                return false;
            else if (equippedItem.ModItem is CeruleanSeedTalisman && incomingItem.ModItem is CrimsonCeruleanSeedTalisman)
                return false;
            return true;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            for (int i=3; i < 9; i++)
                if (player.armor[i].ModItem is CrimsonCeruleanSeedTalisman || player.armor[i].ModItem is CrimsonSeedTalisman || player.armor[i].ModItem is CeruleanSeedTalisman)
                    return false;
            return true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<CrimsonSeedTalisman1>());
            r.AddIngredient(ModContent.ItemType<CeruleanSeedTalisman1>());
            r.AddIngredient(SSSUtils.GetSSSByLevel(10));
            r.AddTile(TileID.MythrilAnvil);
            r.Register();
        }
    }
}