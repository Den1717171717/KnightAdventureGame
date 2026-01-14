using UnityEngine;

public class PickupPopUp : MonoBehaviour
{
    [SerializeField] private float popHeight = 0.25f;
    [SerializeField] private float popTime = 0.15f;

    private Vector3 _startPos;
    private float _timer;
    private bool _popping;

    private void Start()
    {
        _startPos = transform.position;
        _popping = true;
    }

    private void Update()
    {
        if (!_popping) return;

        _timer += Time.deltaTime;
        float t = _timer / popTime;

        if (t >= 1f)
        {
            transform.position = _startPos;
            _popping = false;
            return;
        }

        // ease out (швидко вгору, повільно назад)
        float yOffset = Mathf.Sin(t * Mathf.PI) * popHeight;
        transform.position = _startPos + Vector3.up * yOffset;
    }
}