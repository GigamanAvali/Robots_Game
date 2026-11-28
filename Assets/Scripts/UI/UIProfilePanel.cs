using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class UIProfilePanel : MonoBehaviour
{
	[SerializeField] private TMP_InputField inputField;
	[SerializeField] private TMP_Text selectedText;
	[SerializeField] private Transform content;
	[SerializeField] private UIProfilePlayerItem profileItemPrefab;
	private List<PlayerProfile> playersProfiles;
	private PlayerProfile curPlayer = null;
	private UIProfilePlayerItem curPlayerItem = null;
	private string dataPath = Application.dataPath + "/../Saves";
	string key = "PlayersProfiles.json";
	string keyBin = "PlayersProfiles.bin";
	private bool isBinary = true;

	void Start()
	{
		if (isBinary)
		{
			CheckProfilesBinaryData();
			GetProfilesBinary();
		}
		else
		{
			CheckProfilesData();
			GetProfilesText();
		}
		ShowProfiles();

		string originalName = "Gigman_Robots_Game";
		string encryptedName = TextEncryptor.Encrypt(originalName);
		Debug.Log("Зашифровано: " + encryptedName);
		string decryptedName = TextEncryptor.Decrypt(encryptedName);
		Debug.Log("Расшифровано: " + decryptedName);
	}

	void Update()
	{

	}

	private void GetProfiles()
	{
		string playersProfilesStr = PlayerPrefs.GetString("PlayersProfiles", "");
		//playersProfilesStr = "";
		DeserializeProfilesData(playersProfilesStr);
	}

	public void SetProfiles()
	{
		string playersProfilesStr = JsonConvert.SerializeObject(playersProfiles);
		Debug.Log(playersProfilesStr);
		PlayerPrefs.SetString("PlayersProfiles", playersProfilesStr);
	}

	private void GetProfilesText()
	{
		// Путь к файлу
		string playersProfilesStr;
		string filePath = Path.Combine(dataPath, key);
		using (StreamReader reader = new(filePath))
		{
			playersProfilesStr = reader.ReadToEnd();
		}
		DeserializeProfilesData(playersProfilesStr);
	}

	public void SetProfilesText()
	{
		string playersProfilesStr = JsonConvert.SerializeObject(playersProfiles);
		// Путь к файлу
		string filePath = Path.Combine(dataPath, key);

		// Текстовый файл
		using (StreamWriter writer = new(filePath))
		{
			writer.Write(playersProfilesStr);
		}
	}
	private void GetProfilesBinary()
	{
		// Путь к файлу
		string filePath = Path.Combine(dataPath, keyBin);
		//string playersProfilesStr;
		playersProfiles = new List<PlayerProfile>();
		using (FileStream stream = new FileStream(filePath, FileMode.Open))
		using (BinaryReader reader = new BinaryReader(stream))
		{
			int count = reader.ReadInt32();
			for (int i = 0; i < count; i++)
			{
				PlayerProfile item = new PlayerProfile();
				item.PlayerName = reader.ReadString();
				item.Lvl = reader.ReadInt32();
				item.Time = reader.ReadSingle();
				playersProfiles.Add(item);
			}
		}
		//DeserializeProfilesData(playersProfilesStr);
	}

	public void SetProfilesBinary()
	{
		//string playersProfilesStr = JsonConvert.SerializeObject(playersProfiles);
		// Путь к файлу
		string filePath = Path.Combine(dataPath, keyBin);

		//Бинарный файл.
		using (FileStream stream = new FileStream(filePath, FileMode.Create))
		using (BinaryWriter writer = new BinaryWriter(stream))
		{
			writer.Write(playersProfiles.Count);
			foreach (var item in playersProfiles)
			{
				writer.Write(item.PlayerName);
				writer.Write(item.Lvl);
				writer.Write(item.Time);
			}
			//writer.Write(playersProfilesStr);
		}
	}

	private void CheckProfilesData()
	{
		// Проверка папки с сохранениями
		if (!Directory.Exists(dataPath))
		{
			//Если нет то создаем
			Directory.CreateDirectory(dataPath);
		}
		// Путь к файлу
		string filePath = Path.Combine(dataPath, key);
		if (!File.Exists(filePath))
		{
			Debug.LogWarning("Файл сохранения не найден. Создаем новый профиль.");
			// Cоздаем файл с данными по умолчанию
			File.WriteAllText(filePath, "");
			return;
		}
	}

	private void CheckProfilesBinaryData()
	{
		// Проверка папки с сохранениями
		if (!Directory.Exists(dataPath))
		{
			//Если нет то создаем
			Directory.CreateDirectory(dataPath);
		}
		// Путь к файлу
		string filePath = Path.Combine(dataPath, keyBin);
		if (!File.Exists(filePath))
		{
			using (FileStream stream = new FileStream(filePath, FileMode.Create))
			using (BinaryWriter writer = new BinaryWriter(stream))
			{
				writer.Write(0);
			}
		}
	}

	private void DeserializeProfilesData(string data)
	{
		// Десериализация с указанием конкретного типа
		
		if (data != "")
		{
			Debug.Log(data);
			playersProfiles = JsonConvert.DeserializeObject<List<PlayerProfile>>(data);
		}
		else
		{
			playersProfiles = new List<PlayerProfile>();
		}
	}

	public void CreateProfile()
	{
		if (inputField.text == "")
			return;
		curPlayer = new PlayerProfile();
		curPlayer.PlayerName = inputField.text;
		playersProfiles.Add(curPlayer);
		if (isBinary)
			SetProfilesBinary();
		else
			SetProfilesText();

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
