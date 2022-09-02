/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using TMPro;
using Sirenix.OdinInspector;

public class WidthGate : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ LabelText( "Gate Pair" ), SerializeField ] GameObject gate_pair;
    [ LabelText( "Width" ), SerializeField ] float data_width;
    [ LabelText( "Is Positive" ), SerializeField ] bool data_positive;

  [ Title( "Shared Variables" ) ]
    [ SerializeField ] PlayerWidth property_player_width;

  [ Title( "Components" ) ]
    [ SerializeField ] TextMeshProUGUI _text;
    [ SerializeField ] ColorSetter colorSetter;
    [ SerializeField ] ParticleSystem _particleSystem;
#endregion

#region Properties
#endregion

#region Unity API
	private void Start()
	{
		if( data_positive )
			ChangeColor( CurrentLevelData.Instance.levelData.player_color_data.ColorGateWidth );
	}
#endregion

#region API
    public void OnTrigger()
    {
        if( data_positive )
			property_player_width.Add( data_width );
        else
			property_player_width.Substact( data_width );

		gameObject.SetActive( false );

		if( gate_pair )
			gate_pair.SetActive( false );
	}

    public void OnPlayerColorChange( ColorData colorData )
    {
		ChangeColor( colorData.ColorGateWidth );
	}
#endregion

#region Implementation
	void ChangeColor( Color color )
	{
		colorSetter.SetColor( color );
		var main = _particleSystem.main;
		main.startColor = color;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
    private void OnValidate()
    {
		string modifier = "-";

        if( data_positive )
			modifier = "+";

		_text.text = modifier + data_width;
	}
#endif
#endregion
}
