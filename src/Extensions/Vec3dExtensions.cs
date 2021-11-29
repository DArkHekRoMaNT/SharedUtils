using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace SharedUtils.Extensions
{
    public static class Vec3dExtensions
    {
        public static Vec3d RelativePos(this Vec3d pos, ICoreAPI api)
        {
            pos.X -= api.World.DefaultSpawnPosition.XYZ.X;
            pos.Z -= api.World.DefaultSpawnPosition.XYZ.Z;

            return pos;
        }
    }
}