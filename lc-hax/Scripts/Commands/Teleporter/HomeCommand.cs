using System;
using GameNetcodeStuff;

namespace Hax;

public class HomeCommand : ITeleporter, ICommand {
    Action TeleportPlayerToBaseLater(PlayerControllerB targetPlayer) => () => {
        HaxObjects.Instance?.ShipTeleporters.Renew();

        if (!this.TryGetTeleporter(out ShipTeleporter teleporter)) {
            Console.Print("ShipTeleporter not found!");
            return;
        }

        Helper.SwitchRadarTarget(targetPlayer);
        Helper.CreateComponent<WaitForBehaviour>()
              .SetPredicate(() => Helper.IsRadarTarget(targetPlayer.playerClientId))
              .Init(teleporter.PressTeleportButtonServerRpc);
    };

    public void Execute(string[] args) {
        if (args.Length is 0) {
            Helper.StartOfRound?.ForcePlayerIntoShip();
            return;
        }

        if (!Helper.GetPlayer(args[0]).IsNotNull(out PlayerControllerB targetPlayer)) {
            Console.Print("Player not found!");
            return;
        }

        this.PrepareToTeleport(this.TeleportPlayerToBaseLater(targetPlayer));
    }
}
