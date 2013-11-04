using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CatcherGame.GameObjects;
using CatcherGame.TextureManager;
using System.Diagnostics;
namespace CatcherGame
{
    public delegate void DropObjsTimeUpEventHandler(List<DropObjects> objs);
    public class RandGenerateDropObjsSystem
    {
        /// <summary>
        /// 產生角色的事件
        /// </summary>
        public event DropObjsTimeUpEventHandler GenerateDropObjs;

        List<DropObjectDataRecord> GeneraterReocrd; 
        List<TexturesKeyEnum> loadTexureKeys;
        List<DropObjects> generatedDropObjs;
        int maxGenerateTimes;
        int creatureMaxGenerateAmount;
        float totaleEapsed;
        int nextGenerateTimes;
        int nextGenerateCreatureAmount;
        float leftBorder, rightBorder;
        /// <summary>
        /// 載入所有掉落角色的資料
        /// </summary>
        /// <param name="generateObjsTimeMaxRange">允許最久可以產生角色的時間(MillionSecond)</param>
        /// <param name="maxCreatureAmount">最大可以產生的生物角色數量</param>
        public RandGenerateDropObjsSystem(float leftScreenBorder,float rightScreenBorder,int generateObjsTimeMaxRange, int maxCreatureAmount)
        {
            this.maxGenerateTimes = generateObjsTimeMaxRange;
            this.creatureMaxGenerateAmount = maxCreatureAmount;
            leftBorder = leftScreenBorder;
            rightBorder = rightScreenBorder;
            generatedDropObjs = new List<DropObjects>();
            LoadDropObjsGenerateRecord();
        }
        private void LoadDropObjsGenerateRecord() {
            GeneraterReocrd = new List<DropObjectDataRecord>();
             
            loadTexureKeys = new List<TexturesKeyEnum>();
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_FLYOLDELADY_FALL);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_FLYOLDELADY_CAUGHT);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_FLYOLDELADY_WALK);
            GeneraterReocrd.Add(new CreatureDataRecord(DropObjectsKeyEnum.PERSON_FLY_OLDLADY, 0.8f, loadTexureKeys, 3f, 0f, 1));

            loadTexureKeys = new List<TexturesKeyEnum>();
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_FATDANCE_FALL);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_FATDANCE_CAUGHT);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_FATDANCE_WALK);
            GeneraterReocrd.Add(new CreatureDataRecord(DropObjectsKeyEnum.PERSON_FAT_DANCE, 0.5f, loadTexureKeys, 2f, 0f, 0));

            loadTexureKeys = new List<TexturesKeyEnum>();
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_LITTLEGIRL_FALL);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_LITTLEGIRL_CAUGHT);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_LITTLEGIRL_WALK);
            GeneraterReocrd.Add(new CreatureDataRecord(DropObjectsKeyEnum.PERSON_LITTLE_GIRL, 0.45f, loadTexureKeys, 4f, 0f, 1));


            loadTexureKeys = new List<TexturesKeyEnum>();
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_MANSTUBBLE_FALL);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_MANSTUBBLE_CAUGHT);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_MANSTUBBLE_WALK);
            GeneraterReocrd.Add(new CreatureDataRecord(DropObjectsKeyEnum.PERSON_MAN_STUBBLE, 0.6f, loadTexureKeys, 3f, 0f, 0));

            loadTexureKeys = new List<TexturesKeyEnum>();
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_NAUGHTYBOY_FALL);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_NAUGHTYBOY_CAUGHT);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_NAUGHTYBOY_WALK);
            GeneraterReocrd.Add(new CreatureDataRecord(DropObjectsKeyEnum.PERSON_NAUGHTY_BOY, 0.3f, loadTexureKeys, 4f, 0f, 1));

            loadTexureKeys = new List<TexturesKeyEnum>();
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_OLDMAN_FALL);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_OLDMAN_CAUGHT);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_OLDMAN_WALK);
            GeneraterReocrd.Add(new CreatureDataRecord(DropObjectsKeyEnum.PERSON_NAUGHTY_BOY, 0.5f, loadTexureKeys, 3f, 0f, 1));

            loadTexureKeys = new List<TexturesKeyEnum>();
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_ROXANNE_FALL);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_ROXANNE_CAUGHT);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_ROXANNE_WALK);
            GeneraterReocrd.Add(new CreatureDataRecord(DropObjectsKeyEnum.PERSON_ROXANNE, 0.3f, loadTexureKeys, 3f, 0f, 1));
        }

       
        //第一次產生
        public void FirstWorkRandom() { 
            Random NextDropTimes = new Random(Guid.NewGuid().GetHashCode());
            Random DropObjAmount = new Random(Guid.NewGuid().GetHashCode());
            nextGenerateTimes =  NextDropTimes.Next(1000, maxGenerateTimes);
            nextGenerateCreatureAmount =  DropObjAmount.Next(1, creatureMaxGenerateAmount);
            //清除之前殘留指向的位置
            generatedDropObjs.Clear();

            while (generatedDropObjs.Count == nextGenerateCreatureAmount) { 
                //加入人數產生與位置
            
            }
        }
        /// <summary>
        /// 更新現在的時間(需要)
        /// </summary>
        /// <param name="gameTimeSpan"></param>
        public void UpdateTime(TimeSpan gameTimeSpan) {
            totaleEapsed += gameTimeSpan.Milliseconds;
            if (totaleEapsed > nextGenerateTimes)
            {
                totaleEapsed -= nextGenerateTimes;
                Debug.WriteLine("Time Up!");
                if (GenerateDropObjs != null) { 
                    
                }
            }
        
        }
    }
}
