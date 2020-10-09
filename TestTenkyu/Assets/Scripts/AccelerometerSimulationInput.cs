using UnityEngine;
public class AccelerometerSimulationInput : Singleton<AccelerometerSimulationInput>
{
    private Vector2 _direction;
    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            var x = Input.GetAxis("Mouse X");
            var y = Input.GetAxis("Mouse Y");
            _direction = new Vector2(x, y);
        }
        else if (Input.GetMouseButtonUp(1)) 
        {
            _direction = Vector2.zero;
        }
    }
    public Vector2 GetDirection()
    {
        return _direction;
    }
}
