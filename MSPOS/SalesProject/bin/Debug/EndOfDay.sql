USE [MSPOS]
GO

/****** Object:  Table [dbo].[EndOfDay_Table]    Script Date: 11/19/2013 19:49:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EndOfDay_Table](
	[Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[EndOfDay] [datetime] NULL
) ON [PRIMARY]

GO


