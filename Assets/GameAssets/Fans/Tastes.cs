using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Tastes
{
    public int FluteDesire { get; set; }
    public int BassDesire { get; set; }
    public int DrumDesire { get; set; }
    public int TrumpetDesire { get; set; }

    public Tastes(GameSettings.FAN_TYPE fanType)
    {
        FluteDesire = 0;
        BassDesire = 0;
        DrumDesire = 0;
        TrumpetDesire = 0;

        switch (fanType)
        {
            case GameSettings.FAN_TYPE.FLUTE:
                FluteDesire = 100;
                break;
            case GameSettings.FAN_TYPE.BASS:
                BassDesire = 100;
                break;
            case GameSettings.FAN_TYPE.DRUM:
                DrumDesire = 100;
                break;
            case GameSettings.FAN_TYPE.TRUMPET:
                TrumpetDesire = 100;
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

        return bestStages.OrderBy(kvp => Random.Range(0, 1000)) // order the stages randomly
                         .Select(kvp => kvp.Key) // select the enum
                         .First(); // take the first one
    }

    private int CalculateStageScore(StageManager stage)
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
