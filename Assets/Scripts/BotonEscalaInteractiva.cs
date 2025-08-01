using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class BotonEscalaInteractiva : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    [Header("Escalas")]
    public Vector3 escalaNormal = Vector3.one;
    public Vector3 escalaHover = Vector3.one * 1.1f;
    public Vector3 escalaPresionado = Vector3.one * 0.95f;

    [Header("Animación")]
    public float duracion = 0.12f; // qué tan rápido interpola
    private RectTransform rt;
    private Coroutine animCorr;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        rt.localScale = escalaNormal;
    }

    void CambiarEscala(Vector3 objetivo)
    {
        if (animCorr != null) StopCoroutine(animCorr);
        animCorr = StartCoroutine(InterpEscala(objetivo));
    }

    System.Collections.IEnumerator InterpEscala(Vector3 objetivo)
    {
        Vector3 inicio = rt.localScale;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / Mathf.Max(0.0001f, duracion);
            rt.localScale = Vector3.Lerp(inicio, objetivo, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
        rt.localScale = objetivo;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CambiarEscala(escalaHover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CambiarEscala(escalaNormal);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CambiarEscala(escalaPresionado);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Si el puntero sigue encima, vuelve a hover; si no, normal
        bool estaEncima = RectTransformUtility.RectangleContainsScreenPoint(
            rt, eventData.position, eventData.pressEventCamera);
        CambiarEscala(estaEncima ? escalaHover : escalaNormal);
    }
}

