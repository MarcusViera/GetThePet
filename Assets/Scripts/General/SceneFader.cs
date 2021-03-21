using UnityEngine;
using UnityEngine.UI;

public enum FadeState
{
    FadeIn,
    FadeOut
};

/*! \class SceneFader
 *  \brief Controls the scene transition fade effect.
 */
public class SceneFader : MonoBehaviour {	
	
	[SerializeField]private float fadeSpeed;				        //!< Time speed of the fade.	    
    [SerializeField]private Image fadeImage;                        //!< Image of fade effect.
    private FadeState fadeState;    	                            //!< Check if the image needs to fade in or out.

    private float fadeTimer;                                        //!< Counts the fade time.

    public AnimationCurve fadeCurve;                                //!< Curve determining the fade time behavior.  

    // Callback for fade end.
    public delegate void Callback();
    Callback faderCallback;   
    	
	// Update is called once per frame
	void Update () {		
		FadeTransition();		
	}

	/// Interpolates the color of the image to fade it in or out.
	private void FadeTransition () {   
        float fadeValue = 0f;
        fadeTimer += Time.deltaTime;
        float percentageComplete = fadeTimer / fadeSpeed;

        if (fadeState == FadeState.FadeOut)
		{
            fadeValue = Mathf.Lerp(1f, 0f, fadeCurve.Evaluate(percentageComplete));            
		}		
		else if (fadeState == FadeState.FadeIn)
		{
            fadeValue = Mathf.Lerp(0f, 1f, fadeCurve.Evaluate(percentageComplete));           
        }

        Color actualColor = fadeImage.color;
        actualColor.a = fadeValue;
        fadeImage.color = actualColor;        

        if (percentageComplete >= 1f)
        {
            if (fadeState == FadeState.FadeOut)
            {
                fadeImage.enabled = false;
            }   
                     
            enabled = false;
            
            if (faderCallback != null)
            {
                faderCallback();
            }                
        }
    }

	/// <summary>
	/// Starts to fade the scene accordingly to the option selected (in or out).
	/// </summary>
	public void FadeScene (FadeState fadeOption, Callback callback = null) {        
        fadeState = fadeOption;
        fadeTimer = 0f;	
        faderCallback = callback;
        fadeImage.enabled = true;

        enabled = true;
	}
}
