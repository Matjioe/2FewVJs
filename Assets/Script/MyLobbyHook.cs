using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityStandardAssets.Network;

public class MyLobbyHook : LobbyHook
{
	public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
	{
		gamePlayer.GetComponent<PlayerScript>().RpcChangeColor(lobbyPlayer.GetComponent<LobbyPlayer>().playerColor);
	}
}
