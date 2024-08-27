CREATE TABLE [dbo].[Flats] (
    [FlatId]      BIGINT          IDENTITY (1, 1) NOT NULL,
    [FlatNo]      NVARCHAR (10)   NOT NULL,
    [Wing]        NVARCHAR (10)   NOT NULL,
    [TotalArea]   DECIMAL (10, 2) NULL,
    [RoomTypeId]  BIGINT          NULL,
    [TanentCode]  NVARCHAR (250)  NULL,
    [IsActive]    BIT             NULL,
    [CreatedDate] DATETIME        NULL,
    [CreatedBy]   BIGINT          NULL,
    [UpdatedDate] DATETIME        NULL,
    [UpdatedBy]   BIGINT          NULL,
    CONSTRAINT [PK__Flats__7CD6EDB16F37EC4C] PRIMARY KEY CLUSTERED ([FlatId] ASC)
);





