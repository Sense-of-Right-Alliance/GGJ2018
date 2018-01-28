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

    private Tastes(float fluteDesire, float bassDesire, float drumDesire, float trumpetDesire)
    {
        FluteDesire = fluteDesire;
        BassDesire = bassDesire;
        DrumDesire = drumDesire;
        TrumpetDesire = trumpetDesire;
    }

    public Tastes GetFluteTastes() { return new Tastes(1, 0, 0, 0); }
    public Tastes GetBassTastes() { return new Tastes(0, 1, 0, 0); }
    public Tastes GetDrumTastes() { return new Tastes(0, 0, 1, 0); }
    public Tastes GetTrumpetTastes() { return new Tastes(0, 0, 0, 1); }

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
            case GameSettings.INSTRUMENT.NONE:
            default:
                return 0;
        }
    }
}
