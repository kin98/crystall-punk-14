namespace Content.Shared._CE14.Wallpaper;

/// <summary>
/// After a delay, it removes all wallpaper from the entity.
/// </summary>
[RegisterComponent, Access(typeof(CE14SharedWallpaperSystem))]
public sealed partial class CE14WallpaperRemoverComponent : Component
{
    [DataField]
    public float Delay = 1f;
}
