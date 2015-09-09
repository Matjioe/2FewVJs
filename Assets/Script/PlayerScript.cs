using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerScript : NetworkBehaviour {

	public GameObject UI;
	public GameObject bodyText;
	public GameObject body;
	public GameObject activityMessageText;

	public enum BodyType
	{
		TextBody,
		PixelBody
	}

	[SyncVar]
	public float expression = 1.0f;
	[SyncVar(hook="OnTextChange")]
	public string text = "";
	[SyncVar(hook="OnHost")]
	public bool isHost = false;

	private PlayerController controller;
	private ExpressionEvaluator expressionEvaluator;
	
	void Start()
	{
		controller = GetComponent<PlayerController>();
		expressionEvaluator = GetComponent<ExpressionEvaluator>();
	}

	public override void OnStartLocalPlayer ()
	{
		if (isServer)
		{
			gameObject.SetActive(false);
			CmdSetHost();
			return;
		}

		GameObject UIContainer = GameObject.Find("ClientUIContainer");
		UI.transform.SetParent(UIContainer.transform);
		UI.SetActive(true);

		// Init text with player Nb
		CmdSetText(netId.Value.ToString());
	}

	public override void OnStartClient ()
	{
		OnTextChange(text);
	}

	public override void OnStartServer ()
	{
		MyGameManager.instance.RegisterPlayer(this);
		RpcSetActivityMessage(MyGameManager.instance.GetComponent<UnityEngine.UI.Text>().text);
	}
	
	void Destroy()
	{
		MyGameManager.instance.UnregisterPlayer(this);
	}

	void Update ()
	{
		if (isClient)
		{
			expressionEvaluator.SetExpressionValue1(expression);
			expressionEvaluator.Compute();
		}

		if (!isLocalPlayer)
			return;

		if (Input.GetKeyDown(KeyCode.UpArrow))
			controller.MoveForward();
		if (Input.GetKeyDown(KeyCode.DownArrow))
			controller.MoveBack();
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			controller.MoveLeft();
		if (Input.GetKeyDown(KeyCode.RightArrow))
			controller.MoveRight();
	}

	public void SetExpression(float val)
	{
		CmdSetExpression(val);
	}

	[Command]
	void CmdSetExpression(float val)
	{
		expression = val;
	}

	[Command]
	void CmdSetText(string val)
	{
		text = val;
	}

	void OnTextChange(string newText)
	{
		text = newText;
		bodyText.GetComponent<TextMesh>().text = text;
	}

	[ClientRpc]
	public void RpcChangeColor(Color color)
	{
		body.GetComponent<MeshRenderer>().material.color = color;
	}

	[ClientRpc]
	public void RpcSetActivityMessage(string msg)
	{
		activityMessageText.GetComponent<UnityEngine.UI.Text>().text = msg;
	}

	[ClientRpc]
	public void RpcChangeBody(BodyType bodyType)
	{
		if (bodyType == BodyType.PixelBody)
		{
			body.SetActive(true);
			bodyText.SetActive(false);
		}
		else
		{
			body.SetActive(false);
			bodyText.SetActive(true);
		}
	}

	[Command]
	void CmdSetHost()
	{
		isHost = true;
	}

	void OnHost(bool newIsHost)
	{
		isHost = newIsHost;
		gameObject.SetActive(false);
		MyGameManager.instance.UnregisterPlayer(this);
	}
}
