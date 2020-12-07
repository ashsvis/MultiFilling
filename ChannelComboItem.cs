namespace MultiFilling
{
    public class ChannelComboItem
    {
        public string Name { get; set; }
        public int Index { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}