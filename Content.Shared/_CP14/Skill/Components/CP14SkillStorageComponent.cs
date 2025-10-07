using Content.Shared._CE14.Skill.Prototypes;
using Content.Shared.FixedPoint;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._CE14.Skill.Components;

/// <summary>
/// Component that stores the skills learned by a player and their progress in the skill trees.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(true, fieldDeltas: true)]
[Access(typeof(CE14SharedSkillSystem))]
public sealed partial class CE14SkillStorageComponent : Component
{
    /// <summary>
    /// Skill trees displayed in the skill tree interface. Only skills from these trees can be learned by this player.
    /// </summary>
    [DataField, AutoNetworkedField]
    public HashSet<ProtoId<CE14SkillTreePrototype>> AvailableSkillTrees = new();

    /// <summary>
    /// Tracks skills that are learned without spending memory points.
    /// the skills that are here are DOUBLED in the LearnedSkills,
    /// </summary>
    [DataField, AutoNetworkedField]
    public List<ProtoId<CE14SkillPrototype>> FreeLearnedSkills = new();

    [DataField, AutoNetworkedField]
    public List<ProtoId<CE14SkillPrototype>> LearnedSkills = new();

    [DataField, AutoNetworkedField]
    public Dictionary<ProtoId<CE14SkillPointPrototype>, CE14SkillPointContainerEntry> SkillPoints = new();
}

[DataDefinition, Serializable, NetSerializable]
public sealed partial class CE14SkillPointContainerEntry
{
    /// <summary>
    /// The number of experience points spent on skills.
    /// </summary>
    [DataField]
    public FixedPoint2 Sum = 0;

    /// <summary>
    /// The maximum of experience points that can be spent on learning skills.
    /// </summary>
    [DataField]
    public FixedPoint2 Max = 0;
}

/// <summary>
/// Raised when a player attempts to learn a skill. This is sent from the client to the server.
/// </summary>
[Serializable, NetSerializable]
public sealed class CE14TryLearnSkillMessage(NetEntity entity, ProtoId<CE14SkillPrototype> skill) : EntityEventArgs
{
    public readonly NetEntity Entity = entity;
    public readonly ProtoId<CE14SkillPrototype> Skill = skill;
}
