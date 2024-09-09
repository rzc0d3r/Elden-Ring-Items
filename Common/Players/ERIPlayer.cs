using Terraria.ModLoader;

namespace EldenRingItems.Common.Players
{
    public class ERIPlayer : ModPlayer
    {
        public bool Blessed;

        public override void ResetEffects()
        {
        }

        public override void OnRespawn()
        {
            Blessed = false;
        }
    }
}