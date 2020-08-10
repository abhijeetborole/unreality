// http://wiki.unity3d.com/index.php?title=FramesPerSecond

/* **************************************************************************
 * FPS COUNTER
 * **************************************************************************
 * Written by: Annop "Nargus" Prapasapong
 * Created: 7 June 2012
 * *************************************************************************/
 
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
/* **************************************************************************
 * CLASS: FPS COUNTER
 * *************************************************************************/ 
[RequireComponent(typeof(Text))]
public class FPSCounter : MonoBehaviour {
	/* Public Variables */
	public float frequency = 0.5f;
 
	/* **********************************************************************
	 * PROPERTIES
	 * *********************************************************************/
	public int FramesPerSec { get; protected set; }
 
	/* **********************************************************************
	 * EVENT HANDLERS
	 * *********************************************************************/
	/*
	 * EVENT: Start
	 */
	private void Start() {
		StartCoroutine(FPS());
	}
 
	/*
	 * EVENT: FPS
	 */
	private IEnumerator FPS() {
		for(;;){
			// Capture frame-per-second
			int lastFrameCount = Time.frameCount;
			float lastTime = Time.realtimeSinceStartup;
			yield return new WaitForSeconds(frequency);
			float timeSpan = Time.realtimeSinceStartup - lastTime;
			int frameCount = Time.frameCount - lastFrameCount;
 
			// Display it
			FramesPerSec = Mathf.RoundToInt(frameCount / timeSpan);
			gameObject.GetComponent<Text>().text = FramesPerSec.ToString() + " fps";
		}
	}
}