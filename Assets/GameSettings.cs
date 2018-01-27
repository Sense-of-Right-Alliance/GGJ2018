public static class GameSettings {

    public enum DEBUG_STATE {
        RELEASE = 0,
        DEBUG_LITE = 1, // This is a mode that players can activate to provide additional information such as the version number.
        DEBUG_FULL = 2
    }
    
    // Scene Names:
    public const string MAIN_MENU_SCENE = "Init";
    public const string GAME_SCENE = "Rink";

    // Button Names:
    public const string REWIRED_START = "Start";
    public const string REWIRED_MAIN_BUTTON = "MainAction";
    public const string REWIRED_SECONDARY_BTN = "SecondaryAction";
    // Bleh these probably shouldn't be different. Start is used in menu scenes, pause is used in game.
    public const string REWIRED_SYSTEM_START = "MainAction";
    public const string REWIRED_SYSTEM_PAUSE = "SecondaryAction";
    
    public const int MAX_PLAYERS = 4;  // Currently 4, could be eiiiight
    // TODO-DG: would also have to change Rewired to handle 8

    public const int MAX_SKATERS_PER_TEAM = 4;  // Most likely won't change, but who knows

    public static int[] teamOneRewiredIDs = new int[MAX_SKATERS_PER_TEAM] {0, -1, -1, -1};
    public static int[] teamTwoRewiredIDs = new int[MAX_SKATERS_PER_TEAM] {1, -1, -1, -1};
}
