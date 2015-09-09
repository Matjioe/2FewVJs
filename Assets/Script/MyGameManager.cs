using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class MyGameManager : NetworkBehaviour {

	static public MyGameManager instance { get { return _instance; } }
	static protected MyGameManager _instance;

	public List<PlayerScript> players = new List<PlayerScript>();
	public GameObject serverUI;
	public GameObject activityMessage;
	public ColorPalette colorPalette;

	public void Awake()
	{
		_instance = this;
	}

	public override void OnStartServer ()
	{
		serverUI.SetActive(true);
		GetComponent<Animator>().enabled = true;
	}

	public void RegisterPlayer(PlayerScript player)   { players.Add(player);    }
	public void UnregisterPlayer(PlayerScript player) { players.Remove(player); }

	public void ChangeAllPlayersColor(Color color)
	{
		if (!isServer)
			return;

		foreach (PlayerScript player in players)
		{
			player.RpcChangeColor(color);
		}
	}

	public void ChangeAllPlayersShape(PlayerScript.BodyType bodyType)
	{
		if (!isServer)
			return;
		
		foreach (PlayerScript player in players)
		{
			player.RpcChangeBody(bodyType);
		}
	}

	public void ChangeAllPlayersColor()
	{
		Color color = colorPalette.GetRandomColor();
		ChangeAllPlayersColor(color);
	}

	public void ChangeRandomPlayerColor()
	{
		ChangeRandomPlayerColor(colorPalette.GetRandomColor());
	}

	public void ChangeRandomPlayerColor(Color color)
	{
		players[Random.Range(0, players.Count)].RpcChangeColor(color);
	}

	public void SetActivityMessage(string activityMsg)
	{
		activityMessage.GetComponent<UnityEngine.UI.Text>().text = activityMsg;

		foreach (PlayerScript player in players)
		{
			player.RpcSetActivityMessage(activityMsg);
		}
	}

	public void ActivateActivityMessage(bool show)
	{
		if (!isServer)
			return;

		activityMessage.SetActive(show);
	}
}
