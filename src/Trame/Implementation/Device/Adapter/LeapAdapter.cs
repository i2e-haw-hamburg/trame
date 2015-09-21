using System;
using Leap;

namespace Trame.Implementation.Device.Adapter
{
	/// <summary>
	/// Leap adapter.
	/// </summary>
    class LeapAdapter
    {
        private Controller _leapController;
	    private FrameListener _listener;

	    /// <summary>
		/// Starts the controller.
		/// </summary>
		/// <param name="onFrameArrived">On frame arrived.</param>
        public void StartController(Action<Frame> onFrameArrived)
		{
		    _listener = new FrameListener(onFrameArrived);
            _leapController = new Controller(_listener);
            _leapController.SetPolicy(Controller.PolicyFlag.POLICY_BACKGROUND_FRAMES);
        }
		/// <summary>
		/// Stop this instance.
		/// </summary>
        public void Stop()
		{
            _listener?.Dispose();
            _leapController?.Dispose();
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
