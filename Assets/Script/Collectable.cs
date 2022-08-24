/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class Collectable : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] ColorData data_color; 
    [ SerializeField ] int data_value = 1; 

  [ Title( "Shared Variable" ) ]
    [ SerializeField ] ColorData data_color_player; 
    [ SerializeField ] IntGameEvent event_player_length_gained; 
    [ SerializeField ] IntGameEvent event_player_length_lost; 
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnTrigger()
    {
        if( data_color.ColorID == data_color_player.ColorID )
			event_player_length_gained.Raise( data_value );
		else
			event_player_length_lost.Raise( 0 );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
