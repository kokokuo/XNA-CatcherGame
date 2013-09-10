using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatcherGame
{
    interface Clickable
    {
        /// <summary>
        /// 檢查是否有點擊到
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        bool IsClick(float x, float y);
    }
}
