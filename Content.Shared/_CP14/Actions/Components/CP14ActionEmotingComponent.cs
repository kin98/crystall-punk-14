namespace Content.Shared._CE14.Actions.Components;

[RegisterComponent]
public sealed partial class CE14ActionEmotingComponent : Component
{
    [DataField]
    public string StartEmote = string.Empty; //Not LocId!

    [DataField]
    public string EndEmote = string.Empty; //Not LocId!
}
