using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using API_3DTI_Common;
using static API_3DTI_HL;

public struct HearingLevels
{
    public int slope;
    public T_HLClassificationScaleCurve hearingLossCurve;
    public T_HLClassificationScaleSeverity hearingLossSeverity;

    public HearingLevels(int newSlope, T_HLClassificationScaleCurve newHearingLossCurve, T_HLClassificationScaleSeverity newHearingLossSeverity)
    {
        slope = newSlope;
        hearingLossCurve = newHearingLossCurve;
        hearingLossSeverity = newHearingLossSeverity;
    }
}

public class AudioManager : MonoBehaviour
{
    API_3DTI_HL audioAPIHearingLoss = null;
    API_3DTI_HA audioAPIAid = null;
    private List<HearingLevels> levelsOfHearing = null;
    private int currentIndex = 0;
    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        audioAPIHearingLoss = Camera.main.GetComponent<API_3DTI_HL>();
        audioAPIAid = Camera.main.GetComponent<API_3DTI_HA>();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioAPIHearingLoss.EnableHearingLossInBothEars(true);
        audioAPIHearingLoss.EnableNonLinearAttenuation(T_ear.BOTH);

        Init();
    }

    private void Init()
    {
        levelsOfHearing = new List<HearingLevels>();

        levelsOfHearing.Add(new HearingLevels(6, T_HLClassificationScaleCurve.HL_CS_A, T_HLClassificationScaleSeverity.HL_CS_SEVERITY_MILDMODERATE));
        levelsOfHearing.Add(new HearingLevels(6, T_HLClassificationScaleCurve.HL_CS_A, T_HLClassificationScaleSeverity.HL_CS_SEVERITY_MILDMODERATE));
        levelsOfHearing.Add(new HearingLevels(6, T_HLClassificationScaleCurve.HL_CS_A, T_HLClassificationScaleSeverity.HL_CS_SEVERITY_MILDMODERATE));
        levelsOfHearing.Add(new HearingLevels(6, T_HLClassificationScaleCurve.HL_CS_A, T_HLClassificationScaleSeverity.HL_CS_SEVERITY_MILDMODERATE));
        levelsOfHearing.Add(new HearingLevels(6, T_HLClassificationScaleCurve.HL_CS_A, T_HLClassificationScaleSeverity.HL_CS_SEVERITY_MILDMODERATE));

        SetAudioHearingLossConfig();
    }

    public void LevelUp()
    {
        if (currentIndex < levelsOfHearing.Count - 1)
        {
            currentIndex++;
        }
        SetAudioHearingLossConfig();
        Debug.Log("Subo de nivel");
    }

    public void LevelDown()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
        }

        SetAudioHearingLossConfig();
        Debug.Log("Bajo de nivel");
    }

    private void SetAudioHearingLossConfig()
    {
        audioAPIHearingLoss.SetAudiometryFromClassificationScale(T_ear.BOTH, levelsOfHearing[currentIndex].hearingLossCurve, levelsOfHearing[currentIndex].slope, levelsOfHearing[currentIndex].hearingLossSeverity);
    }
}