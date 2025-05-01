using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitNav
{
    public class TransactionEventArgs : EventArgs
    {
        public Transaction Transaction { get; }

        public TransactionEventArgs(Transaction transaction)
        {
            Transaction = transaction;
        }
    }
}
