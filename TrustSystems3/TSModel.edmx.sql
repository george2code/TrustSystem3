
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/05/2015 13:57:20
-- Generated from EDMX file: D:\Development\TrustSystems3\TrustSystems3\TSModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TrustSystems];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_ReviewReviewComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReviewComments] DROP CONSTRAINT [FK_ReviewReviewComment];
GO
IF OBJECT_ID(N'[dbo].[FK_RootCategoryCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Categories] DROP CONSTRAINT [FK_RootCategoryCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_ReviewReviewUsefull]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReviewUsefulls] DROP CONSTRAINT [FK_ReviewReviewUsefull];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactCity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cities] DROP CONSTRAINT [FK_ContactCity];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyCompanyUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CompanyUsers] DROP CONSTRAINT [FK_CompanyCompanyUser];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyReview]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reviews] DROP CONSTRAINT [FK_CompanyReview];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyCategory_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CompanyCategory] DROP CONSTRAINT [FK_CompanyCategory_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyCategory_Companies]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CompanyCategory] DROP CONSTRAINT [FK_CompanyCategory_Companies];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles1_AspNetRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles1] DROP CONSTRAINT [FK_AspNetUserRoles1_AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles1_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles1] DROP CONSTRAINT [FK_AspNetUserRoles1_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_CompaniesInvitation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Invitations] DROP CONSTRAINT [FK_CompaniesInvitation];
GO
IF OBJECT_ID(N'[dbo].[FK_AfsCompanies]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AfsSettings] DROP CONSTRAINT [FK_AfsCompanies];
GO
IF OBJECT_ID(N'[dbo].[FK_AfsTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AfsSettings] DROP CONSTRAINT [FK_AfsTemplate];
GO
IF OBJECT_ID(N'[dbo].[FK_ReviewReviewLike]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReviewLikes] DROP CONSTRAINT [FK_ReviewReviewLike];
GO
IF OBJECT_ID(N'[dbo].[FK_ReviewReviewWarning]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReviewWarnings] DROP CONSTRAINT [FK_ReviewReviewWarning];
GO
IF OBJECT_ID(N'[dbo].[FK_CompaniesCompanyBox]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CompanyBoxes] DROP CONSTRAINT [FK_CompaniesCompanyBox];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[C__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[C__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[Reviews]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reviews];
GO
IF OBJECT_ID(N'[dbo].[ReviewComments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReviewComments];
GO
IF OBJECT_ID(N'[dbo].[RootCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RootCategories];
GO
IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[ReviewUsefulls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReviewUsefulls];
GO
IF OBJECT_ID(N'[dbo].[Cities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cities];
GO
IF OBJECT_ID(N'[dbo].[Companies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Companies];
GO
IF OBJECT_ID(N'[dbo].[CompanyUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompanyUsers];
GO
IF OBJECT_ID(N'[dbo].[Contacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contacts];
GO
IF OBJECT_ID(N'[dbo].[Invitations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Invitations];
GO
IF OBJECT_ID(N'[dbo].[Templates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Templates];
GO
IF OBJECT_ID(N'[dbo].[AfsSettings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AfsSettings];
GO
IF OBJECT_ID(N'[dbo].[LoggerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LoggerSet];
GO
IF OBJECT_ID(N'[dbo].[ReviewLikes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReviewLikes];
GO
IF OBJECT_ID(N'[dbo].[ReviewWarnings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReviewWarnings];
GO
IF OBJECT_ID(N'[dbo].[CompanyBoxes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompanyBoxes];
GO
IF OBJECT_ID(N'[dbo].[CompanyCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompanyCategory];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles1];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [BirthDate] datetime  NULL,
    [About] nvarchar(max)  NULL,
    [CityId] int  NULL,
    [Avatar] nvarchar(max)  NULL,
    [Gender] nvarchar(10)  NULL,
    [ExternalAddress] nvarchar(256)  NULL
);
GO

-- Creating table 'Reviews'
CREATE TABLE [dbo].[Reviews] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CompanyId] int  NOT NULL,
    [UserId] nvarchar(max)  NULL,
    [Rating] int  NOT NULL,
    [OrderId] nvarchar(max)  NULL,
    [ReviewShort] nvarchar(255)  NOT NULL,
    [ReviewFull] nvarchar(max)  NOT NULL,
    [IsConfirmed] bit  NOT NULL,
    [DateCreated] datetime  NULL,
    [DatePublished] datetime  NULL,
    [DateUpdated] datetime  NULL
);
GO

-- Creating table 'ReviewComments'
CREATE TABLE [dbo].[ReviewComments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ReviewId] int  NOT NULL,
    [UserId] nvarchar(max)  NOT NULL,
    [Comment] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'RootCategories'
CREATE TABLE [dbo].[RootCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Slug] nvarchar(255)  NOT NULL,
    [Icon] nvarchar(255)  NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Slug] nvarchar(255)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [RootCategoryId] int  NOT NULL,
    [CompanyCategoryId] int  NOT NULL
);
GO

-- Creating table 'ReviewUsefulls'
CREATE TABLE [dbo].[ReviewUsefulls] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ReviewId] int  NOT NULL,
    [UserId] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Cities'
CREATE TABLE [dbo].[Cities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CountryId] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Contact_Id] int  NOT NULL
);
GO

-- Creating table 'Companies'
CREATE TABLE [dbo].[Companies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Slug] nvarchar(255)  NULL,
    [Description] nvarchar(max)  NULL,
    [Website] nvarchar(255)  NOT NULL,
    [Email] nvarchar(255)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [Country] int  NULL,
    [CityId] int  NOT NULL,
    [Address] nvarchar(max)  NULL,
    [Zip] nvarchar(max)  NULL,
    [Logo] nvarchar(max)  NULL,
    [CategoryId] int  NOT NULL,
    [IsOrderRequired] bit  NOT NULL
);
GO

-- Creating table 'CompanyUsers'
CREATE TABLE [dbo].[CompanyUsers] (
    [CompanyId] int  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [UserState] int  NOT NULL
);
GO

-- Creating table 'Contacts'
CREATE TABLE [dbo].[Contacts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Country] int  NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'Invitations'
CREATE TABLE [dbo].[Invitations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CompaniesId] int  NOT NULL,
    [Email] nvarchar(100)  NOT NULL,
    [SenderName] nvarchar(100)  NULL,
    [FromName] nvarchar(100)  NULL,
    [FromEmail] nvarchar(100)  NULL,
    [Status] tinyint  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [SentAt] datetime  NULL,
    [ReferenceId] nvarchar(50)  NOT NULL,
    [DeliveryDate] datetime  NULL
);
GO

-- Creating table 'Templates'
CREATE TABLE [dbo].[Templates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Language] int  NOT NULL,
    [Delay] int  NOT NULL,
    [Subject] nvarchar(250)  NOT NULL,
    [Body] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AfsSettings'
CREATE TABLE [dbo].[AfsSettings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SenderName] nvarchar(250)  NOT NULL,
    [InviteFrequency] tinyint  NOT NULL,
    [TemplateDelay] int  NOT NULL,
    [TemplateSubject] nvarchar(250)  NULL,
    [TemplateBody] nvarchar(max)  NULL,
    [Companies_Id] int  NOT NULL,
    [Template_Id] int  NOT NULL
);
GO

-- Creating table 'LoggerSet'
CREATE TABLE [dbo].[LoggerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] tinyint  NOT NULL,
    [Message] nvarchar(250)  NOT NULL,
    [Date] datetime  NOT NULL
);
GO

-- Creating table 'ReviewLikes'
CREATE TABLE [dbo].[ReviewLikes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(max)  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [ReviewId] int  NOT NULL
);
GO

-- Creating table 'ReviewWarnings'
CREATE TABLE [dbo].[ReviewWarnings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(max)  NOT NULL,
    [Comment] nvarchar(500)  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [ReviewId] int  NOT NULL
);
GO

-- Creating table 'CompanyBoxes'
CREATE TABLE [dbo].[CompanyBoxes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BoxType] int  NOT NULL,
    [Title] nvarchar(100)  NOT NULL,
    [Message] nvarchar(100)  NOT NULL,
    [Details] nvarchar(250)  NOT NULL,
    [CompaniesId] int  NOT NULL,
    [Image] nvarchar(max)  NULL
);
GO

-- Creating table 'CompanyCategory'
CREATE TABLE [dbo].[CompanyCategory] (
    [Categories_Id] int  NOT NULL,
    [Companies_Id] int  NOT NULL
);
GO

-- Creating table 'AspNetUserRoles1'
CREATE TABLE [dbo].[AspNetUserRoles1] (
    [AspNetRoles1_Id] nvarchar(128)  NOT NULL,
    [AspNetUsers1_Id] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Reviews'
ALTER TABLE [dbo].[Reviews]
ADD CONSTRAINT [PK_Reviews]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ReviewComments'
ALTER TABLE [dbo].[ReviewComments]
ADD CONSTRAINT [PK_ReviewComments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RootCategories'
ALTER TABLE [dbo].[RootCategories]
ADD CONSTRAINT [PK_RootCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ReviewUsefulls'
ALTER TABLE [dbo].[ReviewUsefulls]
ADD CONSTRAINT [PK_ReviewUsefulls]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Cities'
ALTER TABLE [dbo].[Cities]
ADD CONSTRAINT [PK_Cities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Companies'
ALTER TABLE [dbo].[Companies]
ADD CONSTRAINT [PK_Companies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [CompanyId], [UserId] in table 'CompanyUsers'
ALTER TABLE [dbo].[CompanyUsers]
ADD CONSTRAINT [PK_CompanyUsers]
    PRIMARY KEY CLUSTERED ([CompanyId], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'Contacts'
ALTER TABLE [dbo].[Contacts]
ADD CONSTRAINT [PK_Contacts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Invitations'
ALTER TABLE [dbo].[Invitations]
ADD CONSTRAINT [PK_Invitations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Templates'
ALTER TABLE [dbo].[Templates]
ADD CONSTRAINT [PK_Templates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AfsSettings'
ALTER TABLE [dbo].[AfsSettings]
ADD CONSTRAINT [PK_AfsSettings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LoggerSet'
ALTER TABLE [dbo].[LoggerSet]
ADD CONSTRAINT [PK_LoggerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ReviewLikes'
ALTER TABLE [dbo].[ReviewLikes]
ADD CONSTRAINT [PK_ReviewLikes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ReviewWarnings'
ALTER TABLE [dbo].[ReviewWarnings]
ADD CONSTRAINT [PK_ReviewWarnings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CompanyBoxes'
ALTER TABLE [dbo].[CompanyBoxes]
ADD CONSTRAINT [PK_CompanyBoxes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Categories_Id], [Companies_Id] in table 'CompanyCategory'
ALTER TABLE [dbo].[CompanyCategory]
ADD CONSTRAINT [PK_CompanyCategory]
    PRIMARY KEY CLUSTERED ([Categories_Id], [Companies_Id] ASC);
GO

-- Creating primary key on [AspNetRoles1_Id], [AspNetUsers1_Id] in table 'AspNetUserRoles1'
ALTER TABLE [dbo].[AspNetUserRoles1]
ADD CONSTRAINT [PK_AspNetUserRoles1]
    PRIMARY KEY CLUSTERED ([AspNetRoles1_Id], [AspNetUsers1_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [ReviewId] in table 'ReviewComments'
ALTER TABLE [dbo].[ReviewComments]
ADD CONSTRAINT [FK_ReviewReviewComment]
    FOREIGN KEY ([ReviewId])
    REFERENCES [dbo].[Reviews]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ReviewReviewComment'
CREATE INDEX [IX_FK_ReviewReviewComment]
ON [dbo].[ReviewComments]
    ([ReviewId]);
GO

-- Creating foreign key on [RootCategoryId] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [FK_RootCategoryCategory]
    FOREIGN KEY ([RootCategoryId])
    REFERENCES [dbo].[RootCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RootCategoryCategory'
CREATE INDEX [IX_FK_RootCategoryCategory]
ON [dbo].[Categories]
    ([RootCategoryId]);
GO

-- Creating foreign key on [ReviewId] in table 'ReviewUsefulls'
ALTER TABLE [dbo].[ReviewUsefulls]
ADD CONSTRAINT [FK_ReviewReviewUsefull]
    FOREIGN KEY ([ReviewId])
    REFERENCES [dbo].[Reviews]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ReviewReviewUsefull'
CREATE INDEX [IX_FK_ReviewReviewUsefull]
ON [dbo].[ReviewUsefulls]
    ([ReviewId]);
GO

-- Creating foreign key on [Contact_Id] in table 'Cities'
ALTER TABLE [dbo].[Cities]
ADD CONSTRAINT [FK_ContactCity]
    FOREIGN KEY ([Contact_Id])
    REFERENCES [dbo].[Contacts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactCity'
CREATE INDEX [IX_FK_ContactCity]
ON [dbo].[Cities]
    ([Contact_Id]);
GO

-- Creating foreign key on [CompanyId] in table 'CompanyUsers'
ALTER TABLE [dbo].[CompanyUsers]
ADD CONSTRAINT [FK_CompanyCompanyUser]
    FOREIGN KEY ([CompanyId])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CompanyId] in table 'Reviews'
ALTER TABLE [dbo].[Reviews]
ADD CONSTRAINT [FK_CompanyReview]
    FOREIGN KEY ([CompanyId])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyReview'
CREATE INDEX [IX_FK_CompanyReview]
ON [dbo].[Reviews]
    ([CompanyId]);
GO

-- Creating foreign key on [Categories_Id] in table 'CompanyCategory'
ALTER TABLE [dbo].[CompanyCategory]
ADD CONSTRAINT [FK_CompanyCategory_Category]
    FOREIGN KEY ([Categories_Id])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Companies_Id] in table 'CompanyCategory'
ALTER TABLE [dbo].[CompanyCategory]
ADD CONSTRAINT [FK_CompanyCategory_Companies]
    FOREIGN KEY ([Companies_Id])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyCategory_Companies'
CREATE INDEX [IX_FK_CompanyCategory_Companies]
ON [dbo].[CompanyCategory]
    ([Companies_Id]);
GO

-- Creating foreign key on [AspNetRoles1_Id] in table 'AspNetUserRoles1'
ALTER TABLE [dbo].[AspNetUserRoles1]
ADD CONSTRAINT [FK_AspNetUserRoles1_AspNetRoles]
    FOREIGN KEY ([AspNetRoles1_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers1_Id] in table 'AspNetUserRoles1'
ALTER TABLE [dbo].[AspNetUserRoles1]
ADD CONSTRAINT [FK_AspNetUserRoles1_AspNetUsers]
    FOREIGN KEY ([AspNetUsers1_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles1_AspNetUsers'
CREATE INDEX [IX_FK_AspNetUserRoles1_AspNetUsers]
ON [dbo].[AspNetUserRoles1]
    ([AspNetUsers1_Id]);
GO

-- Creating foreign key on [CompaniesId] in table 'Invitations'
ALTER TABLE [dbo].[Invitations]
ADD CONSTRAINT [FK_CompaniesInvitation]
    FOREIGN KEY ([CompaniesId])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompaniesInvitation'
CREATE INDEX [IX_FK_CompaniesInvitation]
ON [dbo].[Invitations]
    ([CompaniesId]);
GO

-- Creating foreign key on [Companies_Id] in table 'AfsSettings'
ALTER TABLE [dbo].[AfsSettings]
ADD CONSTRAINT [FK_AfsCompanies]
    FOREIGN KEY ([Companies_Id])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AfsCompanies'
CREATE INDEX [IX_FK_AfsCompanies]
ON [dbo].[AfsSettings]
    ([Companies_Id]);
GO

-- Creating foreign key on [Template_Id] in table 'AfsSettings'
ALTER TABLE [dbo].[AfsSettings]
ADD CONSTRAINT [FK_AfsTemplate]
    FOREIGN KEY ([Template_Id])
    REFERENCES [dbo].[Templates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AfsTemplate'
CREATE INDEX [IX_FK_AfsTemplate]
ON [dbo].[AfsSettings]
    ([Template_Id]);
GO

-- Creating foreign key on [ReviewId] in table 'ReviewLikes'
ALTER TABLE [dbo].[ReviewLikes]
ADD CONSTRAINT [FK_ReviewReviewLike]
    FOREIGN KEY ([ReviewId])
    REFERENCES [dbo].[Reviews]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ReviewReviewLike'
CREATE INDEX [IX_FK_ReviewReviewLike]
ON [dbo].[ReviewLikes]
    ([ReviewId]);
GO

-- Creating foreign key on [ReviewId] in table 'ReviewWarnings'
ALTER TABLE [dbo].[ReviewWarnings]
ADD CONSTRAINT [FK_ReviewReviewWarning]
    FOREIGN KEY ([ReviewId])
    REFERENCES [dbo].[Reviews]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ReviewReviewWarning'
CREATE INDEX [IX_FK_ReviewReviewWarning]
ON [dbo].[ReviewWarnings]
    ([ReviewId]);
GO

-- Creating foreign key on [CompaniesId] in table 'CompanyBoxes'
ALTER TABLE [dbo].[CompanyBoxes]
ADD CONSTRAINT [FK_CompaniesCompanyBox]
    FOREIGN KEY ([CompaniesId])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompaniesCompanyBox'
CREATE INDEX [IX_FK_CompaniesCompanyBox]
ON [dbo].[CompanyBoxes]
    ([CompaniesId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------