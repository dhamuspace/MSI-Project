USE [MSPOS]
GO

/****** Object:  Table [dbo].[ResetDrawer_table]    Script Date: 11/29/2013 11:42:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ResetDrawer_table](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Coin_100] [numeric](18, 0) NULL,
	[Coin_50] [numeric](18, 0) NULL,
	[Coin_20] [numeric](18, 0) NULL,
	[Coin_10] [numeric](18, 0) NULL,
	[Coin_5] [numeric](18, 0) NULL,
	[Coin_2] [numeric](18, 0) NULL,
	[Coin_1] [numeric](18, 0) NULL,
	[Coin_P50] [numeric](18, 0) NULL,
	[Coin_P25] [numeric](18, 0) NULL,
	[Coin_P10] [numeric](18, 0) NULL,
	[Coin_P05] [numeric](18, 0) NULL,
	[Coin_P01] [numeric](18, 0) NULL,
	[Tot_coin] [numeric](18, 0) NULL,
	[Tot_amt] [numeric](18, 2) NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ResetDrawer_table] ADD  CONSTRAINT [DF_ResetDrawer_table_Coin_100]  DEFAULT ((0)) FOR [Coin_100]
GO


