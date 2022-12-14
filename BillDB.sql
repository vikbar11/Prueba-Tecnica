USE [master]
GO
/****** Object:  Database [BillDB]    Script Date: 22/08/2022 7:59:13 a. m. ******/
CREATE DATABASE [BillDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BillDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\BillDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BillDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\BillDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BillDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BillDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BillDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BillDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BillDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BillDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BillDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [BillDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BillDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BillDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BillDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BillDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BillDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BillDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BillDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BillDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BillDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BillDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BillDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BillDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BillDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BillDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BillDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BillDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BillDB] SET RECOVERY FULL 
GO
ALTER DATABASE [BillDB] SET  MULTI_USER 
GO
ALTER DATABASE [BillDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BillDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BillDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BillDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BillDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BillDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'BillDB', N'ON'
GO
ALTER DATABASE [BillDB] SET QUERY_STORE = OFF
GO
USE [BillDB]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 22/08/2022 7:59:13 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[IdBill] [int] IDENTITY(1,1) NOT NULL,
	[IdCustomer] [int] NOT NULL,
	[BillDate] [date] NOT NULL,
 CONSTRAINT [PK_Bill] PRIMARY KEY CLUSTERED 
(
	[IdBill] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillDetail]    Script Date: 22/08/2022 7:59:13 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillDetail](
	[IdBillDetail] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NULL,
	[IdBill] [int] NOT NULL,
	[IdProduct] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_BillDetail] PRIMARY KEY CLUSTERED 
(
	[IdBillDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 22/08/2022 7:59:13 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[IdCustomer] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[BornDay] [date] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[IdCustomer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventory]    Script Date: 22/08/2022 7:59:13 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventory](
	[IdInventory] [int] IDENTITY(1,1) NOT NULL,
	[IdProduct] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Inventory] PRIMARY KEY CLUSTERED 
(
	[IdInventory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 22/08/2022 7:59:13 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[IdProduct] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](50) NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[IdProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK_Bill_Customer] FOREIGN KEY([IdCustomer])
REFERENCES [dbo].[Customer] ([IdCustomer])
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK_Bill_Customer]
GO
ALTER TABLE [dbo].[BillDetail]  WITH CHECK ADD  CONSTRAINT [FK_BillDetail_Bill] FOREIGN KEY([IdBill])
REFERENCES [dbo].[Bill] ([IdBill])
GO
ALTER TABLE [dbo].[BillDetail] CHECK CONSTRAINT [FK_BillDetail_Bill]
GO
ALTER TABLE [dbo].[BillDetail]  WITH CHECK ADD  CONSTRAINT [FK_BillDetail_Product] FOREIGN KEY([IdProduct])
REFERENCES [dbo].[Product] ([IdProduct])
GO
ALTER TABLE [dbo].[BillDetail] CHECK CONSTRAINT [FK_BillDetail_Product]
GO
ALTER TABLE [dbo].[Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_Product] FOREIGN KEY([IdProduct])
REFERENCES [dbo].[Product] ([IdProduct])
GO
ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Inventory_Product]
GO
USE [master]
GO
ALTER DATABASE [BillDB] SET  READ_WRITE 
GO
