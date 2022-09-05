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
    [ LabelText( "Color Body Color" ), SerializeField ] Color data_color_body;
    [ LabelText( "Color Looping Spring Color" ), SerializeField ] Color data_color_spring_looping;
    [ LabelText( "Color Tight Spring Color" ), SerializeField ] Color data_color_spring_tight;
    [ LabelText( "Color Width Gate" ), SerializeField ] Color data_color_gate_width;
    [ LabelText( "Color ID" ), SerializeField ] int data_id;

    public int ColorID => data_id;
    public Color ColorBody => data_color_body;
    public Color ColorSpringLooping => data_color_spring_looping;
    public Color ColorSpringTight => data_color_spring_tight;
    public Color ColorGateWidth => data_color_gate_width;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void ChangeData( ColorData data )
    {
		data_color_body           = data.data_color_body;
		data_color_spring_looping = data.data_color_spring_looping;
		data_color_spring_tight   = data.data_color_spring_tight;
		data_color_gate_width     = data.data_color_gate_width;

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