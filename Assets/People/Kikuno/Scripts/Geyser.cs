using UnityEngine;

public class Geyser : GimmickBase
{
    [SerializeField] private float _waterStartTime = 1f;
    [SerializeField] private float _waterKeepTime;
    [SerializeField] private float _force = 10f;
    [SerializeField] private int _maxSizeCount;
    private Rigidbody2D rb; //SerialzeField は　Unity内に項目を追加し、変更しやすくする
    private float _time = 0;
    private float _keepTime;
    private Vector2 _startOffset;
    private BoxCollider2D _boxCol;
    private Vector2 _startBoxColSize;
    ParticleSystem _ps;
    ParticleSystemRenderer _renderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _boxCol = GetComponent<BoxCollider2D>();
        _ps = GetComponent<ParticleSystem>();
        _renderer = GetComponent<ParticleSystemRenderer>();
        _ps.Stop();
        _startBoxColSize = _boxCol.size;
        _startOffset = _boxCol.offset;
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;

        if (_time > _waterStartTime)
        {
            _keepTime += Time.deltaTime;
            if (_keepTime < _waterKeepTime)
            {
                _ps.Play();
                Bounds bounds = _renderer.bounds;
                Vector3 size = bounds.size;
                _boxCol.size = new Vector2(_startBoxColSize.x, size.y);
                _boxCol.offset = new Vector2(_startOffset.x, _startOffset.y + size.y * 0.5f);
            }
            else
            {
                _ps.Stop();
                _boxCol.size = _startBoxColSize;
                _boxCol.offset = _startOffset;
                _time = 0f;
                _keepTime = 0f;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb = collision.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * _force, ForceMode2D.Impulse);
    }
}
