
/****** Object:  Table [dbo].[Tb_Order]    Script Date: 07/12/2018 18:04:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tb_Order](
	[ID] [varchar](255) NOT NULL,
	[SchoolName] [nvarchar](max) NULL,
	[SchoollID] [int] NULL,
	[AdminAccount] [varchar](50) NULL,
	[Phone] [varchar](500) NULL,
	[SchemeID] [int] NULL,
	[SchemeName] [nvarchar](100) NULL,
	[OrderMon] [int] NULL,
	[SchemeNum] [int] NULL,
	[AccessNum] [int] NULL,
	[SchemeMoney] [money] NULL,
	[OptType] [int] NULL,
	[SchemeStateNm] [nvarchar](50) NULL,
	[SchemeTypeID] [int] NULL,
	[SchemeTypeName] [nvarchar](50) NULL,
	[SchemeNo] [varchar](255) NULL,
	[SchemeDate] [datetime] NULL,
	[Ecordercode] [varchar](50) NULL,
	[Trial] [int] NULL,
	[BossOrderID] [varchar](max) NULL,
	[CustID] [bigint] NULL,
	[CustCode] [varchar](50) NULL,
	[CustType] [int] NULL,
	[RegisterSource] [int] NULL,
	[Email] [varchar](50) NULL,
	[ProductCode] [varchar](50) NULL,
	[BeginTime] [bigint] NULL,
	[EndTime] [bigint] NULL,
	[CustName] [varchar](255) NULL,
	[UserID] [varchar](50) NULL,
	[UserName] [varchar](50) NULL,
	[UserLias] [nvarchar](255) NULL,
	[Produectparas] [nvarchar](max) NULL,
 CONSTRAINT [PK_TB_ORDER] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_UserScheme]    Script Date: 07/12/2018 18:04:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tb_UserScheme](
	[UserID] [bigint] NOT NULL,
	[ID] [varchar](255) NULL,
	[UserName] [varchar](50) NULL,
	[Useralias] [nvarchar](50) NULL,
	[Mobile] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[BeginTime] [bigint] NULL,
	[EndTime] [bigint] NULL,
	[ApplyNo] [varchar](255) NULL,
	[EcorderCode] [varchar](255) NULL,
	[CustID] [bigint] NULL,
	[ProductCode] [varchar](50) NULL,
	[OptType] [int] NULL,
	[IsDelete] [int] NULL,
	[Paras] [nvarchar](max) NULL,
	[UserType] [int] NULL,
 CONSTRAINT [PK_TB_USERSCHEME] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF__Tb_Order__Access__15A53433]    Script Date: 07/12/2018 18:04:25 ******/
ALTER TABLE [dbo].[Tb_Order] ADD  DEFAULT ((0)) FOR [AccessNum]
GO
/****** Object:  ForeignKey [FK_TB_USERS_REFERENCE_TB_ORDER]    Script Date: 07/12/2018 18:04:25 ******/
ALTER TABLE [dbo].[Tb_UserScheme]  WITH CHECK ADD  CONSTRAINT [FK_TB_USERS_REFERENCE_TB_ORDER] FOREIGN KEY([ID])
REFERENCES [dbo].[Tb_Order] ([ID])
GO
ALTER TABLE [dbo].[Tb_UserScheme] CHECK CONSTRAINT [FK_TB_USERS_REFERENCE_TB_ORDER]
GO
