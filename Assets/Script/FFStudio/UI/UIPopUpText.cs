/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	public class UIPopUpText : MonoBehaviour
	{
#region Fields
		UI_Float ui_float;
		UI_Fade_Text ui_text;

		Vector3 ui_start_scale;
#endregion

#region Properties
#endregion

#region Unity API
		void Awake()
		{
			ui_float = GetComponentInChildren< UI_Float >();
			ui_text  = GetComponentInChildren< UI_Fade_Text >();

			ui_start_scale = ui_float.UI_RectTransform.localScale;
		}
#endregion

#region API
		public void Spawn( Vector3 position, string text, float size, Color color, Transform parent = null )
		{
			gameObject.SetActive( true );
			transform.position = position;
			transform.SetParent( parent );

			ui_float.DoFloat( GameSettings.Instance.ui_PopUp_height,
				GameSettings.Instance.ui_PopUp_duration );

			ui_text.DoFade( 0, GameSettings.Instance.ui_PopUp_duration );

			ui_float.UI_RectTransform.localScale = ui_start_scale * size;

			ui_text.UI_Text.text = text;
			ui_text.UI_Text.color = color;
		}

		public void KillTweens()
		{
			ui_float.KillTween();
			ui_text.KillTween();
		}

		public void CompleteTweens()
		{
			ui_float.CompleteTween();
			ui_text.CompleteTween();
		}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}