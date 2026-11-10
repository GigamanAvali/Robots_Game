using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Project.Core.Localization
{
	[DisallowMultipleComponent]
	public class LocalizationText : MonoBehaviour
	{
		public Text Text
		{
			set { _text = value; }
			get
			{
				if (_text == null)
					_text = GetComponent<Text>();
				return _text;
			}
		}
		protected Text _text;
		private object[] parameters = null;

		private string _localizationKey = "";
		public string LocalizationKey
		{
			set
			{
				_localizationKey = value;
                Text.text = value;
                UpdateText();
			}
			get { return _localizationKey; }
		}

		private void Start () 
		{
			ChangeLanguageCommand();
			Localization.Instance.AllLocalizedText.Add(this);
		}

		private void OnDestroy()
		{
			if (Localization.IsAlive 
				&& Localization.Instance.AllLocalizedText != null 
				&& Localization.Instance.AllLocalizedText.Contains(this)
				)
			{
				Localization.Instance.AllLocalizedText.Remove(this);
			}
			
		}

		protected virtual string GetKey()
		{
			return _text != null ? _text.text : string.Empty;
		}

		public void ChangeLanguageCommand()
		{
			if (_text == null)
				_text = GetComponent<Text>();

			if (_text != null)
			{
				if(string.IsNullOrEmpty( _localizationKey ))
					_localizationKey = GetKey();

				//Localization.Instance.LocalizeText(_text, _localizationKey, _localizationKey);
				UpdateText();
			}
		}

		public void ReLocalize()
		{
			if (_text == null)
				_text = GetComponent<Text>();

			if (_text != null)
			{
				_localizationKey = GetKey();
				//Localization.Instance.LocalizeText(_text, _localizationKey, _localizationKey);
				UpdateText();
			}
		}

		public void SetParameters(params object[] parameters)
		{
			if (_text == null)
				ReLocalize();

			this.parameters = parameters;

			UpdateText();
		}

		private void UpdateText()
		{
			string text = Localization.Instance.GetValueByKey(_localizationKey, _localizationKey);
			if (_text != null)
			{
				if (parameters != null)
				{
					_text.text = string.Format(text, parameters);

                }
				else
				{
					_text.text = text;
				}
			}
		}

	}
}
