namespace PoEMarketLookup.PoE.Items.Components
{
    public class SocketGroup
    {
        public int WhiteSockets { get; private set; }
        public int RedSockets { get; private set; }
        public int GreenSockets { get; private set; }
        public int BlueSockets { get; private set; }

        public static SocketGroup Parse(string socketGroup)
        {
            var sg = new SocketGroup();

            foreach(char socket in socketGroup)
            {
                switch (socket)
                {
                    case 'W': sg.WhiteSockets++; break;
                    case 'R': sg.RedSockets++; break;
                    case 'G': sg.GreenSockets++; break;
                    case 'B': sg.BlueSockets++; break;
                }
            }

            return sg;
        }
    }
}
