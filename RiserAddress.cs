namespace MultiFilling
{
    public struct RiserAddress
    {
        public int Channel { get; set; }
        public int Overpass { get; set; }
        public int Way { get; set; }
        public string Product { get; set; }
        public int Riser { get; set; }

        public override string ToString()
        {
            return "Стояк " + Riser;
        }
    }
}