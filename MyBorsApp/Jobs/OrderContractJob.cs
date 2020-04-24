using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using DataLayer;
using Models.Api.Enums;
using Models.Api.Request;
using Models.Api.Response;
using Models.Site.Models.contracts;
using Models.Site.Models.Enums;
using MyBorsApp.ApiControler;
using MyBorsApp.Convertor;
using MyBorsApp.Utility;
using PhoenixFutureApiSdk;
using PhoenixFutureApiSdk.Model.Response;
using Quartz;

namespace MyBorsApp.Jobs
{
    [DisallowConcurrentExecution]

    public class OrderContractJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            //string strErrorMessage =
            //    string.Format(" Time:{0:yyyy/MM/dd - hh:mm:ss}",System.DateTime.Now);
            //System.IO.StreamWriter oStreamWriter = null;
            //string strApplicationPath =
            //    "~/App_Data/Application.log";



            //oStreamWriter =
            //    new System.IO.StreamWriter(System.Web.Hosting.HostingEnvironment.MapPath(strApplicationPath), true, System.Text.Encoding.UTF8);

            //oStreamWriter.WriteLine(strErrorMessage);
            //oStreamWriter.Dispose();
            //oStreamWriter = null;
            
            if (!DataUtility.HolidayTest(DateTime.Now))
            {
                if (DateTime.Now.TimeOfDay>TimeSpan.Parse("10:30:00"))
                {
                    MyContext db = new MyContext();
                    FutureSdk sdk = new FutureSdk("bmi");

                    foreach (var itemContractse in db.UserContractses.Where(p => p.IsActive && p.ContractCountActive > 1))
                    {

                        var user = db.Userses.Where(p => p.UserID == itemContractse.UserID).SingleOrDefault();
                        string token = user.Token;
                        if (!string.IsNullOrEmpty(token))
                        {

                            var Contract1 = await sdk.ContractDetail(itemContractse.ContractID1, token);
                            var Contract2 = await sdk.ContractDetail(itemContractse.ContractID2, token);
                            int askVolume1_Contract1 = Contract1.Response.askVolume1;
                            //int askVolume1_Contract1 = 3;
                            double askPrice1_Contract1 = Contract1.Response.askPrice1;
                            //double askPrice1_Contract1 = 330000;
                            int bidVolume1_Contract1 = Contract1.Response.bidVolume1;
                            //int bidVolume1_Contract1 = 4;
                            double bidPrice1_Contract1 = Contract1.Response.bidPrice1;
                            //double bidPrice1_Contract1 = 340000;
                            int askVolume1_Contract2 = Contract2.Response.askVolume1;
                            //int askVolume1_Contract2 = 4;
                            double askPrice1_Contract2 = Contract2.Response.askPrice1;
                            //double askPrice1_Contract2 = 335000;
                            int bidVolume1_Contract2 = Contract2.Response.bidVolume1;
                            //int bidVolume1_Contract2 = 3;
                            double bidPrice1_Contract2 = Contract2.Response.bidPrice1;
                            //double bidPrice1_Contract2 = 350000;
                            var GetPosition = sdk.GetOpenPositions(token);
                            //List<OpenPosition> GetPosition = new List<OpenPosition>();
                            //if (loop!=0)
                            //{
                            //    GetPosition.Add(new OpenPosition()
                            //        {
                            //            Symbol = "CSAB98",
                            //            Sell = 1,
                            //            Buy = 0,
                            //            ContractId = 131
                            //        });
                            //    GetPosition.Add(
                            //        new OpenPosition()
                            //        {
                            //            Symbol = "CSAZ98",
                            //            Sell = 0,
                            //            Buy = 1,
                            //            ContractId = 136
                            //        });
                            //}
                            switch (itemContractse.ContractType)
                            {
                                case ContractType.Normaly:
                                    {
                                        //آیا در قرار داد هست ؟
                                        //در قرار داد هست

                                        if (GetPosition.Result.Response.Where(p => p.ContractId == itemContractse.ContractID1).Sum(p=>p.Sell)>0&&GetPosition.Result.Response.Where(p=>p.ContractId==itemContractse.ContractID2).Sum(p=>p.Buy)>0)
                                        {

                                           
                                            //        //آیا  به مبلغ سغف برای اف ست رسیده است؟
                                            if ((bidPrice1_Contract2 - askPrice1_Contract1) >= itemContractse.Max && (bidVolume1_Contract2 > 0 && askVolume1_Contract1 > 0))
                                            {
                                                int q = NumberUtility.ReternMin(
                                                            NumberUtility.ReternMin(askVolume1_Contract1,
                                                                bidVolume1_Contract2),
                                                            GetPosition.Result.Response
                                                                .Where(p => p.ContractId == itemContractse.ContractID1)
                                                                .SingleOrDefault().Sell) > 1
                                                    ? NumberUtility.ReternMin(
                                                          NumberUtility.ReternMin(askVolume1_Contract1,
                                                              bidVolume1_Contract2),
                                                          GetPosition.Result.Response
                                                              .Where(p => p.ContractId == itemContractse.ContractID1)
                                                              .SingleOrDefault().Sell) / 2
                                                    : 1;
                                                if (Contract2.Response.tradesVolume>=Contract1.Response.tradesVolume)
                                                {

                                                    NewOrderViewModel item = new NewOrderViewModel()
                                                    {
                                                        customer = -1,
                                                        contract = itemContractse.ContractID1,
                                                        orderSide = orderSide.Bye,
                                                        price = Convert.ToInt32(askPrice1_Contract1),
                                                        quantity = q,

                                                    };
                                                    await sdk.NewOrder(item, token);

                                                    NewOrderViewModel itemcell = new NewOrderViewModel()
                                                    {
                                                        customer = -1,
                                                        contract = itemContractse.ContractID2,
                                                        orderSide = orderSide.Cells,
                                                        price = Convert.ToInt32(bidPrice1_Contract2),
                                                        quantity = item.quantity
                                                    };
                                                    await sdk.NewOrder(itemcell, token);
                                                    db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                    {
                                                        UserID = itemContractse.UserID,
                                                        ContractID = itemContractse.ContractID1,
                                                        OrderSide = item.orderSide,
                                                        Price = item.price,
                                                        quantity = item.quantity,
                                                        DateTime = DateTime.Now,
                                                        Contract1Price = Convert.ToInt32(askPrice1_Contract1),
                                                        Contract1Valum = askVolume1_Contract1,
                                                        contract2price = Convert.ToInt32(bidPrice1_Contract2),
                                                        Contract2Valum = bidVolume1_Contract2,


                                                    });
                                                    db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                    {
                                                        UserID = itemContractse.UserID,
                                                        ContractID = itemContractse.ContractID2,
                                                        OrderSide = itemcell.orderSide,
                                                        Price = itemcell.price,
                                                        quantity = itemcell.quantity,
                                                        DateTime = DateTime.Now,
                                                        Contract1Price = Convert.ToInt32(askPrice1_Contract1),
                                                        Contract1Valum = askVolume1_Contract1,
                                                        contract2price = Convert.ToInt32(bidPrice1_Contract2),
                                                        Contract2Valum = bidVolume1_Contract2,
                                                    });
                                                }
                                                else
                                                {
                                                    NewOrderViewModel itemcell = new NewOrderViewModel()
                                                    {
                                                        customer = -1,
                                                        contract = itemContractse.ContractID2,
                                                        orderSide = orderSide.Cells,
                                                        price = Convert.ToInt32(bidPrice1_Contract2),
                                                        quantity = q
                                                    };
                                                    await sdk.NewOrder(itemcell, token);
                                                    NewOrderViewModel item = new NewOrderViewModel()
                                                    {
                                                        customer = -1,
                                                        contract = itemContractse.ContractID1,
                                                        orderSide = orderSide.Bye,
                                                        price = Convert.ToInt32(askPrice1_Contract1),
                                                        quantity =itemcell.quantity ,

                                                    };
                                                    await sdk.NewOrder(item, token);
                                                    db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                    {
                                                        UserID = itemContractse.UserID,
                                                        ContractID = itemContractse.ContractID1,
                                                        OrderSide = item.orderSide,
                                                        Price = item.price,
                                                        quantity = item.quantity,
                                                        DateTime = DateTime.Now,
                                                        Contract1Price = Convert.ToInt32(askPrice1_Contract1),
                                                        Contract1Valum = askVolume1_Contract1,
                                                        contract2price = Convert.ToInt32(bidPrice1_Contract2),
                                                        Contract2Valum = bidVolume1_Contract2,


                                                    });
                                                    db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                    {
                                                        UserID = itemContractse.UserID,
                                                        ContractID = itemContractse.ContractID2,
                                                        OrderSide = itemcell.orderSide,
                                                        Price = itemcell.price,
                                                        quantity = itemcell.quantity,
                                                        DateTime = DateTime.Now,
                                                        Contract1Price = Convert.ToInt32(askPrice1_Contract1),
                                                        Contract1Valum = askVolume1_Contract1,
                                                        contract2price = Convert.ToInt32(bidPrice1_Contract2),
                                                        Contract2Valum = bidVolume1_Contract2,
                                                    });
                                                }
                                               
                                                
                                                itemContractse.ContractCountActive = itemContractse.ContractCountActive - 2;
                                                if (itemContractse.ContractCountActive < 2)
                                                {
                                                    itemContractse.IsActive = false;

                                                }
                                                db.UserContractses.AddOrUpdate(itemContractse);


                                                //            //انجام معامله افست
                                            }
                                            //        //آیا به سقف تعداد خرید و فروش رسیده است ؟
                                            var pos = sdk.OpenPosstions(token).Result.Response;
                                            if (itemContractse.ContContract > pos.Where(p => p.contractId == itemContractse.ContractID1).SingleOrDefault().sell && itemContractse.ContContract > pos.Where(p => p.contractId == itemContractse.ContractID2).SingleOrDefault().buy && itemContractse.ContractCountActive > 1)
                                            {
                                                //            //آیا به مبلغ  کف خرید رسیده است ؟
                                                if ((askPrice1_Contract2 - bidPrice1_Contract1) <= itemContractse.Min && (askVolume1_Contract2 > 0 && bidVolume1_Contract1 > 0))
                                                {
                                                    //            //عملیات لغو قرار داد های در حال انتظار 
                                                    var pending = sdk.pendingOrders(token);
                                                    foreach (var VARIABLE in pending.Result.Response.rows.Where(p => p.contractId == itemContractse.ContractID1 || p.contractId == itemContractse.ContractID2))
                                                    {
                                                        foreach (var itemsss in VARIABLE.orderItems)
                                                        {
                                                            await sdk.DeleteOrder(new DeleteOrderViewModel()
                                                            {
                                                                customer = -1,
                                                                orderItemId = Convert.ToInt32(itemsss.id),
                                                            }, token);
                                                        }

                                                        itemContractse.ContractCountActive += 1;

                                                    }
                                                    db.UserContractses.AddOrUpdate(itemContractse);
                                                    int q = NumberUtility.ReternMin(
                                                                NumberUtility.ReternMin(askVolume1_Contract2,
                                                                    bidVolume1_Contract1),
                                                                itemContractse.ContContract -
                                                                pos.Where(p =>
                                                                        p.contractId == itemContractse.ContractID2)
                                                                    .SingleOrDefault().buy) > 1
                                                        ? NumberUtility.ReternMin(
                                                              NumberUtility.ReternMin(askVolume1_Contract2,
                                                                  bidVolume1_Contract1),
                                                              itemContractse.ContContract -
                                                              pos.Where(p => p.contractId == itemContractse.ContractID2)
                                                                  .SingleOrDefault().buy) / 2
                                                        : 1;
                                                    if (Contract1.Response.tradesVolume<=Contract2.Response.tradesVolume)
                                                    {
                                                        NewOrderViewModel sellitem = new NewOrderViewModel()
                                                        {
                                                            quantity = q,
                                                            orderSide = orderSide.Cells,
                                                            price = Convert.ToInt32(bidPrice1_Contract1),
                                                            contract = itemContractse.ContractID1,
                                                            customer = -1
                                                        };
                                                        await sdk.NewOrder(sellitem, token);
                                                        NewOrderViewModel item = new NewOrderViewModel()
                                                        {
                                                            quantity = sellitem.quantity,
                                                            orderSide = orderSide.Bye,
                                                            price = Convert.ToInt32(askPrice1_Contract2),
                                                            contract = itemContractse.ContractID2,
                                                            customer = -1

                                                        };
                                                        await sdk.NewOrder(item, token);



                                                        db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                        {
                                                            UserID = itemContractse.UserID,
                                                            ContractID = itemContractse.ContractID2,
                                                            OrderSide = item.orderSide,
                                                            Price = item.price,
                                                            quantity = item.quantity,
                                                            DateTime = DateTime.Now,
                                                            Contract1Price = Convert.ToInt32(bidPrice1_Contract1),
                                                            Contract1Valum = bidVolume1_Contract1,
                                                            contract2price = Convert.ToInt32(askPrice1_Contract2),
                                                            Contract2Valum = askVolume1_Contract2,

                                                        });

                                                        db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                        {
                                                            UserID = itemContractse.UserID,
                                                            ContractID = itemContractse.ContractID2,
                                                            OrderSide = sellitem.orderSide,
                                                            Price = sellitem.price,
                                                            quantity = sellitem.quantity,
                                                            DateTime = DateTime.Now,
                                                            Contract1Price = Convert.ToInt32(bidPrice1_Contract1),
                                                            Contract1Valum = bidVolume1_Contract1,
                                                            contract2price = Convert.ToInt32(askPrice1_Contract2),
                                                            Contract2Valum = askVolume1_Contract2,

                                                        });
                                                    }
                                                    else
                                                    {
                                                        
                                                        NewOrderViewModel item = new NewOrderViewModel()
                                                        {
                                                            quantity = q,
                                                            orderSide = orderSide.Bye,
                                                            price = Convert.ToInt32(askPrice1_Contract2),
                                                            contract = itemContractse.ContractID2,
                                                            customer = -1

                                                        };
                                                        await sdk.NewOrder(item, token);
                                                        NewOrderViewModel sellitem = new NewOrderViewModel()
                                                        {
                                                            quantity =item.quantity ,
                                                            orderSide = orderSide.Cells,
                                                            price = Convert.ToInt32(bidPrice1_Contract1),
                                                            contract = itemContractse.ContractID1,
                                                            customer = -1
                                                        };
                                                        await sdk.NewOrder(sellitem, token);


                                                        db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                        {
                                                            UserID = itemContractse.UserID,
                                                            ContractID = itemContractse.ContractID2,
                                                            OrderSide = item.orderSide,
                                                            Price = item.price,
                                                            quantity = item.quantity,
                                                            DateTime = DateTime.Now,
                                                            Contract1Price = Convert.ToInt32(bidPrice1_Contract1),
                                                            Contract1Valum = bidVolume1_Contract1,
                                                            contract2price = Convert.ToInt32(askPrice1_Contract2),
                                                            Contract2Valum = askVolume1_Contract2,

                                                        });

                                                        db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                        {
                                                            UserID = itemContractse.UserID,
                                                            ContractID = itemContractse.ContractID2,
                                                            OrderSide = sellitem.orderSide,
                                                            Price = sellitem.price,
                                                            quantity = sellitem.quantity,
                                                            DateTime = DateTime.Now,
                                                            Contract1Price = Convert.ToInt32(bidPrice1_Contract1),
                                                            Contract1Valum = bidVolume1_Contract1,
                                                            contract2price = Convert.ToInt32(askPrice1_Contract2),
                                                            Contract2Valum = askVolume1_Contract2,

                                                        });
                                                    }
                                                    //                //انجام معامله خرید 
                                                   
                                                    itemContractse.ContractCountActive = itemContractse.ContractCountActive - 2;
                                                    if (itemContractse.ContractCountActive < 2)
                                                    {
                                                        itemContractse.IsActive = false;
                                                    }
                                                    db.UserContractses.AddOrUpdate(itemContractse);


                                                }



                                            }

                                        }
                                        //     //در قرار داد نیست 
                                        else
                                        {
                                            var pending = sdk.pendingOrders(token);
                                            //        //عملیات لغو قرار داد های در حال انتظار 
                                            foreach (var VARIABLE in pending.Result.Response.rows.Where(p => p.contractId == itemContractse.ContractID1 || p.contractId == itemContractse.ContractID2))
                                            {
                                                foreach (var itemsss in VARIABLE.orderItems)
                                                {
                                                    await sdk.DeleteOrder(new DeleteOrderViewModel()
                                                    {
                                                        customer = -1,
                                                        orderItemId = Convert.ToInt32(itemsss.id),
                                                    }, token);
                                                }
                                                
                                                itemContractse.ContractCountActive += 1;
                                            }

                                            //        //آیا به کف مبلغ برای خرید  رسیده  است؟
                                            if ((askPrice1_Contract2 - bidPrice1_Contract1) <= itemContractse.Min&&(askVolume1_Contract2 > 0 && bidVolume1_Contract1>0))
                                            {
                                                int q =
                                                    NumberUtility.ReternMin(
                                                        NumberUtility.ReternMin(askVolume1_Contract2,
                                                            bidVolume1_Contract1), itemContractse.ContContract) > 1
                                                        ? NumberUtility.ReternMin(
                                                              NumberUtility.ReternMin(askVolume1_Contract2,
                                                                  bidVolume1_Contract1),
                                                              itemContractse.ContContract) / 2
                                                        : 1;
                                                if (Contract1.Response.tradesVolume<=Contract2.Response.tradesVolume)
                                                {
                                                   
                                                    NewOrderViewModel sellitem = new NewOrderViewModel()
                                                    {
                                                        quantity =q ,
                                                        orderSide = orderSide.Cells,
                                                        price = Convert.ToInt32(bidPrice1_Contract1),
                                                        contract = itemContractse.ContractID1,
                                                        customer = -1
                                                    };
                                                    await sdk.NewOrder(sellitem, token);
                                                    NewOrderViewModel item = new NewOrderViewModel()
                                                    {
                                                        quantity = sellitem.quantity,
                                                        orderSide = orderSide.Bye,
                                                        price = Convert.ToInt32(askPrice1_Contract2),
                                                        contract = itemContractse.ContractID2,
                                                        customer = -1,
                                                        //marketOrder = true
                                                    };
                                                    await sdk.NewOrder(item, token);



                                                    db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                    {
                                                        UserID = itemContractse.UserID,
                                                        ContractID = itemContractse.ContractID2,
                                                        OrderSide = item.orderSide,
                                                        Price = item.price,
                                                        quantity = item.quantity,
                                                        DateTime = DateTime.Now,

                                                        Contract1Price = Convert.ToInt32(bidPrice1_Contract1),
                                                        Contract1Valum = bidVolume1_Contract1,
                                                        contract2price = Convert.ToInt32(askPrice1_Contract2),
                                                        Contract2Valum = askVolume1_Contract2,
                                                    });


                                                    db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                    {
                                                        UserID = itemContractse.UserID,
                                                        ContractID = itemContractse.ContractID1,
                                                        OrderSide = sellitem.orderSide,
                                                        Price = sellitem.price,
                                                        quantity = sellitem.quantity,
                                                        DateTime = DateTime.Now,
                                                        Contract1Price = Convert.ToInt32(bidPrice1_Contract1),
                                                        Contract1Valum = bidVolume1_Contract1,
                                                        contract2price = Convert.ToInt32(askPrice1_Contract2),
                                                        Contract2Valum = askVolume1_Contract2,
                                                    });
                                                }
                                                else
                                                {
                                                   
                                                    NewOrderViewModel item = new NewOrderViewModel()
                                                    {
                                                        quantity =q,
                                                        orderSide = orderSide.Bye,
                                                        price = Convert.ToInt32(askPrice1_Contract2),
                                                        contract = itemContractse.ContractID2,
                                                        customer = -1,
                                                        //marketOrder = true
                                                    };
                                                    await sdk.NewOrder(item, token);

                                                    NewOrderViewModel sellitem = new NewOrderViewModel()
                                                    {
                                                        quantity =item.quantity,
                                                        orderSide = orderSide.Cells,
                                                        price = Convert.ToInt32(bidPrice1_Contract1),
                                                        contract = itemContractse.ContractID1,
                                                        customer = -1
                                                    };
                                                    await sdk.NewOrder(sellitem, token);

                                                    db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                    {
                                                        UserID = itemContractse.UserID,
                                                        ContractID = itemContractse.ContractID2,
                                                        OrderSide = item.orderSide,
                                                        Price = item.price,
                                                        quantity = item.quantity,
                                                        DateTime = DateTime.Now,

                                                        Contract1Price = Convert.ToInt32(bidPrice1_Contract1),
                                                        Contract1Valum = bidVolume1_Contract1,
                                                        contract2price = Convert.ToInt32(askPrice1_Contract2),
                                                        Contract2Valum = askVolume1_Contract2,
                                                    });


                                                    db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                    {
                                                        UserID = itemContractse.UserID,
                                                        ContractID = itemContractse.ContractID1,
                                                        OrderSide = sellitem.orderSide,
                                                        Price = sellitem.price,
                                                        quantity = sellitem.quantity,
                                                        DateTime = DateTime.Now,
                                                        Contract1Price = Convert.ToInt32(bidPrice1_Contract1),
                                                        Contract1Valum = bidVolume1_Contract1,
                                                        contract2price = Convert.ToInt32(askPrice1_Contract2),
                                                        Contract2Valum = askVolume1_Contract2,
                                                    });
                                                }
                                                //انجام معامله خرید 
                                               
                                                itemContractse.ContractCountActive = itemContractse.ContractCountActive - 2;
                                                if (itemContractse.ContractCountActive < 2)
                                                {
                                                    itemContractse.IsActive = false;
                                                }
                                            }
                                            db.UserContractses.AddOrUpdate(itemContractse);


                                        }

                                        break;
                                    }
                                case ContractType.UpsideDown:
                                    {
                                        //آیا در قرار داد هست ؟
                                        //در قرار داد هست

                                        if (GetPosition.Result.Response.Where(p => p.ContractId == itemContractse.ContractID1 || p.ContractId == itemContractse.ContractID2).Any())
                                        {


                                            //        //آیا  به مبلغ سغف برای اف ست رسیده است؟
                                            if ((askPrice1_Contract2 - bidPrice1_Contract1) <= itemContractse.Min&&(askVolume1_Contract2>0&&bidVolume1_Contract1>0))
                                            {
                                                int q = NumberUtility.ReternMin(
                                                            NumberUtility.ReternMin(bidVolume1_Contract1,
                                                                askVolume1_Contract2),
                                                            GetPosition.Result.Response
                                                                .Where(p => p.ContractId == itemContractse.ContractID1)
                                                                .SingleOrDefault().Buy) > 1
                                                    ? NumberUtility.ReternMin(
                                                          NumberUtility.ReternMin(bidVolume1_Contract1,
                                                              askVolume1_Contract2),
                                                          GetPosition.Result.Response
                                                              .Where(p => p.ContractId == itemContractse.ContractID1)
                                                              .SingleOrDefault().Buy) / 2
                                                    : 1;
                                                if (Contract1.Response.tradesVolume<=Contract2.Response.tradesVolume)
                                                {
                                                    NewOrderViewModel item = new NewOrderViewModel()
                                                    {
                                                        customer = -1,
                                                        contract = itemContractse.ContractID1,
                                                        orderSide = orderSide.Cells,
                                                        price = Convert.ToInt32(bidPrice1_Contract1),
                                                        quantity = q,

                                                    };
                                                    await sdk.NewOrder(item, token);

                                                    NewOrderViewModel itemcell = new NewOrderViewModel()
                                                    {
                                                        customer = -1,
                                                        contract = itemContractse.ContractID2,
                                                        orderSide = orderSide.Bye,
                                                        price = Convert.ToInt32(askPrice1_Contract2),
                                                        quantity = item.quantity
                                                    };
                                                    await sdk.NewOrder(itemcell, token);
                                                    db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                    {
                                                        UserID = itemContractse.UserID,
                                                        ContractID = itemContractse.ContractID1,
                                                        OrderSide = item.orderSide,
                                                        Price = item.price,
                                                        quantity = item.quantity,
                                                        DateTime = DateTime.Now,
                                                        Contract1Price = Convert.ToInt32(bidPrice1_Contract1),
                                                        Contract1Valum = bidVolume1_Contract1,
                                                        contract2price = Convert.ToInt32(askPrice1_Contract2),
                                                        Contract2Valum = askVolume1_Contract2,


                                                    });
                                                    db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                    {
                                                        UserID = itemContractse.UserID,
                                                        ContractID = itemContractse.ContractID2,
                                                        OrderSide = item.orderSide,
                                                        Price = item.price,
                                                        quantity = item.quantity,
                                                        DateTime = DateTime.Now,
                                                        Contract1Price = Convert.ToInt32(bidPrice1_Contract1),
                                                        Contract1Valum = bidVolume1_Contract1,
                                                        contract2price = Convert.ToInt32(askPrice1_Contract2),
                                                        Contract2Valum = askVolume1_Contract2,
                                                    });
                                                }
                                                else
                                                {
                                                   

                                                    NewOrderViewModel itemcell = new NewOrderViewModel()
                                                    {
                                                        customer = -1,
                                                        contract = itemContractse.ContractID2,
                                                        orderSide = orderSide.Bye,
                                                        price = Convert.ToInt32(askPrice1_Contract2),
                                                        quantity = q
                                                    };
                                                    await sdk.NewOrder(itemcell, token);
                                                    NewOrderViewModel item = new NewOrderViewModel()
                                                    {
                                                        customer = -1,
                                                        contract = itemContractse.ContractID1,
                                                        orderSide = orderSide.Cells,
                                                        price = Convert.ToInt32(bidPrice1_Contract1),
                                                        quantity =itemcell.quantity ,

                                                    };
                                                    await sdk.NewOrder(item, token);
                                                    db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                    {
                                                        UserID = itemContractse.UserID,
                                                        ContractID = itemContractse.ContractID1,
                                                        OrderSide = item.orderSide,
                                                        Price = item.price,
                                                        quantity = item.quantity,
                                                        DateTime = DateTime.Now,
                                                        Contract1Price = Convert.ToInt32(bidPrice1_Contract1),
                                                        Contract1Valum = bidVolume1_Contract1,
                                                        contract2price = Convert.ToInt32(askPrice1_Contract2),
                                                        Contract2Valum = askVolume1_Contract2,


                                                    });
                                                    db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                    {
                                                        UserID = itemContractse.UserID,
                                                        ContractID = itemContractse.ContractID2,
                                                        OrderSide = item.orderSide,
                                                        Price = item.price,
                                                        quantity = item.quantity,
                                                        DateTime = DateTime.Now,
                                                        Contract1Price = Convert.ToInt32(bidPrice1_Contract1),
                                                        Contract1Valum = bidVolume1_Contract1,
                                                        contract2price = Convert.ToInt32(askPrice1_Contract2),
                                                        Contract2Valum = askVolume1_Contract2,
                                                    });
                                                }
                                               
                                                itemContractse.ContractCountActive = itemContractse.ContractCountActive - 2;
                                                if (itemContractse.ContractCountActive < 2)
                                                {
                                                    itemContractse.IsActive = false;

                                                }
                                                db.UserContractses.AddOrUpdate(itemContractse);


                                                //            //انجام معامله افست
                                            }
                                            //        //آیا به سقف تعداد خرید و فروش رسیده است ؟
                                            var pos = sdk.OpenPosstions(token).Result.Response;
                                            if (itemContractse.ContContract > pos.Where(p => p.contractId == itemContractse.ContractID1).SingleOrDefault().buy && itemContractse.ContContract > pos.Where(p => p.contractId == itemContractse.ContractID2).SingleOrDefault().sell && itemContractse.ContractCountActive > 1)
                                            {
                                                //            //آیا به مبلغ  کف خرید رسیده است ؟
                                                if ((bidPrice1_Contract2 - askPrice1_Contract1) >= itemContractse.Max&&(bidVolume1_Contract2>0&&askVolume1_Contract1>0))
                                                {
                                                    //            //عملیات لغو قرار داد های در حال انتظار 
                                                    var pending = sdk.pendingOrders(token);
                                                    foreach (var VARIABLE in pending.Result.Response.rows.Where(p => p.contractId == itemContractse.ContractID1 || p.contractId == itemContractse.ContractID2))
                                                    {
                                                        foreach (var itemsss in VARIABLE.orderItems)
                                                        {
                                                            await sdk.DeleteOrder(new DeleteOrderViewModel()
                                                            {
                                                                customer = -1,
                                                                orderItemId = Convert.ToInt32(itemsss.id),
                                                            }, token);
                                                        }

                                                        itemContractse.ContractCountActive += 1;

                                                    }
                                                    db.UserContractses.AddOrUpdate(itemContractse);
                                                    int q = NumberUtility.ReternMin(
                                                                NumberUtility.ReternMin(bidVolume1_Contract2,
                                                                    askVolume1_Contract1),
                                                                itemContractse.ContContract -
                                                                pos.Where(p =>
                                                                        p.contractId == itemContractse.ContractID2)
                                                                    .SingleOrDefault().sell) > 1
                                                        ? NumberUtility.ReternMin(
                                                              NumberUtility.ReternMin(bidVolume1_Contract2,
                                                                  askVolume1_Contract1),
                                                              itemContractse.ContContract -
                                                              pos.Where(p => p.contractId == itemContractse.ContractID2)
                                                                  .SingleOrDefault().sell) / 2
                                                        : 1;
                                                    if (Contract1.Response.tradesVolume<=Contract2.Response.tradesVolume)
                                                    {
                                                        NewOrderViewModel sellitem = new NewOrderViewModel()
                                                        {
                                                            quantity = q,
                                                            orderSide = orderSide.Bye,
                                                            price = Convert.ToInt32(askPrice1_Contract1),
                                                            contract = itemContractse.ContractID1,
                                                            customer = -1
                                                        };
                                                        await sdk.NewOrder(sellitem, token);
                                                        NewOrderViewModel item = new NewOrderViewModel()
                                                        {
                                                            quantity = sellitem.quantity,
                                                            orderSide = orderSide.Cells,
                                                            price = Convert.ToInt32(bidPrice1_Contract2),
                                                            contract = itemContractse.ContractID2,
                                                            customer = -1

                                                        };
                                                        await sdk.NewOrder(item, token);



                                                        db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                        {
                                                            UserID = itemContractse.UserID,
                                                            ContractID = itemContractse.ContractID2,
                                                            OrderSide = item.orderSide,
                                                            Price = item.price,
                                                            quantity = item.quantity,
                                                            DateTime = DateTime.Now,
                                                            Contract1Price = Convert.ToInt32(askPrice1_Contract1),
                                                            Contract1Valum = askVolume1_Contract1,
                                                            contract2price = Convert.ToInt32(bidPrice1_Contract2),
                                                            Contract2Valum = bidVolume1_Contract2,

                                                        });

                                                        db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                        {
                                                            UserID = itemContractse.UserID,
                                                            ContractID = itemContractse.ContractID2,
                                                            OrderSide = sellitem.orderSide,
                                                            Price = sellitem.price,
                                                            quantity = sellitem.quantity,
                                                            DateTime = DateTime.Now,
                                                            Contract1Price = Convert.ToInt32(askPrice1_Contract1),
                                                            Contract1Valum = askVolume1_Contract1,
                                                            contract2price = Convert.ToInt32(bidPrice1_Contract2),
                                                            Contract2Valum = bidVolume1_Contract2,

                                                        });
                                                    }
                                                    else
                                                    {
                                                       
                                                        NewOrderViewModel item = new NewOrderViewModel()
                                                        {
                                                            quantity = q,
                                                            orderSide = orderSide.Cells,
                                                            price = Convert.ToInt32(bidPrice1_Contract2),
                                                            contract = itemContractse.ContractID2,
                                                            customer = -1

                                                        };
                                                        await sdk.NewOrder(item, token);
                                                        NewOrderViewModel sellitem = new NewOrderViewModel()
                                                        {
                                                            quantity = item.quantity,
                                                            orderSide = orderSide.Bye,
                                                            price = Convert.ToInt32(askPrice1_Contract1),
                                                            contract = itemContractse.ContractID1,
                                                            customer = -1
                                                        };
                                                        await sdk.NewOrder(sellitem, token);


                                                        db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                        {
                                                            UserID = itemContractse.UserID,
                                                            ContractID = itemContractse.ContractID2,
                                                            OrderSide = item.orderSide,
                                                            Price = item.price,
                                                            quantity = item.quantity,
                                                            DateTime = DateTime.Now,
                                                            Contract1Price = Convert.ToInt32(askPrice1_Contract1),
                                                            Contract1Valum = askVolume1_Contract1,
                                                            contract2price = Convert.ToInt32(bidPrice1_Contract2),
                                                            Contract2Valum = bidVolume1_Contract2,

                                                        });

                                                        db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                        {
                                                            UserID = itemContractse.UserID,
                                                            ContractID = itemContractse.ContractID2,
                                                            OrderSide = sellitem.orderSide,
                                                            Price = sellitem.price,
                                                            quantity = sellitem.quantity,
                                                            DateTime = DateTime.Now,
                                                            Contract1Price = Convert.ToInt32(askPrice1_Contract1),
                                                            Contract1Valum = askVolume1_Contract1,
                                                            contract2price = Convert.ToInt32(bidPrice1_Contract2),
                                                            Contract2Valum = bidVolume1_Contract2,

                                                        });
                                                    }
                                                    //                //انجام معامله خرید 
                                                   
                                                    itemContractse.ContractCountActive = itemContractse.ContractCountActive - 2;
                                                    if (itemContractse.ContractCountActive < 2)
                                                    {
                                                        itemContractse.IsActive = false;
                                                    }
                                                    db.UserContractses.AddOrUpdate(itemContractse);


                                                }



                                            }

                                        }
                                        //     //در قرار داد نیست 
                                        else
                                        {
                                            var pending = sdk.pendingOrders(token);
                                            //        //عملیات لغو قرار داد های در حال انتظار 
                                            foreach (var VARIABLE in pending.Result.Response.rows.Where(p => p.contractId == itemContractse.ContractID1 || p.contractId == itemContractse.ContractID2))
                                            {
                                                foreach (var itemsss in VARIABLE.orderItems)
                                                {
                                                    await sdk.DeleteOrder(new DeleteOrderViewModel()
                                                    {
                                                        customer = -1,
                                                        orderItemId = Convert.ToInt32(itemsss.id),
                                                    }, token);
                                                }

                                                itemContractse.ContractCountActive += 1;
                                            }

                                            //        //آیا به کف مبلغ برای خرید  رسیده  است؟
                                            if ((bidPrice1_Contract2 - askPrice1_Contract1) >= itemContractse.Max&&(bidVolume1_Contract2>0&&askVolume1_Contract1>0))
                                            {
                                                int q =
                                                    NumberUtility.ReternMin(
                                                        NumberUtility.ReternMin(bidVolume1_Contract2,
                                                            askVolume1_Contract1), itemContractse.ContContract) > 1
                                                        ? NumberUtility.ReternMin(
                                                              NumberUtility.ReternMin(bidVolume1_Contract2,
                                                                  askVolume1_Contract1), itemContractse.ContContract) /
                                                          2
                                                        : 1;
                                                if (Contract1.Response.tradesVolume<=Contract2.Response.tradesVolume)
                                                {
                                                    NewOrderViewModel sellitem = new NewOrderViewModel()
                                                    {
                                                        quantity = q,
                                                        orderSide = orderSide.Bye,
                                                        price = Convert.ToInt32(askPrice1_Contract1),
                                                        contract = itemContractse.ContractID1,
                                                        customer = -1
                                                    };
                                                    await sdk.NewOrder(sellitem, token);
                                                    NewOrderViewModel item = new NewOrderViewModel()
                                                    {
                                                        quantity = sellitem.quantity,
                                                        orderSide = orderSide.Cells,
                                                        price = Convert.ToInt32(bidPrice1_Contract2),
                                                        contract = itemContractse.ContractID2,
                                                        customer = -1,
                                                        //marketOrder = true
                                                    };
                                                    await sdk.NewOrder(item, token);



                                                    db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                    {
                                                        UserID = itemContractse.UserID,
                                                        ContractID = itemContractse.ContractID2,
                                                        OrderSide = item.orderSide,
                                                        Price = item.price,
                                                        quantity = item.quantity,
                                                        DateTime = DateTime.Now,

                                                        Contract1Price = Convert.ToInt32(askPrice1_Contract1),
                                                        Contract1Valum = askVolume1_Contract1,
                                                        contract2price = Convert.ToInt32(bidPrice1_Contract2),
                                                        Contract2Valum = bidVolume1_Contract2,
                                                    });


                                                    db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                    {
                                                        UserID = itemContractse.UserID,
                                                        ContractID = itemContractse.ContractID1,
                                                        OrderSide = sellitem.orderSide,
                                                        Price = sellitem.price,
                                                        quantity = sellitem.quantity,
                                                        DateTime = DateTime.Now,
                                                        Contract1Price = Convert.ToInt32(askPrice1_Contract1),
                                                        Contract1Valum = askVolume1_Contract1,
                                                        contract2price = Convert.ToInt32(bidPrice1_Contract2),
                                                        Contract2Valum = bidVolume1_Contract2,
                                                    });
                                                }
                                                else
                                                {
                                                   
                                                    NewOrderViewModel item = new NewOrderViewModel()
                                                    {
                                                        quantity = q,
                                                        orderSide = orderSide.Cells,
                                                        price = Convert.ToInt32(bidPrice1_Contract2),
                                                        contract = itemContractse.ContractID2,
                                                        customer = -1,
                                                        //marketOrder = true
                                                    };
                                                    await sdk.NewOrder(item, token);
                                                    NewOrderViewModel sellitem = new NewOrderViewModel()
                                                    {
                                                        quantity =item.quantity ,
                                                        orderSide = orderSide.Bye,
                                                        price = Convert.ToInt32(askPrice1_Contract1),
                                                        contract = itemContractse.ContractID1,
                                                        customer = -1
                                                    };
                                                    await sdk.NewOrder(sellitem, token);


                                                    db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                    {
                                                        UserID = itemContractse.UserID,
                                                        ContractID = itemContractse.ContractID2,
                                                        OrderSide = item.orderSide,
                                                        Price = item.price,
                                                        quantity = item.quantity,
                                                        DateTime = DateTime.Now,

                                                        Contract1Price = Convert.ToInt32(askPrice1_Contract1),
                                                        Contract1Valum = askVolume1_Contract1,
                                                        contract2price = Convert.ToInt32(bidPrice1_Contract2),
                                                        Contract2Valum = bidVolume1_Contract2,
                                                    });


                                                    db.UsersOrderLogses.Add(new UsersOrderLogs()
                                                    {
                                                        UserID = itemContractse.UserID,
                                                        ContractID = itemContractse.ContractID1,
                                                        OrderSide = sellitem.orderSide,
                                                        Price = sellitem.price,
                                                        quantity = sellitem.quantity,
                                                        DateTime = DateTime.Now,
                                                        Contract1Price = Convert.ToInt32(askPrice1_Contract1),
                                                        Contract1Valum = askVolume1_Contract1,
                                                        contract2price = Convert.ToInt32(bidPrice1_Contract2),
                                                        Contract2Valum = bidVolume1_Contract2,
                                                    });
                                                }
                                                //انجام معامله خرید 
                                              
                                                itemContractse.ContractCountActive = itemContractse.ContractCountActive - 2;
                                                if (itemContractse.ContractCountActive < 2)
                                                {
                                                    itemContractse.IsActive = false;
                                                }
                                            }
                                            db.UserContractses.AddOrUpdate(itemContractse);


                                        }

                                        break;
                                    }
                                case ContractType.ByCoefficient:
                                    {
                                        break;
                                    }
                            }
                        }
                    }

                   

                   
                    db.SaveChanges();
                    db.Dispose();
                }

               
            }

        }
    }
}