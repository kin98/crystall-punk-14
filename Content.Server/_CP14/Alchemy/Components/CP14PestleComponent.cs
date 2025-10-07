using Robust.Shared.Audio;

namespace Content.Server._CE14.Alchemy;

[RegisterComponent, Access(typeof(CE14AlchemyExtractionSystem))]
public sealed partial class CE14PestleComponent : Component
{
    [DataField]
    public float Probability = 0.1f;

    [DataField]
    public SoundSpecifier HitSound = new SoundCollectionSpecifier("CE14Pestle", AudioParams.Default.WithVariation(0.2f));
}
