
namespace Content.Server._CE14.Demiplane.Components;

/// <summary>
///
/// </summary>
[RegisterComponent, Access(typeof(CE14DemiplaneSystem))]
public sealed partial class CE14DemiplaneMapComponent : Component
{
    [DataField]
    public Vector2i Position = Vector2i.Zero;
}
