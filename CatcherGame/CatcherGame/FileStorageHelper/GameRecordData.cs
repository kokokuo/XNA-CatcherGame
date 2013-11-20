using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization; 
using CatcherGame.GameObjects;

namespace CatcherGame.FileStorageHelper
{
    public class GameRecordData
    {
       
        public int HistoryTopSavedNumber { get; set; }

        List<DropObjectsKeyEnum> caughtObjects;


        public List<DropObjectsKeyEnum> CaughtDropObjects { get; set; }


        public int CurrentSavePeopleNumber { get; set; }
    }
}
