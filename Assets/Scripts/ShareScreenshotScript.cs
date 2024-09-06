using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ShareScreenshotScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainMenuCanvas;
    private ARPointCloudManager _aRPointCloudManager;

    // Start is called before the first frame update
    void Start()
    {
        _aRPointCloudManager = FindObjectOfType<ARPointCloudManager>(); 
    }

    public void TakeScreenshot()
    {
        ShowHideARContents();
        StartCoroutine(TakeScreenshotAndShare());
    }

    private void ShowHideARContents()
    {
        _mainMenuCanvas.SetActive(!_mainMenuCanvas.activeSelf);
        var points = _aRPointCloudManager.trackables;

        foreach (var point in points) { 
            point.gameObject.SetActive(!point.gameObject.activeSelf);
        }
    }

    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare()
            .AddFile(filePath)
            .SetSubject("Captura de Pantalla Home Design App")
            .SetText("Mira el diseño de mi casa con estos muebles impresionantes!")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();

        ShowHideARContents();
    }
}
