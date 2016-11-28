using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
//
using StardewValley.Menus;
using System.Reflection;
using System.Timers;

namespace luckydaymod
{

    class ModConfig
    {
        public bool enable { get; set; } = true;
        public bool debugmode { get; set; } = false;
        public double luckvalue { get; set; } = 0.115;
    }

    public class ModEntry : Mod
    {
        internal static ModConfig Config;
        public string modversion = "0.0.1";
        
        private void HandleDebugHelp(object sender, EventArgsCommand e)
        {
            this.Monitor.Log("=========================================", LogLevel.Info);
            this.Monitor.Log("How to use the luckyday mod version " + modversion , LogLevel.Info);
            this.Monitor.Log("Use config file to enable or disable the mod ", LogLevel.Info);
            this.Monitor.Log("type command currentluck to view your current daily luck amount value", LogLevel.Info);
            this.Monitor.Log("=========================================", LogLevel.Info);
        }

        private void HandleDebugValuePrinter(object sender, EventArgsCommand e)
        {
            this.Monitor.Log("Current daily luck : " + Game1.dailyLuck.ToString(), LogLevel.Info);
        }

        private void ReceiveKeyPress(object sender, EventArgsKeyPressed e)
        {
            if (e.KeyPressed == Keys.NumPad7 && Config.debugmode)
            {
                this.Monitor.Log("Current daily luck : " + Game1.dailyLuck.ToString() , LogLevel.Info);
            }
            if (e.KeyPressed == Keys.NumPad8 && Config.debugmode)
            {
                Game1.dailyLuck = 0.999;
            }
            if (e.KeyPressed == Keys.NumPad3)
            {
                //Learn all professions
                int i = 0;
                while (i < 29)//Profession from 0 to 28
                {
                    Game1.player.professions.Add(i);
                    this.Monitor.Log("Profession No. " + i + " learned !", LogLevel.Info);
                    i++;
                }

            }



        }

        private void GameEvents_OneSecondTick(object sender, EventArgs e)//TICK
        {
            if (Config.enable)
            {
                Game1.dailyLuck = Config.luckvalue;
                
            }

            if(Config.debugmode)
            {
                this.Monitor.Log("Current daily luck : " + Game1.dailyLuck.ToString(), LogLevel.Info);
            }
        }

        public override void Entry(IModHelper helper)
        {
            Command.RegisterCommand("help_luckydaymod", "Shows luckyday mod  infos | uckyday mod").CommandFired += this.HandleDebugHelp;
            Command.RegisterCommand("currentluck", "Shows your current daily luck value").CommandFired += this.HandleDebugValuePrinter;
            GameEvents.OneSecondTick += this.GameEvents_OneSecondTick;
            Config = helper.ReadConfig<ModConfig>();
            ControlEvents.KeyPressed += this.ReceiveKeyPress;
        }
    }
}
