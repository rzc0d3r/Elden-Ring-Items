using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.Audio;
using EldenRingItems.Content.Buffs.StatBuff;

namespace EldenRingItems.Content.Items.Consumables
{
    internal class BlackFlameSpell : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.consumable = true;
            Item.UseSound = new SoundStyle("EldenRingItems/Sounds/UsingSpell");
            Item.useTime = 130;
            Item.useAnimation = 130;
            Item.mana = 160;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.maxStack = 1;
            Item.buffType = ModContent.BuffType<WeaponImbueBlackFlame>();
            Item.buffTime = 60 * 60 * 8; // 8m
        }

        public override bool CanUseItem(Player player)
        { 
            return !player.HasBuff(ModContent.BuffType<WeaponImbueBlackFlame>());
        }

        public override bool? UseItem(Player player)
        {
            if(player.HasBuff(ModContent.BuffType<WeaponImbueBlackFlame>()))
                return false;
            return true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.SpellTome);
            r.AddIngredient(ItemID.SoulofNight, 25);
            r.AddTile(TileID.Bookcases);
            r.Register();
        }
    }
}
