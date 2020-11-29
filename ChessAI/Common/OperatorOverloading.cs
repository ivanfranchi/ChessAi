namespace ChessAI.Common
{
    public static class OperatorOverloading
    {
        /// <summary>
        /// If increment > 0 (going up) stop before 7
        /// otherwise, if going down, stop before 0
        /// </summary>
        /// <param name="increment">going up or wown, generally +-1</param>
        /// <param name="coordinate">Check if we are leaving the board by coordinate > or < of 0 or 7</param>
        /// <returns>Are we out of the board</returns>
        public static bool Comparer(int increment, int coordinate)
        {
            if (increment > 0)
            {
                return coordinate <= 7;
            }
            if (increment < 0)
            {
                return coordinate >= 0;
            }
            return true;
        }
    }
}
