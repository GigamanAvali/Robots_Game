using UnityEngine;
using System.Collections;
using TMPro;

namespace Project.Core.Localization
{
	[DisallowMultipleComponent]
	public class LocalizationTMP : MonoBehaviour
	{
		protected TMP_Text _textMesh;

		private string _localizationKey = "";

		private void Start () 
		{
			ChangeLanguageCommand();
			Localization.Instance.AllLocalizedTMP.Add(this);
		}

		protected virtual string GetKey()
		{
			return _textMesh != null ? _textMesh.text : string.Empty;
		}

		public void SetKey(string key)
		{
			_localizationKey = key;
			ChangeLanguageCommand();
		}

		public void ChangeLanguageCommand()
		{
			if (_textMesh == null)
				_textMesh = GetComponent<TMP_Text>();

			if (_textMesh != null)
			{
				if(string.IsNullOrEmpty( _localizationKey ))
					_localizationKey = GetKey();

                Localization.Instance.LocalizeTMP(_textMesh, _localizationKey, _localizationKey);
			}
		}
	}
}
