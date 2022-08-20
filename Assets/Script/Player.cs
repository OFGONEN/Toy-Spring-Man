/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class Player : MonoBehaviour
{
#region Fields
  [ Title( "Shared Variables" ) ]
    [ SerializeField ] SharedVector2 shared_input_drag;


// Delegates
    UnityMessage onUpdateMethod;
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
		onUpdateMethod = ExtensionMethods.EmptyMethod;
	}

    private void Start()
    {
		OnLevelStart();
	}

    private void Update()
    {
		onUpdateMethod();
	}
#endregion

#region API
    public void OnLevelStart()
    {
		onUpdateMethod = Movement;
	}
#endregion

#region Implementation
    void Movement()
    {
		var position = transform.position;

		position.x = Mathf.Clamp(
			position.x + shared_input_drag.sharedValue.x * GameSettings.Instance.player_movement_speed_lateral,
			-GameSettings.Instance.player_movement_clamp_lateral,
			GameSettings.Instance.player_movement_clamp_lateral
		);

		position.z += GameSettings.Instance.player_movement_speed_forward * Time.deltaTime;

		transform.position = position;

		//Info: Since LeanTouch executive order is before default time, We can default an input value every frame after use
		shared_input_drag.sharedValue = Vector2.zero;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}