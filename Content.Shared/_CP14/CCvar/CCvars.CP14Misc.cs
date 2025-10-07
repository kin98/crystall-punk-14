using Robust.Shared.Configuration;

namespace Content.Shared.CCVar;

public sealed partial class CCVars
{
    public static readonly CVarDef<int> CE14RoundEndMinutes =
        CVarDef.Create("CE14.round_end_minutes", 15, CVar.SERVERONLY);

    /// <summary>
    /// Automatically shuts down the server outside of the CBT plytime. Shitcoded enough, but it's temporary anyway
    /// </summary>
    public static readonly CVarDef<bool> CE14ClosedBetaTest =
        CVarDef.Create("CE14.closet_beta_test", false, CVar.SERVERONLY);

    /// <summary>
    ///     Should powerful spells be restricted from being learned until a certain time has elapsed?
    /// </summary>
    public static readonly CVarDef<bool>
        CE14SkillTimers = CVarDef.Create("CE14.skill_timers", true, CVar.SERVER | CVar.REPLICATED);
}
