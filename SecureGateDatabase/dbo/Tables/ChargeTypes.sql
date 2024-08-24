CREATE TABLE [dbo].[ChargeTypes] (
    [ChargeTypeId] BIGINT          IDENTITY (1, 1) NOT NULL,
    [ChargeName]   NVARCHAR (50)   NOT NULL,
    [Description]  NVARCHAR (255)  NULL,
    [MethodId]     BIGINT          NOT NULL,
    [RatePerSqFt]  DECIMAL (10, 2) NULL,
    [FixedAmount]  DECIMAL (10, 2) NULL,
    [RoomTypeId]   BIGINT          NULL,
    CONSTRAINT [PK__ChargeTy__602EC0374F6D0BA3] PRIMARY KEY CLUSTERED ([ChargeTypeId] ASC),
    CONSTRAINT [FK__ChargeTyp__Metho__5BE2A6F2] FOREIGN KEY ([MethodId]) REFERENCES [dbo].[ChargeCalculationMethod] ([MethodId]),
    CONSTRAINT [FK__ChargeTyp__RoomT__5CD6CB2B] FOREIGN KEY ([RoomTypeId]) REFERENCES [dbo].[RoomType] ([RoomTypeId])
);

