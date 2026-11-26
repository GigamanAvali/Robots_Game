using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIProfilePanel : MonoBehaviour
{
	[SerializeField] private TMP_InputField inputField;
	[SerializeField] private TMP_Text selectedText;
	[SerializeField] private Transform content;
	[SerializeField] private UIProfilePlayerItem profileItemPrefab;
	private List<PlayerProfile> playersProfiles;
	private PlayerProfile curPlayer = null;
	private UIProfilePlayerItem curPlayerItem = null;
	void Start()
	{
		GetProfiles();
		ShowProfiles();
	}

	void Update()
	{

	}

	private void GetProfiles()
	{
		string playersProfilesStr = PlayerPrefs.GetString("PlayersProfiles", "");
		//playersProfilesStr = "";
		if (playersProfilesStr != "")
		{
			Debug.Log(playersProfilesStr);
			playersProfiles = JsonConvert.DeserializeObject<List<PlayerProfile>>(playersProfilesStr);
		}
		else
			playersProfiles = new List<PlayerProfile>();
	}

	public void SetProfiles()
	{
		string playersProfilesStr = JsonConvert.SerializeObject(playersProfiles);
		Debug.Log(playersProfilesStr);
		PlayerPrefs.SetString("PlayersProfiles", playersProfilesStr);
	}

	public void CreateProfile()
	{
		if (inputField.text == "")
			return;
		curPlayer = new PlayerProfile();
		curPlayer.PlayerName = inputField.text;
		playersProfiles.Add(curPlayer);
		SetProfiles();
		var newItemPrefab = Instantiate(profileItemPrefab, content);
		newItemPrefab.SetData(curPlayer);
		newItemPrefab.gameObject.SetActive(true);
		selectedText.text = $"Текущий игрок: {curPlayer.PlayerName}";
		//ShowProfiles();
	}

	private void ShowProfiles()
	{
		string lastCurPlayerName = PlayerPrefs.GetString("LastPlayer", "");
		foreach (var item in playersProfiles)
		{
			var newItemPrefab = Instantiate(profileItemPrefab, content);
			newItemPrefab.SetData(item);
			newItemPrefab.gameObject.SetActive(true);
			if (lastCurPlayerName == "")
			{
				curPlayer = item;
			}
			else if (item.PlayerName == lastCurPlayerName)
			{
				curPlayer = item;
			}
		}
		if (curPlayer != null)
			selectedText.text = $"Текущий игрок: {curPlayer.PlayerName}";
	}

	public void SetPlayerProfileItem(UIProfilePlayerItem item)
	{
		curPlayerItem = item;
	}

	public void SelectPlayerProfileItem()
	{
		curPlayer = curPlayerItem.Profile;
		PlayerPrefs.SetString("LastPlayer", curPlayer.PlayerName);
	}
}
