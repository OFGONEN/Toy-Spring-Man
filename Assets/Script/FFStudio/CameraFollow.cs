/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

namespace FFStudio
{
    public class CameraFollow : MonoBehaviour
    {
#region Fields
    [ Title( "Shared Variables" ) ]
        [ SerializeField ] SharedReferenceNotifier notif_player_transform;
        [ SerializeField ] SharedFloat shared_camera_offset_ratio;

        Transform target_transform;
// Delegate
        UnityMessage onUpdateMethod;
#endregion

#region Properties
#endregion

#region Unity API
        void Awake()
        {
            onUpdateMethod = ExtensionMethods.EmptyMethod;
        }

        void Update()
        {
            onUpdateMethod();
        }
#endregion

#region API
        public void OnLevelStart()
        {
			target_transform = notif_player_transform.sharedValue as Transform;
			onUpdateMethod   = FollowTarget;
		}

        public void OnPlayerLengthChanged( int value )
        {
			shared_camera_offset_ratio.sharedValue = ( float )value / GameSettings.Instance.player_length_max_pseudo;
		}
#endregion

#region Implementation
        void FollowTarget()
        {
			// Info: Simple follow logic.
			var offset = Vector3.Lerp( GameSettings.Instance.camera_follow_offset_start, GameSettings.Instance.camera_follow_offset_end, shared_camera_offset_ratio.sharedValue );
			var targetPosition = target_transform.position + offset;

            transform.position = Vector3.Lerp( transform.position, targetPosition, GameSettings.Instance.camera_follow_speed * Time.deltaTime );
        }
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
    }
}