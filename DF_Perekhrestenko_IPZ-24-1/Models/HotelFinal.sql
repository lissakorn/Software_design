-- Створення таблиці для Персоналу
CREATE TABLE [dbo].[StaffSet] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [FullName] NVARCHAR(MAX) NOT NULL,
    [Position] NVARCHAR(MAX) NOT NULL
);

-- Створення таблиці для Замовлень
CREATE TABLE [dbo].[ZakazSet] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [RoomNumber] INT NOT NULL,
    [Description] NVARCHAR(MAX) NOT NULL,
    [OrderDate] DATETIME NOT NULL,
    [IsDone] BIT NOT NULL,
    [StaffId] INT NOT NULL,
    CONSTRAINT [FK_StaffZakaz] FOREIGN KEY ([StaffId]) 
        REFERENCES [dbo].[StaffSet] ([Id])
);