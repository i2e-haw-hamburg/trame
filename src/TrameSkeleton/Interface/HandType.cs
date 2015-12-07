namespace TrameSkeleton.Interface
{
    /// <summary>
    /// Determines, which hand is meant.
    /// </summary>
    public enum HandType
    {
        // Only the left hand. (Or null, if not tracked)
        Left,

        // Only the right hand. (Or null, if not tracked)
        Right,

        // Accept any hand (or null if no hands tracked)
        Any
    }
}
