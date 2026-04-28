using JupiterX;
using JupiterX.Classes;
using JupiterX.Menu;
using JupiterX.Mods;
using UnityEngine;
using MelonLoader;

[assembly: MelonInfo(typeof(JupiterXPlugin.JupiterPlugin), "JupiterX Plugin", "1.0.0", "YourName")]
[assembly: MelonGame(null, null)]

namespace JupiterXPlugin
{
    public class JupiterPlugin : MelonMod
    {
        private int categoryIndex = -1;
        private string categoryName = "Extra Movement";

        public override void OnInitializeMelon()
        {
            categoryIndex = Buttons.GetCategory(categoryName);
            if (categoryIndex == -1)
                categoryIndex = Buttons.AddCategory(categoryName);

            Buttons.RemoveButton(Buttons.GetCategory("Main"), categoryName);

            Buttons.AddButton(Buttons.GetCategory("Main"), new ButtonInfo
            {
                buttonText = categoryName,
                method = () => Buttons.CurrentCategoryName = categoryName,
                isTogglable = false
            });

            Buttons.AddButtons(categoryIndex, new ButtonInfo[]
            {
                new ButtonInfo
                {
                    buttonText = $"Exit {categoryName}",
                    method = () => Buttons.CurrentCategoryName = "Main",
                    isTogglable = false
                },
                new ButtonInfo
                {
                    buttonText = "Fly",
                    method = () => RightTriggerFly(),
                    isTogglable = true
                }
            });

            int cat = Buttons.GetCategory(catergoryName);

            Buttons.buttons[cat] = Buttons.buttons[cat]
                .Where(b =>
                    b.buttonText != "Search" &&
                    b.buttonText != "Global Return" &&
                    b.buttonText != "Accept Prompt" &&
                    b.buttonText != "Decline Prompt")
                .ToArray();
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
            // Your code here for updating always
        }

        public void OnUnload()
        {
            if (Buttons.CurrentCategoryIndex == categoryIndex)
                Buttons.CurrentCategoryIndex = Buttons.GetCategory("Main");

            Buttons.RemoveCategory(categoryName);
            Buttons.RemoveButton(Buttons.GetCategory("Main"), categoryName);

            Utility.Log($"Unloading plugin {categoryName}");
        }
    }
}
