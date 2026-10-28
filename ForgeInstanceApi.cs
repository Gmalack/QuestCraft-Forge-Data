using UnityEngine;

/// Thin wrapper over pojlib.API.createNewInstance for Forge instances.
/// Signature in your Pojlib-release-with-ForgeWrapper.aar (read from the compiled class):
///   createNewInstance(Activity activity, MinecraftInstances instances, String instanceName,
///                     boolean useDefaultMods, String minecraftVersion, String modLoader, String imageURL)
/// ForgeMeta.Flavor.of() uses String.equals -> "Forge" / "NeoForge" are CASE-SENSITIVE.
public static class ForgeInstanceApi
{
    public const string LoaderForge = "Forge";       // exact casing, no lowercasing/trimming tricks
    public const string DefaultMcVersion = "1.20.1";

    public static AndroidJavaObject CreateForgeInstance(
        AndroidJavaObject activity,
        AndroidJavaObject instances,   // result of pojlib.API.loadAll()
        string instanceName,
        string minecraftVersion = DefaultMcVersion,
        string imageUrl = "",          // never pass null: Unity can't infer the Java type for a bare null
        bool useDefaultMods = true)    // true = pull the list from mods-forge.json at MODS_BASE_URL
    {
        using (var api = new AndroidJavaClass("pojlib.API"))
        {
            // 7-arg overload (bool in 4th slot) - the 6-arg overload is the .mrpack one, don't use it here.
            return api.CallStatic<AndroidJavaObject>(
                "createNewInstance",
                activity, instances, instanceName,
                useDefaultMods, minecraftVersion, LoaderForge, imageUrl);
        }
    }
}
