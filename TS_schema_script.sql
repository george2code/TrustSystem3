USE [master]
GO
/****** Object:  Database [TrustSystems]    Script Date: 08/05/2015 20:03:57 ******/
CREATE DATABASE [TrustSystems] ON  PRIMARY 
( NAME = N'TrustSystems', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\TrustSystems.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TrustSystems_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\TrustSystems_log.ldf' , SIZE = 12352KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TrustSystems] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TrustSystems].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TrustSystems] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [TrustSystems] SET ANSI_NULLS OFF
GO
ALTER DATABASE [TrustSystems] SET ANSI_PADDING OFF
GO
ALTER DATABASE [TrustSystems] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [TrustSystems] SET ARITHABORT OFF
GO
ALTER DATABASE [TrustSystems] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [TrustSystems] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [TrustSystems] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [TrustSystems] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [TrustSystems] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [TrustSystems] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [TrustSystems] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [TrustSystems] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [TrustSystems] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [TrustSystems] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [TrustSystems] SET  DISABLE_BROKER
GO
ALTER DATABASE [TrustSystems] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [TrustSystems] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [TrustSystems] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [TrustSystems] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [TrustSystems] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [TrustSystems] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [TrustSystems] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [TrustSystems] SET  READ_WRITE
GO
ALTER DATABASE [TrustSystems] SET RECOVERY FULL
GO
ALTER DATABASE [TrustSystems] SET  MULTI_USER
GO
ALTER DATABASE [TrustSystems] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [TrustSystems] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'TrustSystems', N'ON'
GO
USE [TrustSystems]
GO
/****** Object:  Table [dbo].[Templates]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Templates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Language] [int] NOT NULL,
	[Delay] [int] NOT NULL,
	[Subject] [nvarchar](250) NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Templates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RootCategories]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RootCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Slug] [nvarchar](255) NOT NULL,
	[Icon] [nvarchar](255) NULL,
 CONSTRAINT [PK_RootCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoggerSet]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoggerSet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [tinyint] NOT NULL,
	[Message] [nvarchar](250) NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_LoggerSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Country] [int] NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Companies]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Slug] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
	[Website] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[Country] [int] NULL,
	[CityId] [int] NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Zip] [nvarchar](max) NULL,
	[Logo] [nvarchar](max) NULL,
	[CategoryId] [int] NOT NULL,
	[IsOrderRequired] [bit] NOT NULL,
 CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[BirthDate] [datetime] NULL,
	[About] [nvarchar](max) NULL,
	[CityId] [int] NULL,
	[Avatar] [nvarchar](max) NULL,
	[Gender] [nvarchar](10) NULL,
	[ExternalAddress] [nvarchar](256) NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers] 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles] 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CompanyBoxes]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyBoxes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BoxType] [int] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Message] [nvarchar](100) NOT NULL,
	[Details] [nvarchar](250) NOT NULL,
	[CompaniesId] [int] NOT NULL,
	[Image] [nvarchar](max) NULL,
 CONSTRAINT [PK_CompanyBoxes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_CompaniesCompanyBox] ON [dbo].[CompanyBoxes] 
(
	[CompaniesId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AfsSettings]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AfsSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SenderName] [nvarchar](250) NOT NULL,
	[InviteFrequency] [tinyint] NOT NULL,
	[TemplateDelay] [int] NOT NULL,
	[TemplateSubject] [nvarchar](250) NULL,
	[TemplateBody] [nvarchar](max) NULL,
	[Companies_Id] [int] NOT NULL,
	[Template_Id] [int] NOT NULL,
 CONSTRAINT [PK_AfsSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_AfsCompanies] ON [dbo].[AfsSettings] 
(
	[Companies_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_AfsTemplate] ON [dbo].[AfsSettings] 
(
	[Template_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles] 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cities]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CountryId] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Contact_Id] [int] NOT NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_ContactCity] ON [dbo].[Cities] 
(
	[Contact_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Slug] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[RootCategoryId] [int] NOT NULL,
	[CompanyCategoryId] [int] NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_RootCategoryCategory] ON [dbo].[Categories] 
(
	[RootCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyUsers]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyUsers](
	[CompanyId] [int] NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[IsOwner] [tinyint] NOT NULL,
	[UserState] [int] NOT NULL,
 CONSTRAINT [PK_CompanyUsers] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reviews]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reviews](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[UserId] [nvarchar](max) NULL,
	[Rating] [int] NOT NULL,
	[OrderId] [nvarchar](max) NULL,
	[ReviewShort] [nvarchar](255) NOT NULL,
	[ReviewFull] [nvarchar](max) NOT NULL,
	[IsConfirmed] [bit] NOT NULL,
	[DateCreated] [datetime] NULL,
	[DatePublished] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
 CONSTRAINT [PK_Reviews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_CompanyReview] ON [dbo].[Reviews] 
(
	[CompanyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invitations]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invitations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompaniesId] [int] NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[SenderName] [nvarchar](100) NULL,
	[FromName] [nvarchar](100) NULL,
	[FromEmail] [nvarchar](100) NULL,
	[Status] [tinyint] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[SentAt] [datetime] NULL,
	[ReferenceId] [nvarchar](50) NOT NULL,
	[DeliveryDate] [datetime] NULL,
 CONSTRAINT [PK_Invitations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_CompaniesInvitation] ON [dbo].[Invitations] 
(
	[CompaniesId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReviewWarnings]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReviewWarnings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](max) NOT NULL,
	[Comment] [nvarchar](500) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[ReviewId] [int] NOT NULL,
 CONSTRAINT [PK_ReviewWarnings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_ReviewReviewWarning] ON [dbo].[ReviewWarnings] 
(
	[ReviewId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReviewUsefulls]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReviewUsefulls](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReviewId] [int] NOT NULL,
	[UserId] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ReviewUsefulls] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_ReviewReviewUsefull] ON [dbo].[ReviewUsefulls] 
(
	[ReviewId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReviewLikes]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReviewLikes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](max) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[ReviewId] [int] NOT NULL,
 CONSTRAINT [PK_ReviewLikes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_ReviewReviewLike] ON [dbo].[ReviewLikes] 
(
	[ReviewId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReviewComments]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReviewComments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReviewId] [int] NOT NULL,
	[UserId] [nvarchar](max) NOT NULL,
	[Comment] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ReviewComments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_ReviewReviewComment] ON [dbo].[ReviewComments] 
(
	[ReviewId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyCategory]    Script Date: 08/05/2015 20:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyCategory](
	[Categories_Id] [int] NOT NULL,
	[Companies_Id] [int] NOT NULL,
 CONSTRAINT [PK_CompanyCategory] PRIMARY KEY CLUSTERED 
(
	[Categories_Id] ASC,
	[Companies_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_CompanyCategory_Companies] ON [dbo].[CompanyCategory] 
(
	[Companies_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Default [DF__Companies__IsOrd__68687968]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[Companies] ADD  DEFAULT ((0)) FOR [IsOrderRequired]
GO
/****** Object:  Default [DF_AfsSettings_InviteFrequency]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[AfsSettings] ADD  CONSTRAINT [DF_AfsSettings_InviteFrequency]  DEFAULT ((1)) FOR [InviteFrequency]
GO
/****** Object:  Default [DF_AfsSettings_TemplateDelay]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[AfsSettings] ADD  CONSTRAINT [DF_AfsSettings_TemplateDelay]  DEFAULT ((7)) FOR [TemplateDelay]
GO
/****** Object:  Default [DF__CompanyUs__UserS__695C9DA1]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[CompanyUsers] ADD  DEFAULT ((0)) FOR [UserState]
GO
/****** Object:  Default [DF__Reviews__IsConfi__668030F6]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[Reviews] ADD  CONSTRAINT [DF__Reviews__IsConfi__668030F6]  DEFAULT ((0)) FOR [IsConfirmed]
GO
/****** Object:  Default [DF_Invitations_ReferenceId]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[Invitations] ADD  CONSTRAINT [DF_Invitations_ReferenceId]  DEFAULT ((0)) FOR [ReferenceId]
GO
/****** Object:  ForeignKey [FK_CompaniesCompanyBox]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[CompanyBoxes]  WITH CHECK ADD  CONSTRAINT [FK_CompaniesCompanyBox] FOREIGN KEY([CompaniesId])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[CompanyBoxes] CHECK CONSTRAINT [FK_CompaniesCompanyBox]
GO
/****** Object:  ForeignKey [FK_AfsCompanies]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[AfsSettings]  WITH CHECK ADD  CONSTRAINT [FK_AfsCompanies] FOREIGN KEY([Companies_Id])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[AfsSettings] CHECK CONSTRAINT [FK_AfsCompanies]
GO
/****** Object:  ForeignKey [FK_AfsTemplate]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[AfsSettings]  WITH CHECK ADD  CONSTRAINT [FK_AfsTemplate] FOREIGN KEY([Template_Id])
REFERENCES [dbo].[Templates] ([Id])
GO
ALTER TABLE [dbo].[AfsSettings] CHECK CONSTRAINT [FK_AfsTemplate]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
/****** Object:  ForeignKey [FK_ContactCity]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[Cities]  WITH CHECK ADD  CONSTRAINT [FK_ContactCity] FOREIGN KEY([Contact_Id])
REFERENCES [dbo].[Contacts] ([Id])
GO
ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_ContactCity]
GO
/****** Object:  ForeignKey [FK_RootCategoryCategory]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_RootCategoryCategory] FOREIGN KEY([RootCategoryId])
REFERENCES [dbo].[RootCategories] ([Id])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_RootCategoryCategory]
GO
/****** Object:  ForeignKey [FK_CompanyCompanyUser]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[CompanyUsers]  WITH CHECK ADD  CONSTRAINT [FK_CompanyCompanyUser] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[CompanyUsers] CHECK CONSTRAINT [FK_CompanyCompanyUser]
GO
/****** Object:  ForeignKey [FK_CompanyReview]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_CompanyReview] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_CompanyReview]
GO
/****** Object:  ForeignKey [FK_CompaniesInvitation]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[Invitations]  WITH CHECK ADD  CONSTRAINT [FK_CompaniesInvitation] FOREIGN KEY([CompaniesId])
REFERENCES [dbo].[Companies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Invitations] CHECK CONSTRAINT [FK_CompaniesInvitation]
GO
/****** Object:  ForeignKey [FK_ReviewReviewWarning]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[ReviewWarnings]  WITH CHECK ADD  CONSTRAINT [FK_ReviewReviewWarning] FOREIGN KEY([ReviewId])
REFERENCES [dbo].[Reviews] ([Id])
GO
ALTER TABLE [dbo].[ReviewWarnings] CHECK CONSTRAINT [FK_ReviewReviewWarning]
GO
/****** Object:  ForeignKey [FK_ReviewReviewUsefull]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[ReviewUsefulls]  WITH CHECK ADD  CONSTRAINT [FK_ReviewReviewUsefull] FOREIGN KEY([ReviewId])
REFERENCES [dbo].[Reviews] ([Id])
GO
ALTER TABLE [dbo].[ReviewUsefulls] CHECK CONSTRAINT [FK_ReviewReviewUsefull]
GO
/****** Object:  ForeignKey [FK_ReviewReviewLike]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[ReviewLikes]  WITH CHECK ADD  CONSTRAINT [FK_ReviewReviewLike] FOREIGN KEY([ReviewId])
REFERENCES [dbo].[Reviews] ([Id])
GO
ALTER TABLE [dbo].[ReviewLikes] CHECK CONSTRAINT [FK_ReviewReviewLike]
GO
/****** Object:  ForeignKey [FK_ReviewReviewComment]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[ReviewComments]  WITH CHECK ADD  CONSTRAINT [FK_ReviewReviewComment] FOREIGN KEY([ReviewId])
REFERENCES [dbo].[Reviews] ([Id])
GO
ALTER TABLE [dbo].[ReviewComments] CHECK CONSTRAINT [FK_ReviewReviewComment]
GO
/****** Object:  ForeignKey [FK_CompanyCategory_Category]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[CompanyCategory]  WITH CHECK ADD  CONSTRAINT [FK_CompanyCategory_Category] FOREIGN KEY([Categories_Id])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[CompanyCategory] CHECK CONSTRAINT [FK_CompanyCategory_Category]
GO
/****** Object:  ForeignKey [FK_CompanyCategory_Companies]    Script Date: 08/05/2015 20:03:58 ******/
ALTER TABLE [dbo].[CompanyCategory]  WITH CHECK ADD  CONSTRAINT [FK_CompanyCategory_Companies] FOREIGN KEY([Companies_Id])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[CompanyCategory] CHECK CONSTRAINT [FK_CompanyCategory_Companies]
GO
