
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/24/2020 21:48:24
-- Generated from EDMX file: C:\Users\Acer\Desktop\Project\FinalProject\FinalProject\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FinalProject_DB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__T_Bills__UserId__52593CB8]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[T_Bills] DROP CONSTRAINT [FK__T_Bills__UserId__52593CB8];
GO
IF OBJECT_ID(N'[dbo].[FK__T_Complai__UserI__4F7CD00D]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[T_Complaints] DROP CONSTRAINT [FK__T_Complai__UserI__4F7CD00D];
GO
IF OBJECT_ID(N'[dbo].[FK__T_FamilyM__UserI__4CA06362]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[T_FamilyMembers] DROP CONSTRAINT [FK__T_FamilyM__UserI__4CA06362];
GO
IF OBJECT_ID(N'[dbo].[FK__T_Users__RoleId__49C3F6B7]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[T_Users] DROP CONSTRAINT [FK__T_Users__RoleId__49C3F6B7];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[T_Bills]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_Bills];
GO
IF OBJECT_ID(N'[dbo].[T_Complaints]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_Complaints];
GO
IF OBJECT_ID(N'[dbo].[T_ErrorLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_ErrorLogs];
GO
IF OBJECT_ID(N'[dbo].[T_Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_Events];
GO
IF OBJECT_ID(N'[dbo].[T_FamilyMembers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_FamilyMembers];
GO
IF OBJECT_ID(N'[dbo].[T_Notices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_Notices];
GO
IF OBJECT_ID(N'[dbo].[T_OTP_Details]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_OTP_Details];
GO
IF OBJECT_ID(N'[dbo].[T_PasswordHistoryLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_PasswordHistoryLog];
GO
IF OBJECT_ID(N'[dbo].[T_Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_Roles];
GO
IF OBJECT_ID(N'[dbo].[T_Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'T_ErrorLogs'
CREATE TABLE [dbo].[T_ErrorLogs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Source] varchar(150)  NOT NULL,
    [Method] varchar(150)  NOT NULL,
    [ErrorOn] datetime  NOT NULL,
    [Message] varchar(250)  NOT NULL,
    [StackTrace] varchar(max)  NOT NULL
);
GO

-- Creating table 'T_OTP_Details'
CREATE TABLE [dbo].[T_OTP_Details] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NULL,
    [OTP] varchar(5)  NOT NULL,
    [GeneratedOn] datetime  NOT NULL,
    [ValidTill] datetime  NOT NULL
);
GO

-- Creating table 'T_PasswordHistoryLog'
CREATE TABLE [dbo].[T_PasswordHistoryLog] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NULL,
    [ChangedOn] datetime  NOT NULL,
    [OldPassword] varchar(30)  NOT NULL,
    [NewPassword] varchar(30)  NOT NULL
);
GO

-- Creating table 'T_Roles'
CREATE TABLE [dbo].[T_Roles] (
    [RoleId] int IDENTITY(1,1) NOT NULL,
    [RoleName] varchar(15)  NOT NULL
);
GO

-- Creating table 'T_Users'
CREATE TABLE [dbo].[T_Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [EmailId] varchar(150)  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Password] varchar(30)  NOT NULL,
    [MobileNo] varchar(10)  NOT NULL,
    [IsOnline] bit  NOT NULL,
    [IsLocked] bit  NOT NULL,
    [RoleId] int  NULL,
    [Age] int  NOT NULL,
    [Gender] varchar(50)  NOT NULL
);
GO

-- Creating table 'T_Bills'
CREATE TABLE [dbo].[T_Bills] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CulturalFund] int  NULL,
    [EmergencyFund] int  NULL,
    [Maintenance] int  NULL,
    [Total] int  NULL,
    [UserId] int  NULL
);
GO

-- Creating table 'T_Complaints'
CREATE TABLE [dbo].[T_Complaints] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ComplaintType] varchar(50)  NOT NULL,
    [Status] varchar(50)  NOT NULL,
    [UserId] int  NULL
);
GO

-- Creating table 'T_FamilyMembers'
CREATE TABLE [dbo].[T_FamilyMembers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MemberName] varchar(50)  NOT NULL,
    [MemberAge] int  NOT NULL,
    [MemberGender] varchar(50)  NOT NULL,
    [MemberRelation] varchar(50)  NOT NULL,
    [UserId] int  NULL
);
GO

-- Creating table 'T_Notices'
CREATE TABLE [dbo].[T_Notices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Announcement] varchar(250)  NULL,
    [MeetingName] varchar(50)  NULL,
    [MeetingDate] datetime  NULL
);
GO

-- Creating table 'T_Events'
CREATE TABLE [dbo].[T_Events] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EventName] varchar(50)  NOT NULL,
    [EventDate] datetime  NOT NULL,
    [EventTime] varchar(20)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'T_ErrorLogs'
ALTER TABLE [dbo].[T_ErrorLogs]
ADD CONSTRAINT [PK_T_ErrorLogs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'T_OTP_Details'
ALTER TABLE [dbo].[T_OTP_Details]
ADD CONSTRAINT [PK_T_OTP_Details]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'T_PasswordHistoryLog'
ALTER TABLE [dbo].[T_PasswordHistoryLog]
ADD CONSTRAINT [PK_T_PasswordHistoryLog]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [RoleId] in table 'T_Roles'
ALTER TABLE [dbo].[T_Roles]
ADD CONSTRAINT [PK_T_Roles]
    PRIMARY KEY CLUSTERED ([RoleId] ASC);
GO

-- Creating primary key on [UserId] in table 'T_Users'
ALTER TABLE [dbo].[T_Users]
ADD CONSTRAINT [PK_T_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [Id] in table 'T_Bills'
ALTER TABLE [dbo].[T_Bills]
ADD CONSTRAINT [PK_T_Bills]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'T_Complaints'
ALTER TABLE [dbo].[T_Complaints]
ADD CONSTRAINT [PK_T_Complaints]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'T_FamilyMembers'
ALTER TABLE [dbo].[T_FamilyMembers]
ADD CONSTRAINT [PK_T_FamilyMembers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'T_Notices'
ALTER TABLE [dbo].[T_Notices]
ADD CONSTRAINT [PK_T_Notices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'T_Events'
ALTER TABLE [dbo].[T_Events]
ADD CONSTRAINT [PK_T_Events]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'T_OTP_Details'
ALTER TABLE [dbo].[T_OTP_Details]
ADD CONSTRAINT [FK__T_OTP_Det__UserI__35BCFE0A]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[T_Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__T_OTP_Det__UserI__35BCFE0A'
CREATE INDEX [IX_FK__T_OTP_Det__UserI__35BCFE0A]
ON [dbo].[T_OTP_Details]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'T_PasswordHistoryLog'
ALTER TABLE [dbo].[T_PasswordHistoryLog]
ADD CONSTRAINT [FK__T_Passwor__UserI__32E0915F]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[T_Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__T_Passwor__UserI__32E0915F'
CREATE INDEX [IX_FK__T_Passwor__UserI__32E0915F]
ON [dbo].[T_PasswordHistoryLog]
    ([UserId]);
GO

-- Creating foreign key on [RoleId] in table 'T_Users'
ALTER TABLE [dbo].[T_Users]
ADD CONSTRAINT [FK__T_Users__RoleId__2A4B4B5E]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[T_Roles]
        ([RoleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__T_Users__RoleId__2A4B4B5E'
CREATE INDEX [IX_FK__T_Users__RoleId__2A4B4B5E]
ON [dbo].[T_Users]
    ([RoleId]);
GO

-- Creating foreign key on [UserId] in table 'T_Bills'
ALTER TABLE [dbo].[T_Bills]
ADD CONSTRAINT [FK__T_Bills__UserId__52593CB8]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[T_Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__T_Bills__UserId__52593CB8'
CREATE INDEX [IX_FK__T_Bills__UserId__52593CB8]
ON [dbo].[T_Bills]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'T_Complaints'
ALTER TABLE [dbo].[T_Complaints]
ADD CONSTRAINT [FK__T_Complai__UserI__4F7CD00D]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[T_Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__T_Complai__UserI__4F7CD00D'
CREATE INDEX [IX_FK__T_Complai__UserI__4F7CD00D]
ON [dbo].[T_Complaints]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'T_FamilyMembers'
ALTER TABLE [dbo].[T_FamilyMembers]
ADD CONSTRAINT [FK__T_FamilyM__UserI__4CA06362]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[T_Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__T_FamilyM__UserI__4CA06362'
CREATE INDEX [IX_FK__T_FamilyM__UserI__4CA06362]
ON [dbo].[T_FamilyMembers]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------