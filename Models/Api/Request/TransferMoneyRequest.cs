using System;
using PhoenixFutureApiSdk.Model.Enums;

namespace PhoenixFutureApiSdk.Model.Request
{
    public class TransferMoneyRequest
    {
        public double Value { get; set; }
        public TransferType TransferType { get; set; }
    }
}
