IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221220123456_Init')
BEGIN
    CREATE TABLE [KupiFlakonPrices] (
        [Id] bigint NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL,
        [Product] int NOT NULL,
        [ProductPrice] decimal(18,2) NOT NULL,
        [Amount] nvarchar(max) NULL,
        [Availability] bit NOT NULL,
        CONSTRAINT [PK_KupiFlakonPrices] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221220123456_Init')
BEGIN
    CREATE TABLE [StoingPrices] (
        [Id] bigint NOT NULL IDENTITY,
        [AvailbleInMoskow] nvarchar(max) NULL,
        [AvailbleInSpb] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        [Product] int NOT NULL,
        [ProductPrice] decimal(18,2) NOT NULL,
        [Amount] nvarchar(max) NULL,
        [Availability] bit NOT NULL,
        CONSTRAINT [PK_StoingPrices] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221220123456_Init')
BEGIN
    CREATE TABLE [TlgUsers] (
        [Id] bigint NOT NULL IDENTITY,
        [ChatId] bigint NOT NULL,
        [Username] nvarchar(max) NULL,
        [IsAdmin] bit NOT NULL,
        [IsKicked] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_TlgUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221220123456_Init')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221220123456_Init', N'7.0.0');
END;
GO

COMMIT;
GO

