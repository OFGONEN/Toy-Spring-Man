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
    [ LabelText( "Width" ), SerializeField ] float data_width;
    [ LabelText( "Is Positive" ), SerializeField ] bool data_positive;

  [ Title( "Shared Variables" ) ]
    [ SerializeField ] PlayerWidth property_player_width;

  [ Title( "Components" ) ]
    [ SerializeField ] TextMeshProUGUI _text;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnTrigger()
    {
        if( data_positive )
			property_player_width.Add( data_width );
        else
			property_player_width.Substact( data_width );
	}

    public void OnPlayerColorChange( ColorData colorData )
    {

    }
#endregion

#region Implementation
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
