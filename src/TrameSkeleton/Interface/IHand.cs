namespace Trame
{
    public interface IHand : IJoint
    {
        /// <summary>
        /// Returns true, if this hand is confidently tracked and in a closed state.
        /// </summary>
        bool IsClosed { get; }
    }
}