using GoogleARCore;
using GoogleARCore.Examples.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera FirstPersonCamera;
    public InstantPlacementMenu InstantPlacementMenu;
    public GameObject playground = null;
    bool isInited = false;

    void OnPlaneDetected()
    {
        /*
        if (isInited) return;
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if(Frame.Raycast(Screen.width*0.5f,Screen.height*0.5f,raycastFilter, out hit))
        {
            Pose pose = hit.Pose;
            var go = Instantiate(playground,pose.position,pose.rotation);
            isInited = true;
        }
        */

        if (isInited) return;
        TrackableHit hit;
        bool foundHit = false;
        if (InstantPlacementMenu.IsInstantPlacementEnabled())
        {
            foundHit = Frame.RaycastInstantPlacement(
                Screen.width * 0.5f, Screen.height * 0.5f, 1.0f, out hit);
        }
        else
        {
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                TrackableHitFlags.FeaturePointWithSurfaceNormal;
            foundHit = Frame.Raycast(
                Screen.width * 0.5f, Screen.height * 0.5f, raycastFilter, out hit);
        }

        if (foundHit)
        {
            // Use hit pose and camera pose to check if hittest is from the
            // back of the plane, if it is, no need to create the anchor.
            if ((hit.Trackable is DetectedPlane) &&
                Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                    hit.Pose.rotation * Vector3.up) < 0)
            {
                Debug.Log("Hit at back of the current DetectedPlane");
            }
            else
            {
                Pose pose = hit.Pose;
                var go = Instantiate(playground, pose.position, Quaternion.Euler(0,0,0));
                isInited = true;
                GameManager.instance.firstFloor = true;
                GameManager.instance.StartPose = pose;
                UIManager.instance.floorButton.SetActive(false);
                UIManager.instance.actionButton.SetActive(true);
            }
        }
    }
}
