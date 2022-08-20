/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class ColorGate : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] ColorData data_color; 
    [ SerializeField ] ColorData data_color_player; 
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnTrigger()
    {
		data_color_player.ChangeData( data_color );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
