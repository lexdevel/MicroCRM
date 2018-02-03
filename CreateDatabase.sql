CREATE DATABASE [MicroCRM];
USE [MicroCRM];

CREATE TABLE [dbo].[Users] (
    [Id]                UNIQUEIDENTIFIER                    PRIMARY KEY     DEFAULT NEWID(),
    [CreatedAt]         DATETIME                                            DEFAULT GETUTCDATE(),
    [UpdatedAt]         DATETIME                                            DEFAULT GETUTCDATE(),
    [IsActive]          BIT                                                 DEFAULT 1,

    [Username]          NVARCHAR(64)            UNIQUE,
    [Password]          NVARCHAR(64),
    [Role]              INT
);

CREATE TABLE [dbo].[Customers] (
    [Id]                UNIQUEIDENTIFIER                    PRIMARY KEY     DEFAULT NEWID(),
    [CreatedAt]         DATETIME                                            DEFAULT GETUTCDATE(),
    [UpdatedAt]         DATETIME                                            DEFAULT GETUTCDATE(),
    [IsActive]          BIT                                                 DEFAULT 1,

    [CreatedById]       UNIQUEIDENTIFIER                    FOREIGN KEY REFERENCES [dbo].[Users] ([Id]),
    [UpdatedById]       UNIQUEIDENTIFIER                    FOREIGN KEY REFERENCES [dbo].[Users] ([Id]),

    [EmailAddress]      NVARCHAR(96)            UNIQUE,
    [Name]              NVARCHAR(256),
    [Gender]            INT
);

CREATE TABLE [dbo].[Products] (
    [Id]                UNIQUEIDENTIFIER                    PRIMARY KEY     DEFAULT NEWID(),
    [CreatedAt]         DATETIME                                            DEFAULT GETUTCDATE(),
    [UpdatedAt]         DATETIME                                            DEFAULT GETUTCDATE(),
    [IsActive]          BIT                                                 DEFAULT 1,

    [Name]              NVARCHAR(128),
    [CostPrice]         DECIMAL,
    [Price]             DECIMAL
);

CREATE TABLE [dbo].[Orders] (
    [Id]                UNIQUEIDENTIFIER                    PRIMARY KEY     DEFAULT NEWID(),
    [CreatedAt]         DATETIME                                            DEFAULT GETUTCDATE(),
    [UpdatedAt]         DATETIME                                            DEFAULT GETUTCDATE(),
    [IsActive]          BIT                                                 DEFAULT 1,

    [CreatedById]       UNIQUEIDENTIFIER                    FOREIGN KEY REFERENCES [dbo].[Users] ([Id]),
    [UpdatedById]       UNIQUEIDENTIFIER                    FOREIGN KEY REFERENCES [dbo].[Users] ([Id]),

    [OrderStatus]       INT,
    [OrderNumber]       INT                 IDENTITY(1, 1),
    [CustomerId]        UNIQUEIDENTIFIER                    FOREIGN KEY REFERENCES [dbo].[Customers] ([Id])
);

CREATE TABLE [dbo].[OrderLines] (
    [Id]                UNIQUEIDENTIFIER                    PRIMARY KEY     DEFAULT NEWID(),
    [CreatedAt]         DATETIME                                            DEFAULT GETUTCDATE(),
    [UpdatedAt]         DATETIME                                            DEFAULT GETUTCDATE(),
    [IsActive]          BIT                                                 DEFAULT 1,

    [CreatedById]       UNIQUEIDENTIFIER                    FOREIGN KEY REFERENCES [dbo].[Users] ([Id]),
    [UpdatedById]       UNIQUEIDENTIFIER                    FOREIGN KEY REFERENCES [dbo].[Users] ([Id]),

    [Quantity]          INT,
    [ProductId]         UNIQUEIDENTIFIER                    FOREIGN KEY REFERENCES [dbo].[Products] ([Id]),
    [OrderId]           UNIQUEIDENTIFIER                    FOREIGN KEY REFERENCES [dbo].[Orders]   ([Id])
);

-- Insert administrator...
INSERT INTO [dbo].[Users] ([Username],                 [Password], [Role])
VALUES                    ('admin@myawesomestore.com', 'YWRtaW4=', 0);
