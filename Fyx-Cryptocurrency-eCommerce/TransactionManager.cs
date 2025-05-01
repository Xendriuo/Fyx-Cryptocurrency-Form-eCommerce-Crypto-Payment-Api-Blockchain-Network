using NBitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BitNav
{
    public class TransactionManager
    {
        private List<Transaction> transactions;
        private List<Transaction> unconfirmedTransactions;
        private List<Transaction> confirmedTransactions;
        
        private Thread transactionChecker;

        public TransactionManager()
        {
            transactions = new List<Transaction>();
            unconfirmedTransactions = new List<Transaction>();
            confirmedTransactions = new List<Transaction>();
            transactionChecker = transactionCheckerThread();
            transactionChecker.Start();
        }


        private Thread transactionCheckerThread()
        {
            Thread t = new Thread(() => checkTransactions());
            return t;
        }

        private void checkTransactions()
        {
            while (true)
            {
                List<Transaction> transactionsToRemove = new List<Transaction>();

                lock (unconfirmedTransactions)
                {
                    lock (confirmedTransactions)
                    {
                        foreach (Transaction transaction in unconfirmedTransactions)
                        {
                            if (transaction.checkTransactionConfirmed())
                            {
                                confirmedTransactions.Add(transaction);
                                transactionsToRemove.Add(transaction);

                            }
                        }

                        foreach (Transaction transaction in transactionsToRemove)
                        {
                            unconfirmedTransactions.Remove(transaction);
                        }
                    }
                }

                Thread.Sleep(120000);
            }

        }
        //create Transaction
        public Transaction CreateTransaction(object coin, double amount, string address)
        {
            Transaction transaction = new Transaction(coin, amount, address);
            transactions.Add(transaction);
            lock (unconfirmedTransactions)
            {
                unconfirmedTransactions.Add(transaction);
            }
            return transaction;
        }
        //add transaction
        public void AddTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
            lock (unconfirmedTransactions)
            {
                unconfirmedTransactions.Add(transaction);
            }
        }

        //remove transaction
        public void RemoveTransaction(Transaction transaction)
        {
            transactions.Remove(transaction);

            lock (unconfirmedTransactions)
            {
                unconfirmedTransactions.Remove(transaction);
            }

            lock (confirmedTransactions)
            {
                confirmedTransactions.Remove(transaction);
            }
        }

        public List<Transaction> GetTransactions()
        {
            return transactions;
        }
    }
}
