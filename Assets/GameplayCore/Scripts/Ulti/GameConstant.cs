using UnityEngine;

public class GameConstant
{
    public const string PLAYERDATA = "PLAYERDATA";
    public class Event
    {
        public const string START_GAME = "START_GAME";
        public const string CREATE_GAME = "CREATE_GAME";
        public const string RESET_MAP = "RESET_MAP";
        public const string CLEAR_MAP = "CLEAR_MAP";
        public const string END_GAME = "END_GAME";
        public const string UPGRADE_UNIT = "UPGRADE_UNIT";
        public const string SELECT_UNIT = "SELECT_UNIT";

        public const string GOLD_CHANGE = "GOLD_CHANGE";
        public const string GEM_CHANGE = "GEM_CHANGE";
    }

    public class GameLayer
    {
        public const int DICE = 3;
        public const int FLOOR = 6;
    }

    public class Ads
    {
        public const string INTER_ADS_CONFIG = "InterstitialConfig";
        public const string INTER_ADS_DEFAULT = "{\"EndLevel\":10,\"DelayTime\":60}";

        public const string APPOPEN_ADS_CONFIG = "AppOpenConfig";
        public const string APPOPEN_ADS_DEFAULT = "{\"OnOff\":false,\"AppOpenMinTime\":300.0,\"MinLv\":5,\"OpenApp_On\":true,\"ResumeApp_On\":true,\"Interval\":60,\"PauseTime\":30.0,\"Max\":50}";
    }
}

public enum Skill
{
    ADDATK,
    DUPLICATE,
    GOLD,
    SPEED
}

public enum UnitRank
{
    COMMON,
    UNCOMMON,
    RARE,
    EPIC,
    LEGENDARY
}

public enum Element
{
    FIRE,
    ICE,
    LIGHT
}