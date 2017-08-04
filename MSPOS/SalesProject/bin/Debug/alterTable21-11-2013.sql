USE [MSPOS]
GO

/****** Object:  Table [dbo].[Stockadjmas_table]    Script Date: 11/21/2013 15:53:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Stockadjmas_table](
	[stckA_id] [int] IDENTITY(1,1) NOT NULL,
	[stck_adj_no] [int] NULL,
	[stckA_invoiceNo] [varchar](50) NULL,
	[stck_date] [datetime] NULL,
	[stck_time] [datetime] NULL,
	[StckA_code] [varchar](50) NULL,
	[stck_ctrno] [int] NULL,
	[stckA_CtrName] [varchar](50) NULL,
	[stckA_Name] [varchar](50) NULL,
	[stckA_Unit] [varchar](50) NULL,
	[stck_remarks] [varchar](50) NULL,
	[stckA_lesQty] [int] NULL,
	[stckA_addQty] [int] NULL,
	[stckA_Rate] [numeric](18, 2) NULL,
	[stckA_Amt] [numeric](18, 2) NULL,
	[stck_lessTXqty] [int] NULL,
	[stck_cancel] [bit] NULL,
	[stck_lessNTqty] [int] NULL,
	[stck_addTXqty] [int] NULL,
	[stck_addNTqty] [int] NULL,
	[stck_addamt] [numeric](18, 2) NULL,
	[stck_lessamt] [numeric](18, 2) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO





SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Item_Grouptable](
	[Item_groupno] [int] NULL,
	[Item_groupname] [nvarchar](40) NULL,
	[Item_groupmtname] [nvarchar](40) NULL,
	[Item_grouplevel] [int] NULL,
	[Item_groupunder] [int] NULL,
	[Item_Commodity] [nvarchar](255) NULL,
	[Item_groupgno] [smallint] NULL,
	[Item_groupflag] [smallint] NULL,
	[Std_Group] [bit] NULL,
	[GroupPos] [int] NULL,
	[SSMA_TimeStamp] [timestamp] NOT NULL,
	[Items_Image] [image] NULL,
	[Group_Color] [nvarchar](50) NULL,
	[Group_visibility] [nvarchar](50) NULL,
	[Font_Color] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO




