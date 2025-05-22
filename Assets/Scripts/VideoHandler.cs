using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoHandler : MonoBehaviour
{
    [SerializeField] private VideoPlayer video;
    [SerializeField] private bool isOpenVideo = false;

    void Start()
    {
        if (video != null)
        {
            // Adiciona um callback para o evento que é disparado quando o vídeo termina
            video.loopPointReached += OnVideoEnd;
            video.Play();
            if (isOpenVideo) video.Pause();
        }
        else
        {
            Debug.LogError("VideoPlayer não foi atribuído no Inspector!");
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Retorna à cena de índice 0
        // SceneManager.LoadScene(0);
    }
}
