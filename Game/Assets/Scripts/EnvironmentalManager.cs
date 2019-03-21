using UnityEngine;

public class EnvironmentalManager : MonoBehaviour {
    public int Hours;
    public int Minutes;

    public int TimeCounterMax = 2;
    private float _timeCounter;

    private void Awake() {
        _timeCounter = TimeCounterMax;
    }

    private void Update() {
        if (_timeCounter > 0) {
            _timeCounter -= Time.deltaTime;
        }
        else {
            if (Minutes >= 60) {
                Minutes = 0;
                Hours++;
            }
            else {
                Minutes++;
            }
            
            _timeCounter = TimeCounterMax;
        }
    }
}