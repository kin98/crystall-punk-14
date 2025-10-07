using Content.Shared.Weather;
using Robust.Shared.Prototypes;

namespace Content.Server._CE14.GameTicking.Rules.Components;

[RegisterComponent]
public sealed partial class CE14WeatherRuleComponent : Component
{
    [DataField(required: true)]
    public ProtoId<WeatherPrototype> Weather = default!;
}
