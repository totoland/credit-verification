/*** USE [2C2P] ***/
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CREDIT_CARD](
	[CardNumber] [nvarchar](16) NOT NULL,
	[IsActive] [bit] NULL,
	[ReMark] [nvarchar](max) NULL,
 CONSTRAINT [PK_CREDIT_CARD] PRIMARY KEY CLUSTERED 
(
	[CardNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

