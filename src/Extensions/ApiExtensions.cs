using System;
using System.IO;
using Vintagestory.API.Common;

namespace SharedUtils.Extensions
{
    public static class ApiExtensions
    {
        public static string GetWorldId(this ICoreAPI api) => api?.World?.SavegameIdentifier;

        public static T LoadOrCreateConfig<T>(this ICoreAPI api, string file, T defaultConfig = default(T)) where T : new()
        {
            try
            {
                T loadedConfig = api.LoadModConfig<T>(file);
                if (loadedConfig != null)
                {
                    api.StoreModConfig<T>(loadedConfig, file);
                    return loadedConfig;
                }
            }
            catch (Exception e)
            {
                api.World.Logger.ModError($"Failed loading file ({file}), error {e}. Will initialize new one");
            }

            var newConfig = defaultConfig?.Equals(default(T)) == true ? defaultConfig : new T();
            api.StoreModConfig<T>(newConfig, file);
            return newConfig;
        }

        public static T LoadDataFile<T>(this ICoreAPI api, string file)
        {
            try
            {
                if (File.Exists(file))
                {
                    var content = File.ReadAllText(file);
                    return JsonUtil.FromString<T>(content);
                }
            }
            catch (Exception e)
            {
                api.World.Logger.ModError($"Failed loading file ({file}), error {e}");
            }

            return default(T);
        }

        public static T LoadOrCreateDataFile<T>(this ICoreAPI api, string file) where T : new()
        {
            var data = api.LoadDataFile<T>(file);
            if (data.Equals(default(T))) return data;

            api.World.Logger.ModNotification($"Will initialize new one");

            var newData = new T();
            SaveDataFile(api, file, newData);
            return newData;
        }

        public static void SaveDataFile<T>(this ICoreAPI api, string file, T data)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(file));
                var content = JsonUtil.ToString(data);
                File.WriteAllText(file, content);
            }
            catch (Exception e)
            {
                api.World.Logger.ModError($"Failed saving file ({file}), error {e}");
            }
        }
    }
}