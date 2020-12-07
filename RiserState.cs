namespace MultiFilling
{
    public enum RiserState
    {
        None = 0,
        Waiting = 1,
        HandWaiting = 2,
        HandSmallFilling = 3,
        HandBigFilling = 4,
        SmallFilling = 5,
        BigFilling = 6,
        FillingByAuto = 7,
        FillingByOper = 8,
        Filled = 9
    }
}