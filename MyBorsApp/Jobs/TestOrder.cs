using MyBorsApp.Convertor;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using DataLayer;
using Models.Api.Enums;
using Models.Api.Request;
using Models.Api.Response;
using Models.Site.Models.contracts;
using MyBorsApp.ApiControler;
using MyBorsApp.Utility;
using PhoenixFutureApiSdk;

namespace MyBorsApp.Jobs
{
    public static class TestOrder
    {
        public static async void start()
        {
            //if (!DataUtility.HolidayTest(DateTime.Now))
            //{
            //    while (true)
            //    {
            //        MyContext db = new MyContext();
            //        FutureSdk sdk = new FutureSdk("bmi");
            //        foreach (var itemContractse in db.UserContractses.Where(p => p.IsActive))
            //        {

            //            var user = db.Userses.Where(p => p.UserID == itemContractse.UserID).SingleOrDefault();
            //            string token = user.Token;
            //            if (!string.IsNullOrEmpty(token))
            //            {
            //                //
            //                //var Contract1 = Apiservice.GetMessage(string.Format("/api/contract/") + itemContractse.ContractID1, token).Content.ReadAsAsync<ContractDetailViewModel>().Result;
            //                //var Contract2 = Apiservice.GetMessage(string.Format("/api/contract/") + itemContractse.ContractID2, token).Content.ReadAsAsync<ContractDetailViewModel>().Result;
            //                var Contract1 = await sdk.ContractDetail(itemContractse.ContractID1, token);
            //                var Contract2 = await sdk.ContractDetail(itemContractse.ContractID2, token);
            //                int askVolume1_Contract1 = Contract1.Response.askVolume1;
            //                double askPrice1_Contract1 = Contract1.Response.askPrice1;
            //                int bidVolume1_Contract1 = Contract1.Response.bidVolume1;
            //                double bidPrice1_Contract1 = Contract1.Response.bidPrice1;
            //                int askVolume1_Contract2 = Contract2.Response.askVolume1;
            //                double askPrice1_Contract2 = Contract2.Response.askPrice1;
            //                int bidVolume1_Contract2 = Contract2.Response.bidVolume1;
            //                double bidPrice1_Contract2 = Contract2.Response.bidPrice1;
            //                var GetPosition = sdk.GetOpenPositions(token);
            //                //آیا در قرار داد هست ؟
            //                //در قرار داد هست

            //                if (GetPosition.Result.Response.Where(p => p.ContractId == itemContractse.ContractID1 || p.ContractId == itemContractse.ContractID2).Any())
            //                {


            //                    //        //آیا  به مبلغ سغف برای اف ست رسیده است؟
            //                    if ((bidPrice1_Contract2 - askPrice1_Contract1) >= itemContractse.Max)
            //                    {
            //                        NewOrderViewModel item = new NewOrderViewModel()
            //                        {
            //                            customer = -1,
            //                            contract = itemContractse.ContractID1,
            //                            orderSide = orderSide.Bye,
            //                            price = Convert.ToInt32(askPrice1_Contract1),
            //                            quantity = NumberUtility.ReternMin(NumberUtility.ReternMin(askVolume1_Contract1, bidVolume1_Contract2), GetPosition.Result.Response.Where(p => p.ContractId == itemContractse.ContractID1).Sum(p => p.Sell)),

            //                        };
            //                        //await sdk.NewOrder(item, token);
            //                        db.UsersOrderLogses.Add(new UsersOrderLogs()
            //                        {
            //                            UserID = itemContractse.UserID,
            //                            ContractID = itemContractse.ContractID1,
            //                            OrderSide = item.orderSide,
            //                            Price = item.price,
            //                            quantity = item.quantity

            //                        });
            //                        itemContractse.ContractCountActive -= 1;
            //                        db.UserContractses.AddOrUpdate(itemContractse);
            //                        NewOrderViewModel itemcell = new NewOrderViewModel()
            //                        {
            //                            customer = -1,
            //                            contract = itemContractse.ContractID2,
            //                            orderSide = orderSide.Cells,
            //                            price = Convert.ToInt32(bidPrice1_Contract2),
            //                            quantity = item.quantity
            //                        };
            //                        db.UsersOrderLogses.Add(new UsersOrderLogs()
            //                        {
            //                            UserID = itemContractse.UserID,
            //                            ContractID = itemContractse.ContractID2,
            //                            OrderSide = item.orderSide,
            //                            Price = item.price,
            //                            quantity = item.quantity

            //                        });
            //                        //await sdk.NewOrder(itemcell, token);
            //                        itemContractse.ContractCountActive -= 1;
            //                        if (itemContractse.ContractCountActive < 1)
            //                        {
            //                            itemContractse.IsActive = false;
            //                        }
            //                        db.UserContractses.AddOrUpdate(itemContractse);
                                    
            //                        //            //انجام معامله افست
            //                    }
            //                    //        //آیا به سقف تعداد خرید و فروش رسیده است ؟
            //                    if (itemContractse.ContContract < sdk.OpenPosstions(token).Result.Response.Where(p => p.contractId == itemContractse.ContractID1).SingleOrDefault().sell || itemContractse.ContContract < sdk.OpenPosstions(token).Result.Response.Where(p => p.contractId == itemContractse.ContractID1 || p.contractId == itemContractse.ContractID2).SingleOrDefault().buy)
            //                    {
            //                        //            //عملیات لغو قرار داد های در حال انتظار 
            //                        var pending = sdk.pendingOrders(token);
            //                        foreach (var VARIABLE in pending.Result.Response.rows.Where(p => p.contractId == itemContractse.ContractID1 || p.contractId == itemContractse.ContractID2))
            //                        {
            //                            await sdk.DeleteOrder(new DeleteOrderViewModel()
            //                            {
            //                                customer = -1,
            //                                orderItemId = Convert.ToInt32(VARIABLE.id),
            //                            }, token);
            //                            itemContractse.ContractCountActive += 1;
            //                            db.UserContractses.AddOrUpdate(itemContractse);
            //                        }
            //                        //            //آیا به مبلغ  کف خرید رسیده است ؟
            //                        if ((askPrice1_Contract2 - bidPrice1_Contract1) <= itemContractse.Min)
            //                        {
            //                            //                //انجام معامله خرید 
            //                            NewOrderViewModel item = new NewOrderViewModel()
            //                            {
            //                                quantity = NumberUtility.ReternMin(NumberUtility.ReternMin(askVolume1_Contract2, bidVolume1_Contract1), itemContractse.ContContract - GetPosition.Result.Response.Where(p => p.ContractId == itemContractse.ContractID2).Sum(p => p.Sell)),
            //                                orderSide = orderSide.Bye,
            //                                price = Convert.ToInt32(askPrice1_Contract2),
            //                                contract = itemContractse.ContractID2,
            //                                customer = -1

            //                            };
            //                            itemContractse.ContractCountActive -= 1;
            //                            db.UserContractses.AddOrUpdate(itemContractse);
            //                            await sdk.NewOrder(item, token);
            //                            db.UsersOrderLogses.Add(new UsersOrderLogs()
            //                            {
            //                                UserID = itemContractse.UserID,
            //                                ContractID = itemContractse.ContractID2,
            //                                OrderSide = item.orderSide,
            //                                Price = item.price,
            //                                quantity = item.quantity

            //                            });
            //                            NewOrderViewModel sellitem = new NewOrderViewModel()
            //                            {
            //                                quantity = item.quantity,
            //                                orderSide = orderSide.Cells,
            //                                price = Convert.ToInt32(bidPrice1_Contract1),
            //                                contract = itemContractse.ContractID1,
            //                                customer = -1
            //                            };
            //                            //await sdk.NewOrder(sellitem, token);
            //                            db.UsersOrderLogses.Add(new UsersOrderLogs()
            //                            {
            //                                UserID = itemContractse.UserID,
            //                                ContractID = itemContractse.ContractID2,
            //                                OrderSide = sellitem.orderSide,
            //                                Price = sellitem.price,
            //                                quantity = sellitem.quantity

            //                            });
            //                            itemContractse.ContractCountActive -= 1;
            //                            db.UserContractses.AddOrUpdate(itemContractse);
                                        
            //                        }
            //                    }

            //                }
            //                //     //در قرار داد نیست 
            //                else
            //                {
            //                    var pending = sdk.pendingOrders(token);
            //                    //        //عملیات لغو قرار داد های در حال انتظار 
            //                    foreach (var VARIABLE in pending.Result.Response.rows.Where(p => p.contractId == itemContractse.ContractID1 || p.contractId == itemContractse.ContractID2))
            //                    {
            //                        await sdk.DeleteOrder(new DeleteOrderViewModel()
            //                        {
            //                            customer = -1,
            //                            orderItemId = Convert.ToInt32(VARIABLE.id),
            //                        }, token);
            //                        itemContractse.ContractCountActive += 1;
            //                        db.UserContractses.AddOrUpdate(itemContractse);
            //                    }

            //                    //        //آیا به کف مبلغ برای خرید  رسیده  است؟
            //                    if ((askPrice1_Contract2 - bidPrice1_Contract1) <= itemContractse.Min)
            //                    {
            //                        //انجام معامله خرید 
            //                        NewOrderViewModel item = new NewOrderViewModel()
            //                        {
            //                            quantity = NumberUtility.ReternMin(NumberUtility.ReternMin(askVolume1_Contract2, bidVolume1_Contract1), itemContractse.ContContract),
            //                            orderSide = orderSide.Bye,
            //                            price = Convert.ToInt32(askPrice1_Contract2),
            //                            contract = itemContractse.ContractID2,
            //                            customer = -1

            //                        };
            //                        itemContractse.ContractCountActive -= 1;
            //                        db.UserContractses.AddOrUpdate(itemContractse);
            //                        //await sdk.NewOrder(item, token);
            //                        db.UsersOrderLogses.Add(new UsersOrderLogs()
            //                        {
            //                            UserID = itemContractse.UserID,
            //                            ContractID = itemContractse.ContractID2,
            //                            OrderSide = item.orderSide,
            //                            Price = item.price,
            //                            quantity = item.quantity

            //                        });

            //                        NewOrderViewModel sellitem = new NewOrderViewModel()
            //                        {
            //                            quantity = item.quantity,
            //                            orderSide = orderSide.Cells,
            //                            price = Convert.ToInt32(bidPrice1_Contract1),
            //                            contract = itemContractse.ContractID1,
            //                            customer = -1
            //                        };
            //                        //await sdk.NewOrder(sellitem, token);
            //                        db.UsersOrderLogses.Add(new UsersOrderLogs()
            //                        {
            //                            UserID = itemContractse.UserID,
            //                            ContractID = itemContractse.ContractID1,
            //                            OrderSide = sellitem.orderSide,
            //                            Price = sellitem.price,
            //                            quantity = sellitem.quantity

            //                        });
            //                        itemContractse.ContractCountActive -= 1;

            //                        db.UserContractses.AddOrUpdate(itemContractse);
                                    

            //                    }
            //                }
            //                if (itemContractse.ContContract <= 1)
            //                {
            //                    var position = sdk.OpenPosstions(token);
            //                    //        //آیا در قرار داد هست و به مبلغ سقف رسیده؟
            //                    if ((bidPrice1_Contract2 - askPrice1_Contract1) >= itemContractse.Max && position.Result.Response.Where(p => p.contractId == itemContractse.ContractID1 || p.contractId == itemContractse.ContractID2).Any())
            //                    {

            //                        NewOrderViewModel item = new NewOrderViewModel()
            //                        {
            //                            customer = -1,
            //                            contract = itemContractse.ContractID1,
            //                            orderSide = orderSide.Bye,
            //                            price = Convert.ToInt32(askPrice1_Contract1),
            //                            quantity = NumberUtility.ReternMin(NumberUtility.ReternMin(askVolume1_Contract1, bidVolume1_Contract2), position.Result.Response.Where(p => p.contractId == itemContractse.ContractID1).Sum(p => p.sell)),

            //                        };
            //                        //await sdk.NewOrder(item, token);
            //                        db.UsersOrderLogses.Add(new UsersOrderLogs()
            //                        {
            //                            UserID = itemContractse.UserID,
            //                            ContractID = itemContractse.ContractID1,
            //                            OrderSide = item.orderSide,
            //                            Price = item.price,
            //                            quantity = item.quantity

            //                        });
            //                        itemContractse.ContractCountActive -= 1;
            //                        db.UserContractses.AddOrUpdate(itemContractse);
            //                        NewOrderViewModel itemcell = new NewOrderViewModel()
            //                        {
            //                            customer = -1,
            //                            contract = itemContractse.ContractID2,
            //                            orderSide = orderSide.Cells,
            //                            price = Convert.ToInt32(bidPrice1_Contract2),
            //                            quantity = item.quantity
            //                        };
            //                        //await sdk.NewOrder(itemcell, token);
            //                        db.UsersOrderLogses.Add(new UsersOrderLogs()
            //                        {
            //                            UserID = itemContractse.UserID,
            //                            ContractID = itemContractse.ContractID2,
            //                            OrderSide = itemcell.orderSide,
            //                            Price = itemcell.price,
            //                            quantity = itemcell.quantity

            //                        });
            //                        itemContractse.ContractCountActive -= 1;
            //                        if (itemContractse.ContractCountActive < 1)
            //                        {
            //                            itemContractse.IsActive = false;
            //                        }
            //                        db.UserContractses.AddOrUpdate(itemContractse);
                                    
            //                        //                //عملیات offset کردن 


            //                    }
            //                }

            //            }



            //        }
            //        db.SaveChanges();
            //        db.Dispose();
            //        if (DateTime.Now.Date > Convert.ToDateTime("17:00:00"))
            //        {
            //            break;
            //        }
            //        Thread.Sleep(1000);
            //    }

            //}
        }
    }
}