using Content.Server.Movement.Components;
using Content.Shared._CE14.Eye;

namespace Content.Server._CE14.Eye;

public sealed class CE14ToggleableEyeOffsetSystem : EntitySystem
{
    public override void Initialize()
    {
        SubscribeLocalEvent<EyeComponent, CE14EyeOffsetToggleActionEvent>(OnToggleEyeOffset);
    }

    private void OnToggleEyeOffset(Entity<EyeComponent> ent, ref CE14EyeOffsetToggleActionEvent args)
    {
        if (!HasComp<EyeCursorOffsetComponent>(ent))
            AddComp<EyeCursorOffsetComponent>(ent);
        else
            RemComp<EyeCursorOffsetComponent>(ent);
    }
}
