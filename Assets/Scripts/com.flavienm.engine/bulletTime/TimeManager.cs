using UnityEngine;

public class TimeManager : MonoBehaviour {

	public float slowDownFactor = 0.05f;
	public float slowDownLength = 2;
	public float fixedDeltaTimeFactor = 0.02f;

	void Update()
	{
		Time.timeScale += (1 / slowDownLength) * Time.unscaledDeltaTime;
		Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
	}

	void DoSlowMotion()
	{
		Time.timeScale = slowDownFactor;
		Time.fixedDeltaTime = Time.timeScale * fixedDeltaTimeFactor;

	}
	
}
