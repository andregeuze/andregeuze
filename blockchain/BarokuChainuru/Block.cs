using System;
using System.Transactions;

namespace BarokuChainuru
{
    public class Block
    {
        public int Index { get; set; }

        public string Hash { get; set; }

        public string PreviousHash { get; set; }

        public DateTime TimeStamp { get; set; }

        public Transaction[] Data { get; set; }
    }
}
