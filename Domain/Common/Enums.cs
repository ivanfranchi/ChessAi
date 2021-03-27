namespace ChessAI.Domain.Common
{
    /// <summary>
    /// Given a coordinate we might have these kind of pieces in place
    /// </summary>
    public enum ConflictType
    {
        None = 0,
        Ally = 1,
        Enemy = 2
    }
}
