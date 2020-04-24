using System;
namespace PhoenixFutureApiSdk.Model.Response
{
    public class OpenPosition
    {
        public string Symbol { get; set; }
        public int Sell { get; set; }
        public int Buy { get; set; }

        public int ContractId { get; set; }
    }
}
