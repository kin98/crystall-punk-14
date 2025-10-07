using System.Text;
using Content.Shared.Stacks;
using Robust.Shared.Prototypes;
namespace Content.Shared._CE14.Currency;

public partial class CE14SharedCurrencySystem : EntitySystem
{
    public static readonly KeyValuePair<EntProtoId, int> CP = new("CE14CopperCoin1", 1);
    public static readonly KeyValuePair<EntProtoId, int> SP = new("CE14SilverCoin1", 10);
    public static readonly KeyValuePair<EntProtoId, int> GP = new("CE14GoldCoin1", 100);
    public static readonly KeyValuePair<EntProtoId, int> PP = new("CE14PlatinumCoin1", 1000);

    public string GetCurrencyPrettyString(int currency)
    {
        var total = currency;

        var sb = new StringBuilder();

        var gp = total / 100;
        total %= 100;

        var sp = total / 10;
        total %= 10;

        var cp = total;

        if (gp > 0)
            sb.Append(" " + Loc.GetString("CE14-currency-examine-gp", ("coin", gp)));
        if (sp > 0)
            sb.Append(" " + Loc.GetString("CE14-currency-examine-sp", ("coin", sp)));
        if (cp > 0)
            sb.Append(" " + Loc.GetString("CE14-currency-examine-cp", ("coin", cp)));
        if (gp <= 0 && sp <= 0 && cp <= 0)
            sb.Append(" " + Loc.GetString("CE14-trading-empty-price"));
            return sb.ToString();

        return sb.ToString();
    }
}
