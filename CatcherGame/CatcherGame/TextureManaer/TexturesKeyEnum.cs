using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatcherGame.TextureManager
{
    /// <summary>
    /// 使用enum取key的string型態
    /// </summary>
    public enum TexturesKeyEnum
    {
        TEST = 0,

        MENU_BACKGROUND,
        MENU_SIDE,
        MENU_DICTIONARY_BUTTON,
        MENU_PLAY_BUTTON,
        MENU_HOW_TO_PLAY_BUTTON,
        MENU_TOP_SCORE_BUTTON,

        TOP_SCORE_DIALOG_BACK,
        DIALOG_CLOSE_BUTTON,

        PLAY_BACKGROUND,
        PLAY_FIREMAN,
        PLAY_LEFT_MOVE_BUTTON,
        PLAY_RIGHT_MOVE_BUTTON,
        PLAY_PAUSE_BUTTON,
        PLAY_SMOKE,
        PLAY_LIFE,
        PLAY_SCORE,
        PLAY_NET,

        PAUSE_DIALOG_BACK,
        PAUSE_EXIT,
        PAUSE_CONTINUE,

        //DICTIONARY---------------------
        DICTIONARY_BACKGROUND,
        DICTIONARY_LEFT_BUTTON,
        DICTIONARY_RIGHT_BUTOTN,
        DICTIONARY_CONTENT_TEXTURE,
        DICTIONARY_PICTURE_TEXTURE
    }
}
