using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;
/// <summary>
/// <see cref="TimeNetworkChecker"/> is the class that inherits from <seealso cref="MonoBehaviour"/> class.
/// <para>
/// <see cref="MonoBehaviour"/> is the base class from which every Unity script derives.
/// </para>
/// <see href="https://docs.unity3d.com/ScriptReference/MonoBehaviour.html">Unity Documentation - MonoBehaviour</see>
/// </summary>
public class TimeNetworkChecker : MonoBehaviour
{
    private AndroidJavaClass _NativeClass = null;

    private const string PACKAGE_NAME = "com.sh1.androidnativeutil";
    private const string CLASS_NAME = "SystemSettings";
    private const string METHOD_AUTO_TIME = "IsAutoTime";
    private const string METHOD_AUTO_TIME_ZONE = "IsAutoTimeZone";

    [SerializeField]
    private Text _Text = null;

    private void Start()
    {
        _NativeClass = new AndroidJavaClass($"{PACKAGE_NAME}.{CLASS_NAME}");
    }

    private void OnDestroy()
    {
        _NativeClass?.Dispose();
        _NativeClass = null;
    }

    private void Update()
    {
        var text = "";

        try
        {
            var isAutoTime = false; 
            var isAutoTimeZone = false;
#if UNITY_ANDROID
            isAutoTime = _NativeClass.CallStatic<bool>(METHOD_AUTO_TIME);
            isAutoTimeZone = _NativeClass.CallStatic<bool>(METHOD_AUTO_TIME_ZONE);
#endif
            text = $"Time:{isAutoTime}, TimeZone:{isAutoTimeZone}";
        }
        catch(Exception ex)
        {
            text = ex.Message;
        }

        if (_Text != null)
        {
            _Text.text = text;
        }
    }
}
