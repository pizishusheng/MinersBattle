using UnityEngine;

public class FreeCamView : MonoBehaviour
{
    // 相机
    /**
     * freeCam作为camParent的子组件。
     * 这样做是为了防止在观察移动的物体时相机跟不上目标，
     * 对于静止的目标，这样做没有任何意义。
     */
    public GameObject freeCamParent;
    public Camera freeCam;

    // 观察目标
    public GameObject target;

    // 控制参数
    public float defaultDist = 3;// 相机和目标的预设距离
    public float minDist = 1.0f;// 相机和目标的最小距离
    public float maxDist = 30.0f;// 相机和目标的最大距离
    public float distStep = 0.5f;// 滚轮调整相机和目标距离的步长
    public float minFOV = 15.0f;// 相机最小FOV
    public float maxFOV = 60.0f;// 相机最大FOV
    public float fovStep = 3.0f;// 滚轮调整相机FOV的步长
    public float mouseSpeed = 5;// 鼠标速度
    [Range(10, 45)]
    public float minAngle = 10;// 相机与物体的最小垂直夹角

    // 计算量
    private Vector3 offset;// 相机相对于目标的偏移（相机 - 目标）
    private float tanLimit;// 相机自由观察视角的垂直夹角限制参数

    void Start()
    {
        // 默认设置
        if (freeCamParent == null) freeCamParent = gameObject;
        if (freeCam == null) freeCam = freeCamParent.GetComponentInChildren<Camera>();

        tanLimit = Mathf.Tan(minAngle);

        ResetCamPos();
    }

    void Update()
    {
        freeCamParent.transform.position = target.transform.position;
        //Debug.DrawRay(target.transform.position, offset);

        // 鼠标右键调整视角
        if (Input.GetMouseButton(1))
        {
            ViewRotate();
        }

        // 鼠标滚轮调整相机距离，向上滚动拉近，向下滚动拉远
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        if (mouseScroll != 0)
        {
            ViewScale(mouseScroll);
        }

        // 按下鼠标中键复位
        if (Input.GetMouseButton(2))
        {
            ResetCamPos();
        }
    }

    /// <summary>
    /// 相机围绕观察目标旋转进行自由观察
    /// </summary>
    private void ViewRotate()
    {
        float x = Input.GetAxis("Mouse X") * mouseSpeed;
        float y = Input.GetAxis("Mouse Y") * mouseSpeed;

        // 左右移动鼠标在水平方向观察物体
        freeCam.transform.RotateAround(freeCamParent.transform.position, Vector3.up, x);

        // 上下移动鼠标在垂直方向观察物体
        Vector3 axis;// 相机在垂直方向的旋转轴
        offset = freeCam.transform.position - freeCamParent.transform.position;
        // TODO 根据场景的方向修改，当前代码无法从正上/下方自由旋转观察
        if (offset.z == 0) // 几个特殊的位置，无法通过计算获得垂直向量
        {
            if (offset.x > 0) axis = new Vector3(0, 0, 1);// 相机在目标的正前方 offset(x>0, 0, 0)
            else if (offset.x < 0) axis = new Vector3(0, 0, -1);// 相机在目标的正后方 offset(x<0, 0, 0)
            else axis = new Vector3(0, 0, 0);// 相机在目标的正上/下方 offset(0， y!=0, 0)，axis为(0, 0, 0)时不能自由旋转
        }
        else
        {
            float pointX = offset.z < 0 ? 1 : -1;// 旋转轴的X坐标
            // 两个向量垂直，则：X1*X2 + Y1*Y2 + Z1*Z2 = 0，这里令axis的Y=0
            axis = new Vector3(pointX, 0, -pointX * offset.x / offset.z);
        }
        // 防止相机在垂直方向上旋转过度导致画面翻转
        float tan = float.MaxValue;
        if (offset.y != 0) tan = Mathf.Sqrt(offset.x * offset.x + offset.z * offset.z) / offset.y;
        if ((tan > tanLimit || tan < -tanLimit) || (tan > 0 && tan < tanLimit && y > 0) || (tan > -tanLimit && tan < 0 && y < 0))
        {
            freeCam.transform.RotateAround(freeCamParent.transform.position, axis, -y);
        }
    }

    /// <summary>
    /// 调整相机和观察目标之间的距离
    /// </summary>
    /// <param name="mouseScroll">Input.GetAxis("Mouse ScrollWheel")</param>
    private void ViewScale(float mouseScroll)
    {
        offset = freeCam.transform.position - freeCamParent.transform.position;
        float distance = Mathf.Sqrt(offset.x * offset.x + offset.y * offset.y + offset.z * offset.z);// 偏移向量的模，距离
        Vector3 unit = offset / distance;// 偏移向量的单位向量

        // 在相机离物体近时通过调整FOV来缩放，避免相机进入物体
        if (mouseScroll > 0)
        {
            // 滚轮向上滚动，拉近
            if (distance > minDist)
            {
                distance -= distStep;
                freeCam.transform.Translate(-unit * distStep, Space.World);
            }
            else if (freeCam.fieldOfView > minFOV)
            {
                freeCam.fieldOfView -= fovStep;
            }
        }
        if (mouseScroll < 0)
        {
            // 滚轮向下滚动，拉远
            if (freeCam.fieldOfView < maxFOV)
            {
                freeCam.fieldOfView += fovStep;
            }
            else if (distance < maxDist)
            {
                distance += distStep;
                freeCam.transform.Translate(unit * distStep, Space.World);
            }
        }
        // 另一种调整相机距离的方式
        //freeCam.transform.Translate(new Vector3(0, 0, Input.GetAxis("Mouse ScrollWheel") * distStep));
    }

    /// <summary>
    /// 相机复位
    /// </summary>
    private void ResetCamPos()
    {
        Vector3 camParentPos = freeCamParent.transform.position;
        freeCam.fieldOfView = 60;
        // TODO 根据场景的方向修改，当前代码适用于相机从Z轴负方向看向目标
        freeCam.transform.position = new Vector3(camParentPos.x, camParentPos.y, camParentPos.z - defaultDist);
        freeCam.transform.LookAt(camParentPos);
    }
}
