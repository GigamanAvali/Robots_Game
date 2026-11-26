using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIProfilePlayerItem : MonoBehaviour
{
	[SerializeField] private TMP_Text name;
	private PlayerProfile profile;
	public PlayerProfile Profile
	{
		get { return profile; }
	}
	public void SetData(PlayerProfile _profile)
	{
		profile = _profile;
		name.text = profile.PlayerName;
	}
}
