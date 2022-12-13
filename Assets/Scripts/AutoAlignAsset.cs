using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Raph
{
    public class AutoAlignAsset : MonoBehaviour
    {

        public void AligntoGroundNormal(){
             if (Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit, 100)) {
                //found ground to align to
                transform.position = hit.point;
                transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                Debug.DrawRay(hit.point, hit.normal * 3f, Color.red,1f);
             }
             else{
                //no ground found -> no alignement
                Debug.LogWarning("No ground found to align for : " + this.name);
             }
        }

    }
}
