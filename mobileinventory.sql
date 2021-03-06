USE [master]
GO
/****** Object:  Database [DB17]    Script Date: 5/5/2019 12:59:46 PM ******/
CREATE DATABASE [DB17]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DB17', FILENAME = N'E:\SQL\MSSQL12.MSSQLSERVER\MSSQL\DATA\DB17.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DB17_log', FILENAME = N'E:\SQL\MSSQL12.MSSQLSERVER\MSSQL\DATA\DB17_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DB17] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DB17].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DB17] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DB17] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DB17] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DB17] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DB17] SET ARITHABORT OFF 
GO
ALTER DATABASE [DB17] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DB17] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DB17] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DB17] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DB17] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DB17] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DB17] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DB17] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DB17] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DB17] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DB17] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DB17] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DB17] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DB17] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DB17] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DB17] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DB17] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DB17] SET RECOVERY FULL 
GO
ALTER DATABASE [DB17] SET  MULTI_USER 
GO
ALTER DATABASE [DB17] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DB17] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DB17] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DB17] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DB17] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DB17', N'ON'
GO
USE [DB17]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 5/5/2019 12:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[AdminID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[EmailAddress] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AdminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CartReport]    Script Date: 5/5/2019 12:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CartReport](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[details] [varchar](max) NOT NULL,
 CONSTRAINT [PK_CartReport] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Category]    Script Date: 5/5/2019 12:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
	[CategoryImage] [nvarchar](max) NOT NULL,
	[CategoryFK] [int] NULL,
	[CategoryStatus] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customer]    Script Date: 5/5/2019 12:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[EmailAddress] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[HomeAdress] [nvarchar](50) NULL,
	[DateOfBirth] [datetime] NULL,
	[ContactNumber] [nvarchar](50) NOT NULL,
	[UserImage] [nvarchar](max) NULL,
	[Added On] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[EmailAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ContactNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ContactNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 5/5/2019 12:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[ProductImage] [nvarchar](max) NOT NULL,
	[ProductDescription] [nvarchar](max) NOT NULL,
	[ProductPrice] [int] NOT NULL,
	[ProductStatus] [int] NULL,
	[ProdctFK_admin] [int] NULL,
	[ProdctFK_customer] [int] NULL,
	[ProdctFK_category] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Report1]    Script Date: 5/5/2019 12:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Report1](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[details] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Report1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_invoice]    Script Date: 5/5/2019 12:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_invoice](
	[in_id] [int] IDENTITY(1,1) NOT NULL,
	[in_fk_Customer] [int] NULL,
	[in_date] [datetime] NOT NULL,
	[in_totalbill] [float] NULL,
 CONSTRAINT [PK__tbl_invo__1CD08BE944847DC3] PRIMARY KEY CLUSTERED 
(
	[in_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_order]    Script Date: 5/5/2019 12:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_order](
	[o_id] [int] IDENTITY(1,1) NOT NULL,
	[o_fk_Product] [int] NULL,
	[o_fk_invoice] [int] NULL,
	[o_date] [datetime] NOT NULL,
	[o_qty] [int] NULL,
	[o_bill] [float] NULL,
	[o_unitprice] [int] NULL,
 CONSTRAINT [PK__tbl_orde__904BC20E98F03AD1] PRIMARY KEY CLUSTERED 
(
	[o_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[dailyrecord]    Script Date: 5/5/2019 12:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[dailyrecord] as SELECT tbl_order.o_id,Customer.UserName,Product.ProductName,tbl_order.o_qty,cast (tbl_order.o_date as date) as orderdate  FROM Customer join tbl_invoice on Customer.CustomerID=tbl_invoice.in_fk_Customer join tbl_order on tbl_invoice.in_id=tbl_order.o_fk_invoice join Product on tbl_order.o_fk_Product=Product.ProductID
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD FOREIGN KEY([CategoryFK])
REFERENCES [dbo].[Admin] ([AdminID])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([ProdctFK_admin])
REFERENCES [dbo].[Admin] ([AdminID])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([ProdctFK_customer])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([ProdctFK_category])
REFERENCES [dbo].[Category] ([CategoryID])
GO
ALTER TABLE [dbo].[tbl_invoice]  WITH CHECK ADD  CONSTRAINT [FK__tbl_invoi__in_fk__25869641] FOREIGN KEY([in_fk_Customer])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[tbl_invoice] CHECK CONSTRAINT [FK__tbl_invoi__in_fk__25869641]
GO
ALTER TABLE [dbo].[tbl_invoice]  WITH CHECK ADD  CONSTRAINT [FK__tbl_invoi__in_fk__267ABA7A] FOREIGN KEY([in_fk_Customer])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[tbl_invoice] CHECK CONSTRAINT [FK__tbl_invoi__in_fk__267ABA7A]
GO
ALTER TABLE [dbo].[tbl_order]  WITH CHECK ADD  CONSTRAINT [FK__tbl_order__o_fk___276EDEB3] FOREIGN KEY([o_fk_invoice])
REFERENCES [dbo].[tbl_invoice] ([in_id])
GO
ALTER TABLE [dbo].[tbl_order] CHECK CONSTRAINT [FK__tbl_order__o_fk___276EDEB3]
GO
ALTER TABLE [dbo].[tbl_order]  WITH CHECK ADD  CONSTRAINT [FK__tbl_order__o_fk___286302EC] FOREIGN KEY([o_fk_Product])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[tbl_order] CHECK CONSTRAINT [FK__tbl_order__o_fk___286302EC]
GO
ALTER TABLE [dbo].[tbl_order]  WITH CHECK ADD  CONSTRAINT [FK__tbl_order__o_fk___29572725] FOREIGN KEY([o_fk_invoice])
REFERENCES [dbo].[tbl_invoice] ([in_id])
GO
ALTER TABLE [dbo].[tbl_order] CHECK CONSTRAINT [FK__tbl_order__o_fk___29572725]
GO
ALTER TABLE [dbo].[tbl_order]  WITH CHECK ADD  CONSTRAINT [FK__tbl_order__o_fk___2A4B4B5E] FOREIGN KEY([o_fk_Product])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[tbl_order] CHECK CONSTRAINT [FK__tbl_order__o_fk___2A4B4B5E]
GO
ALTER TABLE [dbo].[tbl_order]  WITH CHECK ADD  CONSTRAINT [FK__tbl_order__o_fk___2B3F6F97] FOREIGN KEY([o_fk_invoice])
REFERENCES [dbo].[tbl_invoice] ([in_id])
GO
ALTER TABLE [dbo].[tbl_order] CHECK CONSTRAINT [FK__tbl_order__o_fk___2B3F6F97]
GO
/****** Object:  StoredProcedure [dbo].[CustomerOrder]    Script Date: 5/5/2019 12:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CustomerOrder] AS
BEGIN
SELECT tbl_order.o_id,Customer.UserName,Product.ProductName,tbl_order.o_qty,cast(tbl_order.o_date as date) as orderdate FROM Customer join tbl_invoice on Customer.CustomerID=tbl_invoice.in_fk_Customer join tbl_order on tbl_invoice.in_id=tbl_order.o_fk_invoice join Product on tbl_order.o_fk_Product=Product.ProductID
END
GO
/****** Object:  StoredProcedure [dbo].[DailyReport]    Script Date: 5/5/2019 12:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DailyReport]  @date Datetime AS
BEGIN
SELECT * FROM dailyrecord where orderdate =CAST(@date as date)
END
GO
/****** Object:  StoredProcedure [dbo].[ProductReport]    Script Date: 5/5/2019 12:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductReport] AS
BEGIN
SELECT Product.ProductID,Product.ProductName,Product.ProductDescription,Category.CategoryName,Product.ProductPrice FROM Product join Category on Product.ProdctFK_category=Category.CategoryID
END
GO
USE [master]
GO
ALTER DATABASE [DB17] SET  READ_WRITE 
GO
