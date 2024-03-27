using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSave
{
    public static int PlayerGod
    {
        get => PlayerPrefs.GetInt(GameSaveKey.PLAYER_GOD, 0);
        set
        {
            PlayerPrefs.SetInt(GameSaveKey.PLAYER_GOD, value);
            PlayerPrefs.Save();
        }
    }


    public static int PlayerLevel
    {
        get => PlayerPrefs.GetInt(GameSaveKey.KEY_LEVEL, 0);
        set
        {
            PlayerPrefs.SetInt(GameSaveKey.KEY_LEVEL, value);
            PlayerPrefs.Save();
        }
    }




    public static string DateSpin
    {
        get => PlayerPrefs.GetString(GameSaveKey.KEY_DATE_SPIN, "");
        set
        {
            PlayerPrefs.SetString(GameSaveKey.KEY_DATE_SPIN, value);
            PlayerPrefs.Save();
        }
    }

    public static string DateSpinReward
    {
        get => PlayerPrefs.GetString(GameSaveKey.KEY_DATE_REWARD, "");
        set
        {
            PlayerPrefs.SetString(GameSaveKey.KEY_DATE_REWARD, value);
            PlayerPrefs.Save();
        }
    }

    public static string DateSubscriptionGem
    {
        get => PlayerPrefs.GetString(GameSaveKey.KEY_DATE_SUBSCRIPTION_GEM, "");
        set
        {
            PlayerPrefs.SetString(GameSaveKey.KEY_DATE_SUBSCRIPTION_GEM, value);
            PlayerPrefs.Save();
        }
    }

    public static ulong PlayerCoin
    {
        get =>
#if DEV
return 99999999999;
#endif
            Convert.ToUInt64(PlayerPrefs.GetString(GameSaveKey.KEY_COIN, "0"));
        set
        {

            PlayerPrefs.SetString(GameSaveKey.KEY_COIN, value.ToString());
            OfflineTime = DateTime.Now.ToBinary().ToString();
            PlayerPrefs.Save();
        }
    }


    public static bool Music
    {
        get => PlayerPrefsX.GetBool(GameSaveKey.KEY_MUSIC, true);
        set
        {
            PlayerPrefsX.SetBool(GameSaveKey.KEY_MUSIC, value);
            PlayerPrefs.Save();
        }
    }

    public static bool Sound
    {
        get => PlayerPrefsX.GetBool(GameSaveKey.KEY_SOUND, true);
        set
        {
            PlayerPrefsX.SetBool(GameSaveKey.KEY_SOUND, value);
            PlayerPrefs.Save();
        }
    }

    public static bool Vibra
    {
        get => PlayerPrefsX.GetBool(GameSaveKey.KEY_VIBR, true);
        set
        {
            PlayerPrefsX.SetBool(GameSaveKey.KEY_VIBR, value);
            PlayerPrefs.Save();
        }
    }

    #region LuckyWheel

    public static int NormalSpin
    {
        get => PlayerPrefs.GetInt(GameSaveKey.NORMAL_SPIN, 0);
        set
        {
            PlayerPrefs.SetInt(GameSaveKey.NORMAL_SPIN, value);
            PlayerPrefs.Save();
        }
    }

    public static int VipSpin
    {
        get => PlayerPrefs.GetInt(GameSaveKey.VIP_SPIN, 0);
        set
        {
            PlayerPrefs.SetInt(GameSaveKey.VIP_SPIN, value);
            PlayerPrefs.Save();
        }
    }

    public static int AdsSpin
    {
        get => PlayerPrefs.GetInt(GameSaveKey.ADS_SPIN, 0);
        set
        {
            PlayerPrefs.SetInt(GameSaveKey.ADS_SPIN, value);
            PlayerPrefs.Save();
        }
    }

    public static int GemSpin
    {
        get => PlayerPrefs.GetInt(GameSaveKey.GEM_SPIN, 0);
        set
        {
            PlayerPrefs.SetInt(GameSaveKey.GEM_SPIN, value);
            PlayerPrefs.Save();
        }
    }
    #endregion

    public static int SessionInternCount
    {
        get => PlayerPrefs.GetInt(GameSaveKey.KEY_INTERN_COUNT, 0);
        set
        {
            PlayerPrefs.SetInt(GameSaveKey.KEY_INTERN_COUNT, value);
            PlayerPrefs.Save();
        }
    }
    public static int SessionRewardCount
    {
        get => PlayerPrefs.GetInt(GameSaveKey.KEY_REWARD_COUNT, 0);
        set
        {
            PlayerPrefs.SetInt(GameSaveKey.KEY_REWARD_COUNT, value);
            PlayerPrefs.Save();
        }
    }

    public static int TotalInternCount
    {
        get => PlayerPrefs.GetInt(GameSaveKey.KEY_TOTAL_INTERN_COUNT, 0);
        set
        {
            PlayerPrefs.SetInt(GameSaveKey.KEY_TOTAL_INTERN_COUNT, value);
            PlayerPrefs.Save();
        }
    }
    public static int TotalRewardCount
    {
        get => PlayerPrefs.GetInt(GameSaveKey.KEY_TOTAL_REWARD_COUNT, 0);
        set
        {
            PlayerPrefs.SetInt(GameSaveKey.KEY_TOTAL_REWARD_COUNT, value);
            PlayerPrefs.Save();
        }
    }

    public static int TotalLoseCount
    {
        get => PlayerPrefs.GetInt(GameSaveKey.KEY_TOTAL_LOSE_COUNT, 0);
        set
        {
            PlayerPrefs.SetInt(GameSaveKey.KEY_TOTAL_LOSE_COUNT, value);
            PlayerPrefs.Save();
        }
    }

    public static string OfflineTime
    {
        get => PlayerPrefs.GetString(GameSaveKey.OFFTIME, "");
        set
        {
            PlayerPrefs.SetString(GameSaveKey.OFFTIME, value);
            PlayerPrefs.Save();

        }
    }

    public static string TimeDailyQuest
    {
        get => PlayerPrefs.GetString(GameSaveKey.TimeDailyQuest, "");
        set
        {
            PlayerPrefs.SetString(GameSaveKey.TimeDailyQuest, value);
            PlayerPrefs.Save();
        }
    }

    public static string DailyQuest
    {
        get => PlayerPrefs.GetString(GameSaveKey.DailyQuest, "");
        set
        {
            PlayerPrefs.SetString(GameSaveKey.DailyQuest, value);
            PlayerPrefs.Save();
        }
    }

    public static string DailyQuestClaim
    {
        get => PlayerPrefs.GetString(GameSaveKey.DailyQuestClaim, "");
        set
        {
            PlayerPrefs.SetString(GameSaveKey.DailyQuestClaim, value);
            PlayerPrefs.Save();
        }
    }

    public static bool GiftBox
    {
        get => PlayerPrefsX.GetBool(GameSaveKey.GIFT_BOX, false);
        set
        {
            PlayerPrefsX.SetBool(GameSaveKey.GIFT_BOX, value);
            PlayerPrefs.Save();
        }
    }

    public static string InterRemote
    {
        get => PlayerPrefs.GetString(GameSaveKey.INTER_REMOTE, "");
        set
        {
            PlayerPrefs.SetString(GameSaveKey.INTER_REMOTE, value);
            PlayerPrefs.Save();
        }
    }

    public static string AppOpenRemote
    {
        get => PlayerPrefs.GetString(GameSaveKey.APP_OPEN_REMOTE, "");
        set
        {
            PlayerPrefs.SetString(GameSaveKey.APP_OPEN_REMOTE, value);
            PlayerPrefs.Save();
        }
    }

    public static bool NoAds
    {
        get => PlayerPrefsX.GetBool(GameSaveKey.KEY_NOADS, false);
        set
        {
            PlayerPrefsX.SetBool(GameSaveKey.KEY_NOADS, value);
            PlayerPrefs.Save();
        }
    }


    public static bool VIP
    {
        get => PlayerPrefsX.GetBool(GameSaveKey.KEY_VIP, false);
        set
        {
            PlayerPrefsX.SetBool(GameSaveKey.KEY_VIP, value);
            PlayerPrefs.Save();
        }
    }

    public static bool FirstPurchase
    {
        get => PlayerPrefsX.GetBool(GameSaveKey.KEY_FIRST_PURCHASE, false);
        set
        {
            PlayerPrefsX.SetBool(GameSaveKey.KEY_FIRST_PURCHASE, value);
            PlayerPrefs.Save();
        }
    }

    public static int Tut
    {
        get => PlayerPrefs.GetInt(GameSaveKey.TUTORIAL, -1);
        set
        {
            PlayerPrefs.SetInt(GameSaveKey.TUTORIAL, value);
            PlayerPrefs.Save();
        }
    }

    public static int TypeIAPSubcription
    {
        get => PlayerPrefs.GetInt(GameSaveKey.TypeIAPSubcription, -1);
        set
        {
            PlayerPrefs.SetInt(GameSaveKey.TypeIAPSubcription, value);
            PlayerPrefs.Save();
        }
    }

}

