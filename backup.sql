USE [master]
GO

/****** Object:  Database [TourFirm]    Script Date: 30.10.2021 19:16:19 ******/
CREATE DATABASE [TourFirm]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TourFirm', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TourFirm.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TourFirm_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TourFirm_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TourFirm].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [TourFirm] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [TourFirm] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [TourFirm] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [TourFirm] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [TourFirm] SET ARITHABORT OFF 
GO

ALTER DATABASE [TourFirm] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [TourFirm] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [TourFirm] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [TourFirm] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [TourFirm] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [TourFirm] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [TourFirm] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [TourFirm] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [TourFirm] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [TourFirm] SET  DISABLE_BROKER 
GO

ALTER DATABASE [TourFirm] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [TourFirm] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [TourFirm] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [TourFirm] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [TourFirm] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [TourFirm] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [TourFirm] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [TourFirm] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [TourFirm] SET  MULTI_USER 
GO

ALTER DATABASE [TourFirm] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [TourFirm] SET DB_CHAINING OFF 
GO

ALTER DATABASE [TourFirm] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [TourFirm] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [TourFirm] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [TourFirm] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [TourFirm] SET QUERY_STORE = OFF
GO

ALTER DATABASE [TourFirm] SET  READ_WRITE 
GO

