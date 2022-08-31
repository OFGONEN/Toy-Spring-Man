/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "data_color", menuName = "FF/Game/Data/Color" ) ]
public class ColorData : ScriptableObject
{
#region Fields
    [ LabelText( "Color Data" ), SerializeField ] Color data_color;
    [ LabelText( "Color ID" ), SerializeField ] int data_id;

    public int ColorID => data_id;
    public Color Color => data_color;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void ChangeData( ColorData data )
    {
		data_color = data.data_color;
		data_id    = data.data_id;
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
