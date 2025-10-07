using Content.Server._CE14.ZLevels.Commands;
using Content.Server._CE14.ZLevels.EntitySystems;
using Robust.Shared.Map;
using Robust.Shared.Utility;

namespace Content.Server._CE14.ZLevels.Components;

/// <summary>
/// Initializes the z-level system by creating a series of linked maps
/// </summary>
[RegisterComponent, Access(typeof(CE14StationZLevelsSystem), typeof(CE14CombineMapsIntoZLevelsCommand))]
public sealed partial class CE14StationZLevelsComponent : Component
{
    [DataField(required: true)]
    public int DefaultMapLevel = 0;

    [DataField(required: true)]
    public Dictionary<int, CE14ZLevelEntry> Levels = new();

    public bool Initialized = false;

    public Dictionary<MapId, int> LevelEntities = new();
}

[DataRecord, Serializable]
public sealed class CE14ZLevelEntry
{
    public ResPath? Path { get; set; } = null;
}