public class GameSaveKey
{
    public const string KEY_NOADS = "KEY_NOADS";
    public const string KEY_VIP = "KEY_VIP";

    public const string KEY_USERNAME = "KEY_USERNAME";
    public const string KEY_PASSWORD = "KEY_PASSWORD";
    public const string PLAYER_GOD = "PLAYER_GOD";
    public const string KEY_LEVEL = "KEY_LEVEL";
    public const string KEY_FAKE_LEVEL = "KEY_FAKE_LEVEL";
    public const string KEY_COIN = "KEY_COIN";

    public const string NORMAL_SPIN = "NORMAL_SPIN";
    public const string VIP_SPIN = "VIP_SPIN";
    public const string ADS_SPIN = "ADS_SPIN";
    public const string GEM_SPIN = "GEM_SPIN";

    public const string INTER_REMOTE = "INTER_REMOTE";
    public const string APP_OPEN_REMOTE = "APP_OPEN_REMOTE";

    public const string KEY_INTERN_COUNT = "KEY_INTERN_COUNT";
    public const string KEY_REWARD_COUNT = "KEY_REWARD_COUNT";

    public const string KEY_TOTAL_LOSE_COUNT = "KEY_TOTAL_LOSE_COUNT";

    public const string KEY_TOTAL_INTERN_COUNT = "KEY_TOTAL_INTERN_COUNT";
    public const string KEY_TOTAL_REWARD_COUNT = "KEY_TOTAL_REWARD_COUNT";

    public const string KEY_DATE_REWARD = "KEY_DATE_REWARD";
    public const string KEY_DATE_SPIN = "KEY_DATE_SPIN";
    public const string KEY_DATE_SUBSCRIPTION_GEM = "KEY_DATE_SUBSCRIPTION_GEM";

    public const string OFFTIME = "OFFTIME";

    public const string GIFT_BOX = "GIFT_BOX";
    public const string Daily = "Dailygift";

    public const string KEY_SOUND = "KEY_SOUND";
    public const string KEY_MUSIC = "KEY_MUSIC";
    public const string KEY_VIBR = "KEY_VIBR";
    public const string KEY_FIRST_PURCHASE = "KEY_FIRST_PURCHASE";

    public const string TUTORIAL = "TUTORIAL";

    public const string TimeDailyQuest = "TimeDailyQuest";
    public const string DailyQuest = "DailyQuest";
    public const string DailyQuestClaim = "DailyQuestClaim";
    public const string TypeIAPSubcription = "TypeIAPSubcription";

}

public enum ResourceType
{
    COIN
}