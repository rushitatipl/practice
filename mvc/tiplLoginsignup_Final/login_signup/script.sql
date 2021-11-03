USE [tiplUser]
GO

/****** Object:  Table [dbo].[logandreg]    Script Date: 03-11-2021 15:01:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[logandreg](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Gender] [varchar](50) NULL,
	[Firstname] [varchar](50) NULL,
	[Lastname] [varchar](50) NULL,
	[Mobile] [varchar](50) NULL,
	[Email] [varchar](254) NULL,
	[Password] [nvarchar](max) NULL,
	[Cpassword] [nvarchar](max) NULL,
	[Profile] [varchar](50) NULL,
	[IsEmailVerified] [bit] NULL,
	[Otp] [int] NULL,
	[ResetPasswordCode] [varchar](50) NULL,
 CONSTRAINT [PK_logandreg] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


