using JupiterX;
using JupiterX.Classes;
using JupiterX.Menu;
using JupiterX.Mods;
using UnityEngine;
using MelonLoader;

[assembly: MelonInfo(typeof(JupiterXPlugin.JupiterPlugin), "JupiterX PLugin", "1.0.0", "YourName")] // Change this to your plugin name and your name
[assembly: MelonGame(null, null)]

namespace JupiterXPlugin
{
    public class JupiterPlugin : MelonMod // How to use, Build this then go to your Game Files then go to JupiterX/Plugins folder and put the dll there and it will load
    {
        private int catergoryIndex = -1;
        private string catergoryName = "Extra Movement"; // Change this to like "Extra Movement" or what ever you want your plugin to be

        public override void OnInitializeMelon()
        {
            Buttons.AddCategory(catergoryName);
            Buttons.AddButton(Buttons.GetCategory("Main"), new ButtonInfo
            {
                buttonText = catergoryName,
                method = () =>
                {
                    Buttons.CurrentCategoryName = catergoryName;
                },
                isTogglable = false
            });
            Buttons.RemoveButton(Buttons.GetCategory(catergoryName), "Accept Prompt"); // DO NOT REMOVE because this is dumb and im lazy so dont remove unless you wanna see this on your plugin
            Buttons.RemoveButton(Buttons.GetCategory(catergoryName), "Decline Prompt"); // DO NOT REMOVE because this is dumb and im lazy so dont remove unless you wanna see this on your plugin
            Buttons.AddButtons(Buttons.GetCategory(catergoryName), new ButtonInfo[]
            {
                new ButtonInfo { buttonText = "Back", method = () => Buttons.CurrentCategoryName = "Main", isTogglable = false },
                new ButtonInfo { buttonText = "Fly", method = () => RightTriggerFly(), isTogglable = true }
            });
        }

        private static void RightTriggerFly()
        {
            if (Utility.RTrigger)
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaTagger.Instance.headCollider.transform.forward * Time.deltaTime * Movement.FlySpeed;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }


        public override void OnUpdate()
        {
            // You can put something here if you want it to run every frame without a toggle
        }

        public void OnUnload() // Unloads the plugin
        {
            if (Buttons.CurrentCategoryIndex == catergoryIndex)
                Buttons.CurrentCategoryIndex = 0;
            Buttons.RemoveCategory(catergoryName);
            Buttons.RemoveButton(Buttons.GetCategory("Main"), "Open Plugin");
            Utility.Log("[Plugin] Unloading {}.");
        }
    }
}