using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotater : MonoBehaviour
{
    public float speed;
    void Update()
    {RenderSettings.skybox.SetFloat("_Rotation",Time.time*speed);}
}
