namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _980828 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContractCellslogs",
                c => new
                    {
                        contractId = c.Int(nullable: false, identity: true),
                        symbol = c.String(),
                        tickSize = c.Double(nullable: false),
                        askVolume1 = c.Int(nullable: false),
                        askPrice1 = c.Double(nullable: false),
                        askVolume2 = c.Int(nullable: false),
                        askPrice2 = c.Double(nullable: false),
                        askVolume3 = c.Int(nullable: false),
                        askPrice3 = c.Double(nullable: false),
                        bidVolume1 = c.Int(nullable: false),
                        bidPrice1 = c.Double(nullable: false),
                        bidVolume2 = c.Int(nullable: false),
                        bidPrice2 = c.Double(nullable: false),
                        bidVolume3 = c.Int(nullable: false),
                        bidPrice3 = c.Double(nullable: false),
                        firstTradedPrice = c.Double(nullable: false),
                        lowTradedPrice = c.Double(nullable: false),
                        highTradedPrice = c.Double(nullable: false),
                        lastTradedPrice = c.Double(nullable: false),
                        lastTradingPDateTime = c.String(),
                        ordersPDateTime = c.String(),
                        firstTradedPDateTime = c.String(),
                        lastTradedPDateTime = c.String(),
                        lot = c.Double(nullable: false),
                        initialMargin = c.Double(nullable: false),
                        requiredMargin = c.Double(nullable: false),
                        tradesVolume = c.Int(nullable: false),
                        tradesValue = c.Double(nullable: false),
                        openInterests = c.Int(nullable: false),
                        openInterestsChanges = c.Int(nullable: false),
                        maxOrderSize = c.Double(nullable: false),
                        minPricePercent = c.Double(nullable: false),
                        maxPricePercent = c.Double(nullable: false),
                        PriceType = c.Int(nullable: false),
                        settlementsPrice = c.Double(nullable: false),
                        settlementsDate = c.String(),
                        lastWorkingSettlementPrice = c.Double(nullable: false),
                        realtimePrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.contractId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ContractCellslogs");
        }
    }
}
