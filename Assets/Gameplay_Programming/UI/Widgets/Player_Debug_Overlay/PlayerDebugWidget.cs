using TMPro;
using UnityEngine;

public class PlayerDebugWidget : MonoBehaviour
{
    [SerializeField] TMP_Text fps;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        InvokeRepeating(nameof(UpdateFPS),1.0f,1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateFPS()
    {
        float _fps = (1.0f / Time.unscaledDeltaTime);
        fps.text = "FPS : " + ((int)_fps).ToString();
    }
}
