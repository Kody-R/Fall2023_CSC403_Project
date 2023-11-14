using System;
using System.Collections.Generic;
namespace SlimDX2DFramework
{
    public class ControllerDispatch
    {
        /// <summary>
        /// Declare a new delegate for the controller input event handler.
        /// </summary>
        public delegate void ControllerInputEventHandler();

        /// <summary>
        /// Declare a new event handler which hooks to the controller input.
        /// </summary>
        public event ControllerInputEventHandler CheckInput;

        /// <summary>
        /// Declare a new bool variable which indicates whether an input was passed 
        /// on the controller buttons.
        /// </summary>
        bool controllerInputted = false;

        /// <summary>
        /// Declare a new direct input class.
        /// </summary>
        private SlimDX.DirectInput.DirectInput input = new SlimDX.DirectInput.DirectInput();

        /// <summary>
        /// Hold a list of the inputted stick values.
        /// </summary>
        private List<SlimDX.DirectInput.Joystick> sticksButtons = new List<SlimDX.DirectInput.Joystick>();

        /// <summary>
        /// Declrae a new input type of joystick.
        /// </summary>
        private SlimDX.DirectInput.Joystick stick;

        /// <summary>
        /// This class is passed into the joystick class as a parameter.
        /// </summary>
        private SlimDX.DirectInput.DirectInput directInput;

        /// <summary>
        /// Declare a new joystick array. 
        /// </summary>
        SlimDX.DirectInput.Joystick[] Sticks;

        /// <summary>
        /// Declare the joystick class.
        /// </summary>
        private SlimDX.DirectInput.Joystick[] joystick;

        /// <summary>
        /// Declare a new input type of joystick.
        /// </summary>
        SlimDX.DirectInput.Joystick stickButton;

        /// <summary>
        /// Declare a new int which holds the y value passed through the
        /// joystick position.
        /// </summary>
        int yvalue = 0;

        /// <summary>
        /// Declare a new int which holds the x value passed through the
        /// joystick position.
        /// </summary>
        int xvalue = 0;

        /// <summary>
        /// Holds the value of the analoge stick about the X plane.
        /// </summary>
        private int axisX = 0;

        /// <summary>
        /// Holds the value of the analoge stick about the Y plane.
        /// </summary>
        private int axisY = 0;

        /// <summary>
        /// Holds the value of the analoge stick about the X plane.
        /// </summary>
        private int axisZ = 0;

        /// <summary>
        /// Holds the state of the buttons.
        /// </summary>
        public bool[] buttons;

        /// <summary>
        /// Declare a new int which holds the Y Axis of the controller.
        /// </summary>
        private int yaxis;

        /// <summary>
        /// Declare a new int which holds the X Axis of the controller.
        /// </summary>
        private int xaxis;

        /// <summary>
        /// Declare a guid object class.
        /// </summary>
        private System.Guid guid;

        /// <summary>
        /// Exposes the state of a game controller.
        /// </summary>
        SlimDX.DirectInput.JoystickState state = new SlimDX.DirectInput.JoystickState();

        /// <summary>
        /// The main clock for the program.
        /// </summary>

        public ControllerDispatch()
        {
            // Initialize the controller classes.
            this.guid = new System.Guid();
            this.directInput = new SlimDX.DirectInput.DirectInput();

            this.state = new SlimDX.DirectInput.JoystickState();

            this.Sticks = GetStick();
        }

        /// <summary>
        /// A public accessor for the y-axis value of the analogue stick.
        /// </summary>
        public int YAxis
        {
            get
            {
                return this.yaxis;
            }
            set
            {
                this.yaxis = value;

                this.CheckInput?.Invoke();
            }
        }

        /// <summary>
        /// A public accessor for the x-axis value of the analogue stick.
        /// </summary>
        public int XAxis
        {
            get
            {
                return this.xaxis;
            }
            set
            {
                this.xaxis = value;
                this.CheckInput?.Invoke();
            }
        }

        /// <summary>
        /// The main function for the repeating functionality of the program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CheckControllerInputUpdate(object sender, EventArgs e)
        {
            // Listen for controller inputs.
            for (int i = 0; i < this.Sticks.Length; i++)
            {
                stickHandle(this.Sticks[i], i);
            }
        }

        /// <summary>
        /// Gets the state of the analogue stick, and passes it's value to the 'Stick handle' function.
        /// </summary>
        public SlimDX.DirectInput.Joystick[] GetStick()
        {
            List<SlimDX.DirectInput.Joystick> sticks = new List<SlimDX.DirectInput.Joystick>();

            foreach (SlimDX.DirectInput.DeviceInstance device in input.GetDevices(SlimDX.DirectInput.DeviceClass.GameController, SlimDX.DirectInput.DeviceEnumerationFlags.AttachedOnly))
            {
                try
                {
                    stick = new SlimDX.DirectInput.Joystick(input, device.InstanceGuid);
                    stick.Acquire();

                    stickButton = new SlimDX.DirectInput.Joystick(input, device.InstanceGuid);
                    stickButton.Acquire();

                    foreach (SlimDX.DirectInput.DeviceObjectInstance deviceObject in stick.GetObjects())
                    {
                        if ((deviceObject.ObjectType & SlimDX.DirectInput.ObjectDeviceType.Axis) != 0)
                        {
                            stick.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(-100, 100);
                        }
                    }

                    sticks.Add(stick);
                    sticksButtons.Add(stickButton);
                }
                catch (SlimDX.DirectInput.DirectInputException)
                {

                }


            }
            this.sticksButtons.ToArray();
            return sticks.ToArray();
        }

        /// <summary>
        /// This method handles the button mapping of the controller.
        /// </summary>
        /// <param name="stick">The instance of the joystick being controlled.</param>
        /// <param name="id">An index to see what button/input has been passed.</param>
        private void stickHandle(SlimDX.DirectInput.Joystick stick, int id)
        {
            state = stick.GetCurrentState();

            buttons = state.GetButtons();

            /*this.axisX = state.X;
            this.axisY = state.Y;
            this.axisZ = state.Z;*/

            // Check button IDs to see which button has been pressed.
            if (id == 0)
            {
                for (int x = 0; x < buttons.Length; x++)
                {
                    if (buttons[x] == true)
                    {
                        // Put your button input logic here!!
                    }
                }
            }

            // Moving right.
            if (state.X > 20)
            {
                this.XAxis = state.X;
            }

            // Moving left.
            if (state.X < -20)
            {
                this.XAxis = state.X;
            }

            // Moving up.
            if (state.Y > 20)
            {
                this.YAxis = state.Y;
            }

            // Moving down.
            if (state.Y < -20)
            {
                this.YAxis = state.Y;
            }
        }

    }
}
