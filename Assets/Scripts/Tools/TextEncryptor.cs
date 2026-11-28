using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class TextEncryptor
{
	// Ключ: строго 32 символа (256 бит)
	private static readonly byte[] Key = Encoding.UTF8.GetBytes("MySuperSecretKeyForRobotsGame001");
	// Вектор инициализации (IV): строго 16 символов (128 бит)
	private static readonly byte[] IV = Encoding.UTF8.GetBytes("MySecretIV123456");

	// Шифруем строку -> получаем зашифрованную строку (Base64)
	public static string Encrypt(string plainText)
	{
		if (string.IsNullOrEmpty(plainText)) return string.Empty;

		using (Aes aes = Aes.Create())
		{
			aes.Key = Key;
			aes.IV = IV;

			ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

			using (MemoryStream ms = new MemoryStream())
			using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
			{
				using (StreamWriter sw = new StreamWriter(cs, Encoding.UTF8))
				{
					sw.Write(plainText);
				}
				// Переводим зашифрованные байты в текстовый формат Base64
				return Convert.ToBase64String(ms.ToArray());
			}
		}
	}

	// Расшифровываем строку (Base64) -> получаем исходный текст
	public static string Decrypt(string cipherText)
	{
		if (string.IsNullOrEmpty(cipherText)) return string.Empty;

		try
		{
			// Возвращаем строку Base64 обратно в массив байтов
			byte[] buffer = Convert.FromBase64String(cipherText);

			using (Aes aes = Aes.Create())
			{
				aes.Key = Key;
				aes.IV = IV;

				ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

				using (MemoryStream ms = new MemoryStream(buffer))
				using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
				using (StreamReader sr = new StreamReader(cs, Encoding.UTF8))
				{
					return sr.ReadToEnd();
				}
			}
		}
		catch (Exception e)
		{
			Debug.LogError("Ошибка расшифровки (возможно, текст был изменен вручную): " + e.Message);
			return string.Empty;
		}
	}
}