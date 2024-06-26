using System.Collections.Generic;
using Colyseus.Schema;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float _cameraOffsetY = 15f;
    [SerializeField] private Transform _cursor;
    private MultiplayerManager _multiplayerManager;
    private PlayerAim _playerAim;
    private string _clientID;
    private Player _player;
    private Snake _snake;
    private Camera _camera;
    private Plane _plane;
    
    public void Init(string clientID, PlayerAim aim, Player player, Snake snake)
    {
        _clientID = clientID;
        _multiplayerManager = MultiplayerManager.Instance;
        _player = player;
        _playerAim = aim;
        _snake = snake;
        _camera = Camera.main;
        _plane = new Plane(Vector3.up, Vector3.zero);
        
        _camera.transform.parent = _snake.transform;
        _camera.transform.localPosition = Vector3.up * _cameraOffsetY;

        _player.OnChange += OnChange;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveCursor();    
            _playerAim.SetTargetDirection(_cursor.position);
        }

        SendMove();
    }
    
    private void OnChange(List<DataChange> changes)
    {
        Vector3 position = _snake.transform.position;
        for (int i = 0; i < changes.Count; i++)
        {
            switch (changes[i].Field)
            {
                case "x":
                    position.x = (float)changes[i].Value;
                    break;
                case "z":
                    position.z = (float)changes[i].Value;
                    break;
                case "d":
                    _snake.SetDetailCount((byte)changes[i].Value);
                    break;
                case "score":
                    _multiplayerManager.UpdateScore(_clientID, (ushort)changes[i].Value);
                    break;
                default:
                    Debug.LogWarning("Non handled filed change " + changes[i].Field);
                    break;
            }
        }
        
        _snake.SetRotation(position);
    }

    private void SendMove()
    {
        _playerAim.GetMoveInfo(out Vector3 position);

        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            {"x", position.x},
            {"z", position.z}
        };
        
        _multiplayerManager.SendMessage("move", data);
    }

    private void MoveCursor()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        _plane.Raycast(ray, out float distance);
        Vector3 point = ray.GetPoint(distance);
        _cursor.position = point;
    }

    public void Destroy()
    {
        _camera.transform.parent = null;
        _player.OnChange -= OnChange;
        _snake.Destroy(_clientID);
        Destroy(gameObject);
    }
}
