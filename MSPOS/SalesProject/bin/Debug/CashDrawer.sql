USE [MSPOS]
GO

/****** Object:  Table [dbo].[CashDrawer_table]    Script Date: 11/19/2013 17:10:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CashDrawer_table](
	[Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[StartingAmt] [numeric](18, 2) NULL,
	[ResetDate] [datetime] NULL
) ON [PRIMARY]

GO


