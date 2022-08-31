/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;

public class FInalStageStep : MonoBehaviour
{
#region Fields
    [ SerializeField ] SharedReferenceNotifier notif_player_transform;
    [ SerializeField ] Transform target_transform;
    [ SerializeField ] ParticleSystem _particleSystem;

    UnityMessage onUpdateMethod;
    Transform player_transform;

    RecycledTween recycledTween = new RecycledTween();
#endregion

#region Properties
#endregion

#region Unity API
    private void OnDisable()
    {
        recycledTween.Kill();
    }

    private void Awake()
    {
		onUpdateMethod = ExtensionMethods.EmptyMethod;
        target_transform.localEulerAngles = Vector3.right * GameSettings.Instance.finalStage_step_rotation_start;
	}

    private void Update()
    {
		onUpdateMethod();
	}
#endregion

#region API
    public void OnPlayerReachedFinalStage()
    {
		player_transform = notif_player_transform.sharedValue as Transform;
		onUpdateMethod   = PlayerCheckPosition;
	}

    public void OnLevelFinished()
    {
		onUpdateMethod = ExtensionMethods.EmptyMethod;
	}
#endregion

#region Implementation
    void PlayerCheckPosition()
    {
        if( transform.position.z < player_transform.position.z )
			PlayerPassedPlatform();
	}

    void PlayerPassedPlatform()
    {
		onUpdateMethod = ExtensionMethods.EmptyMethod;

		recycledTween.Recycle( target_transform.DORotate( Vector3.right * GameSettings.Instance.finalStage_step_rotation_end,
			GameSettings.Instance.finalStage_rotation_duration )
			.SetEase( GameSettings.Instance.finalStage_rotation_ease ) );

		_particleSystem.Play( true );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
