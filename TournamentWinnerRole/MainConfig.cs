using System.Collections.Generic;

namespace TournamentWinnerRole;

public class MainConfig
{
    public string RoleName { get; set; } = "Tournament Winner";
    public string RoleColor { get; set; } = "cyan";

    public HashSet<string> RoleWinners { get; set; } = new ()
    {
        "76561199113845460@steam",
        "76561199182962020@steam",
        "76561198255568514@steam",
    };
}