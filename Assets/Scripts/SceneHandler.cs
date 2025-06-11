using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneHandler : MonoBehaviour, ISelectable
{
    [Header("Configuração")]
    [SerializeField] int sceneIndex = 0;
    [SerializeField] float holdTime = 2f;
    [SerializeField] Image progressImage;

    [Header("Scaling")]
    [SerializeField] Transform targetToScale;
    [SerializeField] Vector3 smallScale = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] Vector3 largeScale = Vector3.one;
    [SerializeField] float scaleDuration = 0.5f;
    [SerializeField] bool onlyWhenSmall = true;

    Coroutine _holdCoroutine;
    Coroutine _scaleCoroutine;
    bool _sceneLoaded = false;

    public void OnSelect()
    {
        if (_sceneLoaded) return;

        // se necessário, escala para “large”
        if (!onlyWhenSmall || NearlyEqual(targetToScale.localScale, smallScale))
            StartScale(targetToScale.localScale, largeScale);

        // começa o hold
        _holdCoroutine = StartCoroutine(HoldAndLoad());
    }

    public void OnDeselect()
    {
        if (_sceneLoaded) return;

        // cancela o hold
        if (_holdCoroutine != null)
            StopCoroutine(_holdCoroutine);

        progressImage.fillAmount = 0f;

        // volta à escala “small” se for o caso
        if (onlyWhenSmall)
            StartScale(targetToScale.localScale, smallScale);
    }

    IEnumerator HoldAndLoad()
    {
        float timer = 0f;
        while (timer < holdTime)
        {
            timer += Time.deltaTime;
            progressImage.fillAmount = Mathf.Clamp01(timer / holdTime);
            yield return null;
        }
        _sceneLoaded = true;
        SceneManager.LoadScene(sceneIndex);
    }

    void StartScale(Vector3 from, Vector3 to)
    {
        if (_scaleCoroutine != null)
            StopCoroutine(_scaleCoroutine);
        _scaleCoroutine = StartCoroutine(ScaleOverTime(from, to, scaleDuration));
    }

    IEnumerator ScaleOverTime(Vector3 from, Vector3 to, float duration)
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

    bool NearlyEqual(Vector3 a, Vector3 b, float eps = 0.01f)
        => Vector3.SqrMagnitude(a - b) <= eps * eps;
}
