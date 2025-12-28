namespace Terraria.Audio
{
    public class NPCAudioTracker
    {
        private int _expectedType;
        private int _expectedIndex;

        public NPCAudioTracker(NPC npc)
        {
            _expectedIndex = npc.whoAmI;
            _expectedType = npc.type;
        }

        public bool IsActiveAndInGame()
        {
            if (Main.gameMenu)
                return false;

            NPC npc = Main.npc[_expectedIndex];
            if (npc.active)
                return npc.type == _expectedType;

            return false;
        }
    }
}
