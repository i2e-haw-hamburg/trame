using System;
using Leap;

namespace Trame.Implementation.Device.Adapter
{
	/// <summary>
	/// Leap adapter.
	/// </summary>
    class LeapAdapter
    {
        private Controller leapController;
		/// <summary>
		/// Starts the controller.
		/// </summary>
		/// <param name="onFrameArrived">On frame arrived.</param>
        public void StartController(Action<Frame> onFrameArrived)
        {
            leapController = new Controller(new FrameListener(onFrameArrived));
            leapController.SetPolicy(Controller.PolicyFlag.POLICY_BACKGROUND_FRAMES);
        }
		/// <summary>
		/// Stop this instance.
		/// </summary>
        public void Stop()
        {
            
        }
    }
	/// <summary>
	/// Frame listener.
	/// </summary>
    class FrameListener : Listener
    {
        private Action<Frame> onFrameArrived;
		/// <summary>
		/// Initializes a new instance of the <see cref="Trame.Implementation.Device.Adapter.FrameListener"/> class.
		/// </summary>
		/// <param name="onFrameArrived">On frame arrived.</param>
        public FrameListener(Action<Frame> onFrameArrived)
        {
            this.onFrameArrived = onFrameArrived;
        }
		/// <summary>
		/// Raises the frame event.
		/// </summary>
		/// <param name="controller">Controller.</param>
        public override void OnFrame(Controller controller)
        {
            onFrameArrived(controller.Frame());
        }
    }
}
