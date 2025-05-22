using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [Header("Configuração")]
    [Tooltip("Índice da cena a ser carregada")]
    [SerializeField] private int sceneIndex = 0;
    [Tooltip("Tempo (s) que precisa ficar dentro do trigger para carregar a cena")]
    [SerializeField] private float holdTime = 2f;
    [Tooltip("Image UI com Fill Method = Radial 360")]
    [SerializeField] private Image progressImage;

    [Header("Scaling")]
    [Tooltip("Transform do filho que será escalado")]
    [SerializeField] private Transform targetToScale;
    [Tooltip("Escala considerada 'pequena'")]
    [SerializeField] private Vector3 smallScale = new Vector3(0.5f, 0.5f, 0.5f);
    [Tooltip("Escala alvo ao entrar no trigger")]
    [SerializeField] private Vector3 largeScale = Vector3.one;
    [Tooltip("Duração da animação de escala")]
    [SerializeField] private float scaleDuration = 0.5f;
    [Tooltip("Só executa o hold se o objeto estiver na escala 'smallScale'")]
    [SerializeField] private bool onlyWhenSmall = true;

    private bool _inside = false;
    private float _timer = 0f;
    private bool _sceneLoaded = false;
    private Coroutine _currentScaleCoroutine;

    void Reset()
    {
        progressImage = GetComponentInChildren<Image>();
        // tenta pegar o primeiro filho se não atribuído
        if (targetToScale == null && transform.childCount > 0)
            targetToScale = transform.GetChild(0);
    }

    void Update()
    {
        if (_sceneLoaded) return;

        if (_inside)
        {
            _timer += Time.deltaTime;
            progressImage.fillAmount = Mathf.Clamp01(_timer / holdTime);

            if (_timer >= holdTime)
            {
                _sceneLoaded = true;
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (_sceneLoaded) return;
        if (!other.CompareTag("Player")) return;

        // se não exigir escala ou já estiver "pequeno"
        if (!onlyWhenSmall || NearlyEqual(targetToScale.localScale, smallScale))
        {
            _inside = true;
            StartScaleCoroutine(targetToScale.localScale, largeScale);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_sceneLoaded) return;
        if (!other.CompareTag("Player")) return;

        _inside = false;
        _timer = 0f;
        progressImage.fillAmount = 0f;

        if (onlyWhenSmall)
            StartScaleCoroutine(targetToScale.localScale, smallScale);
    }

    private void StartScaleCoroutine(Vector3 from, Vector3 to)
    {
        if (_currentScaleCoroutine != null)
            StopCoroutine(_currentScaleCoroutine);
        _currentScaleCoroutine = StartCoroutine(ScaleOverTime(from, to, scaleDuration));
    }

    private System.Collections.IEnumerator ScaleOverTime(Vector3 from, Vector3 to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            targetToScale.localScale = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        targetToScale.localScale = to;
    }

    private bool NearlyEqual(Vector3 a, Vector3 b, float epsilon = 0.01f)
    {
        return Vector3.SqrMagnitude(a - b) <= epsilon * epsilon;
    }
}
