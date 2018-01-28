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
        var bestStages = stages.GroupBy(s => CalculateStageScore(s.Value)) // group stages by score
                               .Where(g => g.Key > 0) // where score is above 0
                               .OrderByDescending(g => g.Key) // order by best score
                               .Select(g => g.AsEnumerable()) // select the stages
                               .FirstOrDefault(); // take best group

        if (bestStages == null) // all stages scored 0
        {
            return GameSettings.STAGE.NONE;
        }

        return bestStages.OrderBy(kvp => Random.Range(0, 1)) // order the stages randomly
                         .Select(kvp => kvp.Key) // select the enum
                         .First(); // take the first one
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
