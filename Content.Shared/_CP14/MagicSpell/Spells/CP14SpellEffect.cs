using JetBrains.Annotations;
using Robust.Shared.Map;

namespace Content.Shared._CE14.MagicSpell.Spells;

[ImplicitDataDefinitionForInheritors]
[MeansImplicitUse]
public abstract partial class CE14SpellEffect
{
    public abstract void Effect(EntityManager entManager, CE14SpellEffectBaseArgs args);
}

public record CE14SpellEffectBaseArgs
{
    public EntityUid? User;
    public EntityUid? Used;
    public EntityUid? Target;
    public EntityCoordinates? Position;

    public CE14SpellEffectBaseArgs(EntityUid? user, EntityUid? used, EntityUid? target, EntityCoordinates? position)
    {
        User = user;
        Used = used;
        Target = target;
        Position = position;
    }
}
