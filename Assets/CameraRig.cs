using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraRig : MonoBehaviour
{
    private Transform target;
    private Vector3 camPosSetting;
    private CameraMoveSetting camMoveSetting;
    private float camSpeed;
    public void setCameraSettings(Transform target, CameraPositionSetting setting, float distance,
        CameraMoveSetting move, float speed)
    {
        this.target = target;
        if (target == null)
        {
            return;
        }
        if (setting == CameraPositionSetting.NONE)
        {
            camPosSetting = target.position - transform.position;
        }
        else
        {
            if (setting == CameraPositionSetting.BACK)
            {
                camPosSetting = -target.forward;
            }
            else if (setting == CameraPositionSetting.BACK_BOTTOM_LEFT)
            {
                camPosSetting = -target.forward + -target.up + -target.right;
            }
            else if (setting == CameraPositionSetting.BACK_BOTTOM_RIGHT)
            {
                camPosSetting = -target.forward + -target.up + target.right;
            }
            else if (setting == CameraPositionSetting.BACK_LEFT)
            {
                camPosSetting = -target.forward + -target.right;
            }
            else if (setting == CameraPositionSetting.BACK_RIGHT)
            {
                camPosSetting = -target.forward + target.right;
            }
            else if (setting == CameraPositionSetting.BACK_TOP_LEFT)
            {
                camPosSetting = -target.forward + target.up + -target.right;
            }
            else if (setting == CameraPositionSetting.BACK_TOP_RIGHT)
            {
                camPosSetting = -target.forward + target.up + target.right;
            }
            else if (setting == CameraPositionSetting.FRONT)
            {
                camPosSetting = Vector3.forward;
            }
            else if (setting == CameraPositionSetting.FRONT_BOTTOM_LEFT)
            {
                camPosSetting = target.forward + -target.up + -target.right;
            }
            else if (setting == CameraPositionSetting.FRONT_BOTTOM_RIGHT)
            {
                camPosSetting = target.forward + -target.up + target.right;
            }
            else if (setting == CameraPositionSetting.FRONT_LEFT)
            {
                camPosSetting = target.forward + -target.right;
            }
            else if (setting == CameraPositionSetting.FRONT_RIGHT)
            {
                camPosSetting = target.forward + target.right;
            }
            else if (setting == CameraPositionSetting.FRONT_TOP_LEFT)
            {
                camPosSetting = target.forward + target.up + -target.right;
            }
            else if (setting == CameraPositionSetting.FRONT_TOP_RIGHT)
            {
                camPosSetting = target.forward + target.up + -target.right;
            }
            else if (setting == CameraPositionSetting.LEFT)
            {
                camPosSetting = -target.right;
            }
            else if (setting == CameraPositionSetting.RIGHT)
            {
                camPosSetting = target.right;
            }
            else if (setting == CameraPositionSetting.TOP)
            {
                camPosSetting = target.up;
            }

            camPosSetting = camPosSetting.normalized * distance;
        }

        camMoveSetting = move;
        camSpeed = speed;
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }
        if (camMoveSetting == CameraMoveSetting.ORIENT)
        {
            Vector3 pos = target.position + camPosSetting;
            Quaternion rot = Quaternion.LookRotation(target.position - transform.position);
            transform.SetPositionAndRotation(pos, rot);
        }
        else if (camMoveSetting == CameraMoveSetting.FOLLOW_PAN)
        {
            transform.position = target.position + camPosSetting;
        }
        else if (camMoveSetting == CameraMoveSetting.FOLLOW_ROTATE)
        {
            transform.rotation = Quaternion.LookRotation(target.position - transform.position);
        }
    }
    public enum CameraPositionSetting
    {
        NONE, FRONT, BACK, LEFT, RIGHT, TOP, FRONT_RIGHT, FRONT_LEFT, BACK_RIGHT, BACK_LEFT,
        FRONT_TOP_RIGHT, FRONT_BOTTOM_RIGHT, FRONT_TOP_LEFT, FRONT_BOTTOM_LEFT,
        BACK_TOP_RIGHT, BACK_BOTTOM_RIGHT, BACK_TOP_LEFT, BACK_BOTTOM_LEFT
    }
    public enum CameraMoveSetting
    {
        NONE, ORIENT, FOLLOW_PAN, FOLLOW_ROTATE
    }
}
