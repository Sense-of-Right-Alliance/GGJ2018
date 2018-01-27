//using UnityEngine;
public class Localization {

    // LOC_KEY enum refers to the below array, ensure the lines match up.
    // WARNING!! Be careful when adding or removing entries to anything other than the end,
    //  It's possible that editor references will not be updated
    public enum LOC_KEY {
        PRESS_START,
        PLAY_GAME,
        OPTIONS,
        QUIT,
        TEAM_SELECT,
        PAUSE,
        RESUME,
        RESTART
    }

    // LANGUAGE enum correlates to the columns in the below array.
    private enum LANGUAGE {
        ENGLISH
    }
    private static LANGUAGE currentLang = LANGUAGE.ENGLISH;

    // TODO-DG: Move this from code to data.
    private static string[,] LOC_TABLE = new string[,] 
    {
        {"PRESS START"},
        {"Play!"},
        {"Options"},
        {"Quit"},
        {"Press A to Join!"},
        {"PAUSED"},
        {"Resume"},
        {"Restart"}
    };

    public static string GetLocString(LOC_KEY key) {
        return LOC_TABLE[(int)key, (int)currentLang];
    }
}
