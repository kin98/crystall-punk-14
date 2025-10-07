using Content.Shared._CE14.Skill.Prototypes;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Skill.Components;

/// <summary>
/// Component that stores the skills learned by a player and their progress in the skill trees.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(true)]
[Access(typeof(CE14SharedSkillSystem))]
public sealed partial class CE14MeleeWeaponSkillRequiredComponent : Component
{
    /// <summary>
    /// What skills does a character have to have to use this weapon?
    /// </summary>
    [DataField, AutoNetworkedField]
    public HashSet<ProtoId<CE14SkillPrototype>> Skills = new();

    /// <summary>
    /// The chances of dropping a weapon from your hands if the required skills are not learned by the character.
    /// </summary>
    [DataField, AutoNetworkedField]
    public float DropProbability = 0.5f;

    /// <summary>
    /// Reduces outgoing damage if the required skills are not learned by the character
    /// </summary>
    [DataField, AutoNetworkedField]
    public float DamageMultiplier = 0.5f;
}
