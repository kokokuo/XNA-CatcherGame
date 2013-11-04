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
using CatcherGame.GameStates;
namespace CatcherGame
{
    public delegate void DropObjsTimeUpEventHandler(List<DropObjects> objs);
    public class RandGenerateDropObjsSystem
    {
        /// <summary>
        /// 產生角色的事件
        /// </summary>
        public event DropObjsTimeUpEventHandler GenerateDropObjs;

        List<DropObjectDataRecord> generaterReocrd; 
        List<TexturesKeyEnum> loadTexureKeys;
        List<DropObjects> generatedDropObjs;
        int maxGenerateTimes;
        int creatureMaxGenerateAmount;
        float totaleEapsed;
        int nextGenerateTimes;
        int nextGenerateCreatureAmount;
        float leftBorder, rightBorder;
        const int TEXTURE_MAX_WIDTH = 50;
        const int MAX_HIGH_POS_Y = 70;
        GameState currentState;
        /// <summary>
        /// 載入所有掉落角色的資料
        /// </summary>
        /// <param name="generateObjsTimeMaxRange">允許最久可以產生角色的時間(MillionSecond)</param>
        /// <param name="maxCreatureAmount">最大可以產生的生物角色數量</param>
        public RandGenerateDropObjsSystem(GameState currentState ,float leftScreenBorder,float rightScreenBorder,int generateObjsTimeMaxRange, int maxCreatureAmount)
        {
            this.maxGenerateTimes = generateObjsTimeMaxRange;
            this.creatureMaxGenerateAmount = maxCreatureAmount;
            leftBorder = leftScreenBorder;
            rightBorder = rightScreenBorder;
            generatedDropObjs = new List<DropObjects>();
            LoadDropObjsGenerateRecord();
            this.currentState = currentState;
        }
        private void LoadDropObjsGenerateRecord() {
            generaterReocrd = new List<DropObjectDataRecord>();
             
            loadTexureKeys = new List<TexturesKeyEnum>();
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_FLYOLDELADY_FALL);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_FLYOLDELADY_CAUGHT);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_FLYOLDELADY_WALK);
            generaterReocrd.Add(new CreatureDataRecord(DropObjectsKeyEnum.PERSON_FLY_OLDLADY, 0.8f, loadTexureKeys, 3f, 0f, 1));

            loadTexureKeys = new List<TexturesKeyEnum>();
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_FATDANCE_FALL);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_FATDANCE_CAUGHT);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_FATDANCE_WALK);
            generaterReocrd.Add(new CreatureDataRecord(DropObjectsKeyEnum.PERSON_FAT_DANCE, 0.5f, loadTexureKeys, 2f, 0f, 0));

            loadTexureKeys = new List<TexturesKeyEnum>();
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_LITTLEGIRL_FALL);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_LITTLEGIRL_CAUGHT);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_LITTLEGIRL_WALK);
            generaterReocrd.Add(new CreatureDataRecord(DropObjectsKeyEnum.PERSON_LITTLE_GIRL, 0.45f, loadTexureKeys, 4f, 0f, 1));


            loadTexureKeys = new List<TexturesKeyEnum>();
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_MANSTUBBLE_FALL);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_MANSTUBBLE_CAUGHT);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_MANSTUBBLE_WALK);
            generaterReocrd.Add(new CreatureDataRecord(DropObjectsKeyEnum.PERSON_MAN_STUBBLE, 0.6f, loadTexureKeys, 3f, 0f, 0));

            loadTexureKeys = new List<TexturesKeyEnum>();
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_NAUGHTYBOY_FALL);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_NAUGHTYBOY_CAUGHT);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_NAUGHTYBOY_WALK);
            generaterReocrd.Add(new CreatureDataRecord(DropObjectsKeyEnum.PERSON_NAUGHTY_BOY, 0.3f, loadTexureKeys, 4f, 0f, 1));

            loadTexureKeys = new List<TexturesKeyEnum>();
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_OLDMAN_FALL);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_OLDMAN_CAUGHT);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_OLDMAN_WALK);
            generaterReocrd.Add(new CreatureDataRecord(DropObjectsKeyEnum.PERSON_NAUGHTY_BOY, 0.5f, loadTexureKeys, 3f, 0f, 1));

            loadTexureKeys = new List<TexturesKeyEnum>();
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_ROXANNE_FALL);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_ROXANNE_CAUGHT);
            loadTexureKeys.Add(TexturesKeyEnum.PLAY_ROXANNE_WALK);
            generaterReocrd.Add(new CreatureDataRecord(DropObjectsKeyEnum.PERSON_ROXANNE, 0.3f, loadTexureKeys, 3f, 0f, 1));
        }

       
        //第一次產生
        public void FirstWorkRandom() { 
            Random nextDropTimes = new Random(Guid.NewGuid().GetHashCode());
            Random dropObjAmount = new Random(Guid.NewGuid().GetHashCode());
            nextGenerateTimes =  nextDropTimes.Next(1000, maxGenerateTimes);
            nextGenerateCreatureAmount =  dropObjAmount.Next(1, creatureMaxGenerateAmount);
            //清除之前殘留指向的位置
            generatedDropObjs.Clear();
            

            //紀錄每個Creature實際產生的機率值
            Dictionary<DropObjectsKeyEnum,float> creaturesProbability;
            while (generatedDropObjs.Count == nextGenerateCreatureAmount) {
                Random fallPositionX = new Random(Guid.NewGuid().GetHashCode());
                Random fallPositionY = new Random(Guid.NewGuid().GetHashCode());
                float  x = fallPositionX.Next((int)leftBorder, (int)rightBorder - TEXTURE_MAX_WIDTH);
                float y = fallPositionY.Next(MAX_HIGH_POS_Y);
                if(y != 0)
                    y = -y;
                //產生亂數位置位置
                Vector2 startFallPos = new Vector2(x, y);
                //紀錄有在符合機率的所有Creature
                creaturesProbability = new Dictionary<DropObjectsKeyEnum,float>();
                //計算每個角色遊戲產生出的機率
                foreach (DropObjectDataRecord record in generaterReocrd) {
                    if (record is CreatureDataRecord) { 
                        Random generateProbability = new Random(Guid.NewGuid().GetHashCode());
                        float p =  generateProbability.Next(100)/100f;
                        //如果有在設定的機率值以下(表示有擲出)
                        if (p <= record.Probability){
                            //從滿足產生機率的角色中找出最優先可以產生(實際產生的機率 離設定的機率值差距最大)
                            float probabiltyDiff = record.Probability - p;
                            creaturesProbability.Add(record.DropObjectKey, probabiltyDiff);
                        }
                    }
                }
                
                //從滿足產生機率的角色中找出最優先可以產生(實際產生的機率 離設定的機率值差距最大)
                var max = from z in creaturesProbability where z.Value == creaturesProbability.Max(v => v.Value) select z.Key;
                //加入至陣列
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
