using System.Collections.Generic;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;

namespace TournamentWinnerRole;

public class EntryPoint
{
    public const string Version = "1.0.0.0";

    [PluginConfig] public MainConfig PluginConfig;
    
    [PluginEntryPoint("TournamentWinnerRole", Version, "Gives a role to tournament winners", "Jesus-QC")]
    private void Init()
    {
        EventManager.RegisterEvents(this);
    }
    
    [PluginEvent(ServerEventType.PlayerJoined)]
    void OnPlayerJoin(Player player)
    {
        if (!PluginConfig.RoleWinners.Contains(player.UserId))
            return;

        PermissionsHandler perms = ServerStatic.GetPermissionsHandler();
        
        if (!perms._groups.ContainsKey("tournament_winner_role"))
        {
            perms._groups.Add("tournament_winner_role", new UserGroup
            {
                BadgeColor = PluginConfig.RoleColor,
                BadgeText = PluginConfig.RoleName,
                Cover = false,
                HiddenByDefault = false,
                KickPower = 0,
                Permissions = 0,
                RequiredKickPower = 0,
                Shared = false
            });
        }

        UserGroup group = perms.GetGroup("tournament_winner_role");

        player.ReferenceHub.serverRoles.SetGroup(group, true);
        SetOrAddElement(perms._members, player.UserId, "tournament_winner_role");
    }
    
    public static void SetOrAddElement<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        if (!dictionary.ContainsKey(key))
        {
            dictionary.Add(key, value);
            return;
        }

        dictionary[key] = value;
    }
}
