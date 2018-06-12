using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 MoveBy;
    public float Speed = 0.5f;
    public float Pause = 0.5f;
    
    private Vector3 _pointA;
    private Vector3 _pointB;
    private bool _goingToA;
    private float _timeToWait;

    // Use this for initialization
    private void Start() {
        _pointA = transform.position;
        _pointB = _pointA + MoveBy;
        _goingToA = false;
    }

    private void Update() {
        Vector3 myPos = transform.position;
        Vector3 target = _goingToA ? _pointA : _pointB;

        if (isArrived(myPos, target)) {
            _timeToWait = Pause;
            _goingToA = !_goingToA;
        } else {
            _timeToWait -= Time.deltaTime;
            if (_timeToWait <= 0) {
                myPos = Vector3.MoveTowards(myPos, target, Speed);
            }
        }

        transform.position = myPos;
    }
    
    private bool isArrived(Vector3 pos, Vector3 target) {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.02f;
    }
}