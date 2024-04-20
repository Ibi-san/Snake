using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    public float Speed => _speed;

    [SerializeField] private int _playerLayer = 3;
    [SerializeField] private Tail _tailPrefab;
    [field: SerializeField] public Transform Head { get; private set; }
    [SerializeField] private float _speed = 2f;
    [SerializeField] private List<MeshRenderer> _meshes;
    private Tail _tail;
    [SerializeField] private Text _login;
    
    public void Init(int detailCount, float hue, bool isPlayer = false)
    {
        if (isPlayer)
        {
            gameObject.layer = _playerLayer;
            var childrens = GetComponentsInChildren<Transform>();

            foreach (var child in childrens)
            {
                child.gameObject.layer = _playerLayer;
            }
        }
        
        SetColor(hue);
        _tail = Instantiate(_tailPrefab, transform.position, Quaternion.identity);
        _tail.SetColor(hue);
        _tail.Init(Head, _speed, detailCount, _playerLayer, isPlayer);
    }

    public void SetDetailCount(int detailCount) => _tail.SetDetailCount(detailCount);

    public void SetRotation(Vector3 pointToLook) => Head.LookAt(pointToLook);

    private void SetColor(float hue)
    {
        Color.RGBToHSV(_meshes[0].material.color, out float h,out float s, out float v);
        Color newColor = Color.HSVToRGB(hue, s, v);
        foreach (var mesh in _meshes)
        {
            mesh.material.color = newColor;
        }
    }

    public void SetLogin(string login) => _login.text = login;

    private void Update() => Move();

    private void Move() => transform.position += Head.forward * (Time.deltaTime * _speed);

    public void Destroy(string clientID)
    {
        var detailsPositions = _tail.GetDetailsPositions();
        detailsPositions.id = clientID;
        string json = JsonUtility.ToJson(detailsPositions);
        MultiplayerManager.Instance.SendMessage("gameOver", json);
        _tail.Destroy();
        Destroy(gameObject);
    }
}