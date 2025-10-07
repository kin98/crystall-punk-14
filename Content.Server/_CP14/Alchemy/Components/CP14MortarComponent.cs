
namespace Content.Server._CE14.Alchemy;

[RegisterComponent, Access(typeof(CE14AlchemyExtractionSystem))]
public sealed partial class CE14MortarComponent : Component
{
    [DataField(required: true)]
    public string Solution = string.Empty;

    [DataField(required: true)]
    public string ContainerId = string.Empty;
}

