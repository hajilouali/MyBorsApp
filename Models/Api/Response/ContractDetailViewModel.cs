using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Api.Enums;

namespace Models.Api.Response
{
  public  class ContractDetailViewModel
    {
        public Int32 contractId { get; set; }
        public string symbol { get; set; }
        public double tickSize { get; set; }
        public Int32 askVolume1 { get; set; }
        public double askPrice1 { get; set; }
        public Int32 askVolume2 { get; set; }
        public double askPrice2 { get; set; }
        public Int32 askVolume3 { get; set; }
        public double askPrice3 { get; set; }
        public Int32 bidVolume1 { get; set; }
        public double bidPrice1 { get; set; }
        public Int32 bidVolume2 { get; set; }
        public double bidPrice2 { get; set; }
        public Int32 bidVolume3 { get; set; }
        public double bidPrice3 { get; set; }
        public double firstTradedPrice { get; set; }
        public double lowTradedPrice { get; set; }
        public double highTradedPrice { get; set; }
        public double lastTradedPrice { get; set; }
        public string lastTradingPDateTime { get; set; }
        public string ordersPDateTime { get; set; }
        public string firstTradedPDateTime { get; set; }
        public string lastTradedPDateTime { get; set; }
        public double lot { get; set; }
        public double initialMargin { get; set; }
        public double requiredMargin { get; set; }
        public Int32 tradesVolume { get; set; }
        public double tradesValue { get; set; }
        public Int32 openInterests { get; set; }
        public Int32 openInterestsChanges { get; set; }
        public double maxOrderSize { get; set; }
        public double minPricePercent { get; set; }
        public double maxPricePercent { get; set; }
        public priceType PriceType { get; set; }

        public double settlementsPrice { get; set; }
        public string settlementsDate { get; set; }
        public double lastWorkingSettlementPrice { get; set; }
        public double realtimePrice { get; set; }



    }
}
