using Robust.Shared.Prototypes;

namespace Content.Server._CE14.MagicSpellStorage.Components;

/// <summary>
/// A component that allows you to store spells in items
/// </summary>
[RegisterComponent, Access(typeof(CE14SpellStorageSystem))]
public sealed partial class CE14SpellStorageComponent : Component
{
    /// <summary>
    /// Set true when giving starting abilities to creatures in this way
    /// </summary>
    [DataField]
    public bool GrantAccessToSelf = false;

    /// <summary>
    /// list of spell prototypes used for initialization.
    /// </summary>
    [DataField]
    public List<EntProtoId> Spells = new();

    /// <summary>
    /// created after the initialization of spell entities.
    /// </summary>
    [DataField]
    public List<EntityUid> SpellEntities = new();
}
