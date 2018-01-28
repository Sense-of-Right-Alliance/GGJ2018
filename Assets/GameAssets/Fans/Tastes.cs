using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class Tastes
{
    public float FluteDesire { get; set; }
    public float BassDesire { get; set; }
    public float DrumDesire { get; set; }
    public float TrumpetDesire { get; set; }

    public Tastes(GameSettings.FAN_TYPE fanType)
    {
        switch (fanType)
        {
            case GameSettings.FAN_TYPE.FLUTE:
                FluteDesire = 1;
                break;
            case GameSettings.FAN_TYPE.BASS:
                BassDesire = 1;
                break;
            case GameSettings.FAN_TYPE.DRUM:
                DrumDesire = 1;
                break;
            case GameSettings.FAN_TYPE.TRUMPET:
                TrumpetDesire = 1;
                break;
        }
    }
    

    public GameSettings.STAGE PickStage(Dictionary<GameSettings.STAGE, StageManager> stages)
    {
        var scores = stages.Select(s => new KeyValuePair<GameSettings.STAGE, float>(s.Key, CalculateStageScore(s.Value))).Where(s => s.Value > 0);
        if (!scores.Any())
        {
            return GameSettings.STAGE.NONE;
        }
        return scores.OrderBy(s => Random.Range(0, 1)).Select(s => s.Key).First(); // TODO: May have to change this if we have different weights of taste
    }

    private float CalculateStageScore(StageManager stage)
    {
        switch (stage.CurrentInstrument)
        {
            case GameSettings.INSTRUMENT.DRUM:
                return DrumDesire;
            case GameSettings.INSTRUMENT.BASS:
                return BassDesire;
            case GameSettings.INSTRUMENT.TRUMPET:
                return TrumpetDesire;
            case GameSettings.INSTRUMENT.FLUTE:
                return FluteDesire;
            default:
                return 0;
        }
    }
}
