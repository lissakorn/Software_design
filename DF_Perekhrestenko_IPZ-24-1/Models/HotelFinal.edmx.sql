
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/23/2025 01:22:18
-- Generated from EDMX file: E:\UNIVERSITY\Course_2\BD\кр12\DF_Perekhrestenko_IPZ-24-1\DF_Perekhrestenko_IPZ-24-1\Models\HotelFinal.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [HotelModelFirstDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'StaffSet'
CREATE TABLE [dbo].[StaffSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FullName] nvarchar(max)  NOT NULL,
    [Position] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ZakazSet'
CREATE TABLE [dbo].[ZakazSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoomNumber] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [OrderDate] nvarchar(max)  NOT NULL,
    [IsDone] bit  NOT NULL,
    [StaffId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'StaffSet'
ALTER TABLE [dbo].[StaffSet]
ADD CONSTRAINT [PK_StaffSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ZakazSet'
ALTER TABLE [dbo].[ZakazSet]
ADD CONSTRAINT [PK_ZakazSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [StaffId] in table 'ZakazSet'
ALTER TABLE [dbo].[ZakazSet]
ADD CONSTRAINT [FK_StaffZakaz]
    FOREIGN KEY ([StaffId])
    REFERENCES [dbo].[StaffSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StaffZakaz'
CREATE INDEX [IX_FK_StaffZakaz]
ON [dbo].[ZakazSet]
    ([StaffId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------