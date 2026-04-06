using UnityEngine;
using UnityEngine.Video;

[DefaultExecutionOrder(2)]
public class L3VideoInput : MonoBehaviour
{
    // load level 3 video, disable image when load
    private void Awake()
    {
        GameObject tpImage = GameObject.Find("tpAnimation");
        VideoPlayer vp = GetComponent<VideoPlayer>();
        vp.clip = Resources.Load<VideoClip>("Video/UnityVideoFolder/tp");
        tpImage.SetActive(false);
    }
}
