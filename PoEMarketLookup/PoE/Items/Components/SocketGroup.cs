using System.Collections.Generic;

namespace PoEMarketLookup.PoE.Items.Components
{
    public class SocketGroup
    {
        public int WhiteSockets { get; private set; }
        public int RedSockets { get; private set; }
        public int GreenSockets { get; private set; }
        public int BlueSockets { get; private set; }
        public int Sockets { get { return WhiteSockets + RedSockets + BlueSockets + GreenSockets; } }
        public int[] Links { get; private set; }
        public int LargestLink { get; private set; }

        public static SocketGroup Parse(string socketGroup)
        {
            var sg = new SocketGroup();
            var links = new List<int>();
            int currentLink = 1;

            foreach(char socket in socketGroup)
            {
                switch (socket)
                {
                    case 'W': sg.WhiteSockets++; break;

                    case 'R': sg.RedSockets++; break;

                    case 'G': sg.GreenSockets++; break;

                    case 'B': sg.BlueSockets++; break;

                    case '-': currentLink++; break;

                    case ' ': links.Add(currentLink);
                              currentLink = 1;
                              break;
                }

                if(currentLink > sg.LargestLink)
                {
                    sg.LargestLink = currentLink;
                }
            }

            links.Add(currentLink);
            sg.Links = links.ToArray();

            return sg;
        }
    }
}
