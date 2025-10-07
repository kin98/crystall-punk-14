using System.Numerics;
using Content.Shared._CE14.Procedural.Prototypes;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._CE14.Demiplane;

[Serializable, NetSerializable]
public enum CE14DemiplaneMapUiKey
{
    Key,
}

[Serializable, NetSerializable]
public sealed class CE14DemiplaneMapUiState(Dictionary<Vector2i, CE14DemiplaneMapNode> nodes, HashSet<(Vector2i, Vector2i)>? edges = null) : BoundUserInterfaceState
{
    public Dictionary<Vector2i, CE14DemiplaneMapNode> Nodes = nodes;
    public HashSet<(Vector2i, Vector2i)> Edges = edges ?? new();
}

[Serializable, NetSerializable]
public sealed class CE14DemiplaneMapNode(Vector2 uiPosition, ProtoId<CE14ProceduralLocationPrototype>? locationConfig = null, List<ProtoId<CE14ProceduralModifierPrototype>>? modifiers = null)
{
    public bool Start = false;
    public Vector2 UiPosition = uiPosition;
    public int Level = 0;

    public bool Opened = false;

    public ProtoId<CE14ProceduralLocationPrototype>? LocationConfig = locationConfig;
    public List<ProtoId<CE14ProceduralModifierPrototype>> Modifiers = modifiers ?? [];
}
