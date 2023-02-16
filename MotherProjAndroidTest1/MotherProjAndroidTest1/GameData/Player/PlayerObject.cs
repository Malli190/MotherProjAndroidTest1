using Game.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.PlayerSystem
{
    class PlayerObject
    {
        SaveManager.MainSaver saver;
        
        public Player playerModel;

        public PlayerObject()
        {
            saver = new SaveManager.MainSaver();

            saver.SetFileName("player.s");
            saver.SetFolderName("Data");

            if (!saver.LoadParam())
            {
                playerModel = new Player();

                string new_id = StaticMethods.GetRandomPlayerID();

                playerModel.id = new_id;
                playerModel.name = "noname";
                playerModel.map_id = 0;

                saver.AddParameter(new SaveParameterModel() 
                { 
                    id = 0, 
                    parameterName = $"player{playerModel.name}", 
                    parameterValue = saver.GetStringByObject(playerModel) 
                });
                saver.SaveParam();
            }
            else
            {
                playerModel = saver.GetObjectByString<Player>(saver.GetParameterById(0).parameterValue);
            }
        }
        public void SetPlayer(Player pl)
        {
            playerModel = pl;

            UpdateSaveData();
        }
        void UpdateSaveData()
        {
            saver.GetParameterById(0).parameterValue = saver.GetStringByObject(playerModel);
            saver.SaveParam();
        }
    }
}
