using Vintagestory.API.Common;

namespace SharedUtils
{
    public class ConstantsCore : ModSystem
    {
        public override double ExecuteOrder() => 0;
        public override void StartPre(ICoreAPI api) => ModId = Mod.Info.ModID;

        public static string ModId { get; private set; }
        public static string ModPrefix => $"[{ModId}] ";
    }
}