using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Base;

namespace TestPlugin
{
    // Class name must be public, contain Plugin word, must inherit IDevice interface, can't be abstract
    public class MyPlugin : IDevice
    {
        public MyPlugin()
        {
            //string value = Base.Settings.GetStartupEntry("TEST");
            //Base.Settings.SetStartupEntry("TEST", "2", true);
            // Change DMC main form title
            //DMC.Form1.main.Text += " with my plugin";
            //Base.View.use_VBO = false;
            //TakeImages.ShowForm();
            //MyPoly.AddCommandToCommandList();
            //PopupTools.Create();
            Settings.AddRC(new RC_IMPORT());
            Settings.AddRC(new RC_IS_CONNECED());
            Settings.AddRC(new RC_PICK_AND_PLACE());
            

            //Base.State.StateChangedEvent += State_StateChangedEvent;
            DMC.Actions.ShowRunError = false;

            Base.State.StateChangedEvent += State_StateChangedEvent1;

            //var it = new ViewButton((Bitmap)Bitmap.FromFile(@"C:\VisionLT\Joystick.png"));
            //Base.View.CurrentView.AddToBase(it, ObjectDrawingOrder.END);
        }

        private void State_StateChangedEvent1(StateType new_state)
        {
            if (new_state == StateType.RunningStoppedWithError)
            {

            }
        }

        private void State_StateChangedEvent(StateType new_state)
        {
            if (new_state == StateType.Compiling)
            {
                //var p = new System.Media.SoundPlayer("D:\\sound.wav");
                //for (int i=0; i<10; i++)
                //p.PlayLooping();
            }
        }

        // Action when user clicks Connect to hardware and IsEnabled is true
        public bool Connect() { 



            return true; 
        }

        // Action when user clicks Disconnect from hardware
        public void Disconnect() { }

        // Action when user clicks Stop button
        public void Stop() { }

        public string GetName() { return "My Plugin"; }

        // Action when changes to settings are confirmed or loaded during DMC startup
        public bool ApplySettings() { return true; }

        // Needs to return if device is connected 
        public bool IsConnected() { return false; }

        // Is device is enabled 
        public bool IsEnabled() { return true; }

        // Called before starting recipe
        public bool OnRecipeStart() 
        {
            /*
            var axis = Base.Settings.StageX.Axis;
            var pos = axis.GetPosition();
            if (!axis.Move(pos + 1, true))
            {
                Functions.ErrorF("Unable to move axis. ");
                Functions.ShowLastError();
            }
            */


            return true; }

        // Called after recipe is finished or stopped
        public void OnRecipeFinish() { }

        // Get device settings
        public IDeviceSettings GetSettings() { return null; }

        // Get error message ( is called if Connect returns false )
        public string GetErrorMessage() { return Base.Functions.GetLastErrorMessage(); }
    }

}
