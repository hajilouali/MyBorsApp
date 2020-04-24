using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DataLayer;
using Models.Site.Models.contracts;
using MyBorsApp.Convertor;
using PhoenixFutureApiSdk;
using Quartz;

namespace MyBorsApp.Jobs
{
    [DisallowConcurrentExecution]
    public  class Contractlog:IJob
    {
        
        public async  Task Execute(IJobExecutionContext context)
        {
            if (!DataUtility.HolidayTest(DateTime.Now))
            {
                MyContext db = new MyContext();
                FutureSdk sdk = new FutureSdk("bmi");
                string token = db.Userses.Where(p => p.Email.Contains("hajilouali")).SingleOrDefault().Token;
                var allcontract =
                    await sdk.GetAllContract(token);
                if (DateTime.Now.DayOfWeek==DayOfWeek.Thursday&&DateTime.Now.Hour<16)
                {
                    foreach (var item in allcontract.Response.FindAll(p => p.name.Contains("آتی")))
                    {
                        var con = sdk.ContractDetail(item.id, token);
                        if (con.Result.Response.lastTradedPrice != 0)
                        {
                            db.ContractCellslogs.Add(new ContractCellslog()
                            {

                                tradesVolume = con.Result.Response.tradesVolume,
                                contractId = con.Result.Response.contractId,
                                lastTradedPrice = con.Result.Response.lastTradedPrice,
                                PriceType = con.Result.Response.PriceType,
                                askPrice1 = con.Result.Response.askPrice1,
                                askPrice2 = con.Result.Response.askPrice2,
                                askPrice3 = con.Result.Response.askPrice3,
                                askVolume1 = con.Result.Response.askVolume1,
                                askVolume2 = con.Result.Response.askVolume2,
                                askVolume3 = con.Result.Response.askVolume3,
                                bidPrice1 = con.Result.Response.bidPrice1,
                                bidPrice2 = con.Result.Response.bidPrice2,
                                bidPrice3 = con.Result.Response.bidPrice3,
                                bidVolume1 = con.Result.Response.bidVolume1,
                                bidVolume2 = con.Result.Response.bidVolume2,
                                bidVolume3 = con.Result.Response.bidVolume3,
                                firstTradedPDateTime = con.Result.Response.firstTradedPDateTime,
                                firstTradedPrice = con.Result.Response.firstTradedPrice,
                                highTradedPrice = con.Result.Response.highTradedPrice,
                                initialMargin = con.Result.Response.initialMargin,
                                lastTradedPDateTime = con.Result.Response.lastTradedPDateTime,
                                lastTradingPDateTime = con.Result.Response.lastTradingPDateTime,
                                lastWorkingSettlementPrice = con.Result.Response.lastWorkingSettlementPrice,
                                lot = con.Result.Response.lot,
                                lowTradedPrice = con.Result.Response.lowTradedPrice,
                                maxOrderSize = con.Result.Response.maxOrderSize,
                                maxPricePercent = con.Result.Response.maxPricePercent,
                                minPricePercent = con.Result.Response.minPricePercent,
                                openInterests = con.Result.Response.openInterests,
                                openInterestsChanges = con.Result.Response.openInterestsChanges,
                                ordersPDateTime = con.Result.Response.ordersPDateTime,
                                realtimePrice = con.Result.Response.realtimePrice,
                                requiredMargin = con.Result.Response.requiredMargin,
                                settlementsDate = con.Result.Response.settlementsDate,
                                settlementsPrice = con.Result.Response.settlementsPrice,
                                symbol = con.Result.Response.symbol,
                                tickSize = con.Result.Response.tickSize,
                                tradesValue = con.Result.Response.tradesValue,
                                DateTimeCreatLog = DateTime.Now
                            });
                        }
                        

                    }
                }
                else
                {
                    if (DateTime.Now.DayOfWeek!=DayOfWeek.Thursday)
                    {
                        foreach (var item in allcontract.Response.FindAll(p => p.name.Contains("آتی")))
                        {
                            var con = sdk.ContractDetail(item.id, token);
                            if (con.Result.Response.lastTradedPrice!=0)
                            {
                                db.ContractCellslogs.Add(new ContractCellslog()
                                {

                                    tradesVolume = con.Result.Response.tradesVolume,
                                    contractId = con.Result.Response.contractId,
                                    lastTradedPrice = con.Result.Response.lastTradedPrice,
                                    PriceType = con.Result.Response.PriceType,
                                    askPrice1 = con.Result.Response.askPrice1,
                                    askPrice2 = con.Result.Response.askPrice2,
                                    askPrice3 = con.Result.Response.askPrice3,
                                    askVolume1 = con.Result.Response.askVolume1,
                                    askVolume2 = con.Result.Response.askVolume2,
                                    askVolume3 = con.Result.Response.askVolume3,
                                    bidPrice1 = con.Result.Response.bidPrice1,
                                    bidPrice2 = con.Result.Response.bidPrice2,
                                    bidPrice3 = con.Result.Response.bidPrice3,
                                    bidVolume1 = con.Result.Response.bidVolume1,
                                    bidVolume2 = con.Result.Response.bidVolume2,
                                    bidVolume3 = con.Result.Response.bidVolume3,
                                    firstTradedPDateTime = con.Result.Response.firstTradedPDateTime,
                                    firstTradedPrice = con.Result.Response.firstTradedPrice,
                                    highTradedPrice = con.Result.Response.highTradedPrice,
                                    initialMargin = con.Result.Response.initialMargin,
                                    lastTradedPDateTime = con.Result.Response.lastTradedPDateTime,
                                    lastTradingPDateTime = con.Result.Response.lastTradingPDateTime,
                                    lastWorkingSettlementPrice = con.Result.Response.lastWorkingSettlementPrice,
                                    lot = con.Result.Response.lot,
                                    lowTradedPrice = con.Result.Response.lowTradedPrice,
                                    maxOrderSize = con.Result.Response.maxOrderSize,
                                    maxPricePercent = con.Result.Response.maxPricePercent,
                                    minPricePercent = con.Result.Response.minPricePercent,
                                    openInterests = con.Result.Response.openInterests,
                                    openInterestsChanges = con.Result.Response.openInterestsChanges,
                                    ordersPDateTime = con.Result.Response.ordersPDateTime,
                                    realtimePrice = con.Result.Response.realtimePrice,
                                    requiredMargin = con.Result.Response.requiredMargin,
                                    settlementsDate = con.Result.Response.settlementsDate,
                                    settlementsPrice = con.Result.Response.settlementsPrice,
                                    symbol = con.Result.Response.symbol,
                                    tickSize = con.Result.Response.tickSize,
                                    tradesValue = con.Result.Response.tradesValue,
                                    DateTimeCreatLog = DateTime.Now
                                });
                            }
                            

                        }
                    }
                    
                }
                
                
                db.SaveChanges();
            }
           
            
        }
    }
}