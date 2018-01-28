public static class GameSettings {

    public enum DEBUG_STATE {
        RELEASE = 0,
        DEBUG_LITE = 1, // This is a mode that players can activate to provide additional information such as the version number.
        DEBUG_FULL = 2
    }

    public enum PLAYER_TYPES {
        HUMAN,
        AI,
        NETWORKED
    }

    public enum STAGE {
        TOPLEFT = 0,
        TOPRIGHT = 1,
        BOTTOMRIGHT = 2,
        BOTTOMLEFT = 3,
        NONE
    }

    public enum INSTRUMENT {
        DRUM = 0,
        BASS = 1,
        TRUMPET = 2,
        FLUTE = 3,
        NONE
    }

    public enum FAN_TYPE {
        DRUM = 0,
        BASS = 1,
        TRUMPET = 2,
        FLUTE = 3,
    }

    public const int NUM_INSTRUMENTS = 4;
    
    // Scene Names:
    public const string MAIN_MENU_SCENE = "Init";
    public const string GAME_SCENE = "Game";

    // Button Names:
    public const string REWIRED_START = "Start";
    public const string REWIRED_MAIN_BTN = "MainAction";
    public const string REWIRED_SECONDARY_BTN = "SecondaryAction";
    public const string REWIRED_THIRD_BTN = "ThirdAction";
    public const string REWIRED_FOURTH_BTN = "FourthAction";

    // Bleh these probably shouldn't be different. Start is used in menu scenes, pause is used in game.
    public const string REWIRED_SYSTEM_START = "MainAction";
    
    public const int MAX_PLAYERS = 4;  // Currently 4, could be eiiiight
    // TODO-DG: would also have to change Rewired to handle 8

    public static PLAYER_TYPES[] PlayerTypes = {
        PLAYER_TYPES.HUMAN,
        PLAYER_TYPES.HUMAN,
        PLAYER_TYPES.AI,
        PLAYER_TYPES.AI,
        PLAYER_TYPES.AI,
        PLAYER_TYPES.AI,
        PLAYER_TYPES.AI,
        PLAYER_TYPES.AI
    };
}
