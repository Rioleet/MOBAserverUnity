using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class MyNetworkLobbyHook : LobbyHook
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        NetworkPlayerHelper hero = gamePlayer.GetComponent<NetworkPlayerHelper>();

        hero.name = lobby.name;
        hero.color = lobby.playerColor;
        hero.Group = System.Convert.ToInt32(lobby.playerGroup);
    }
}
