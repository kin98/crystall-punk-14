using Robust.Shared.Configuration;

namespace Content.Shared.CCVar;

public sealed partial class CCVars
{
    public static readonly CVarDef<bool> DiscordAuthEnabled =
        CVarDef.Create("CE14.discord_auth_enabled", false, CVar.SERVERONLY);

    public static readonly CVarDef<string> DiscordAuthUrl =
        CVarDef.Create("CE14.discord_auth_url", "http://localhost:8000/sponsors", CVar.SERVERONLY | CVar.CONFIDENTIAL);

    public static readonly CVarDef<string> DiscordAuthToken =
        CVarDef.Create("CE14.discord_auth_token", "token", CVar.SERVERONLY | CVar.CONFIDENTIAL);
}
