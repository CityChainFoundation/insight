using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace City.Chain.Insight.Models.ApiModels
{
    public class AddressModel
    {
        public AddressModel()
        {
            Transactions = new List<TransactionModel>();
            UnconfirmedTransactions = new List<TransactionModel>();
        }

        public string CoinTag { get; set; }

        public string Address { get; set; }

        public double Balance { get; set; }

        public double TotalReceived { get; set; }

        public double TotalSent { get; set; }

        public double UnconfirmedBalance { get; set; }

        public List<TransactionModel> Transactions { get; set; }

        public List<TransactionModel> UnconfirmedTransactions { get; set; }
    }
}
