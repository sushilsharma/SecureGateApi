CREATE TABLE [dbo].[RoomType] (
    [RoomTypeId]   BIGINT         NOT NULL,
    [RoomTypeName] NVARCHAR (50)  NOT NULL,
    [TanentCode]   NVARCHAR (250) NULL,
    [IsActive]     BIT            NULL,
    [CreatedDate]  DATETIME       NULL,
    [CreatedBy]    BIGINT         NULL,
    [UpdatedDate]  DATETIME       NULL,
    [UpdatedBy]    BIGINT         NULL,
    PRIMARY KEY CLUSTERED ([RoomTypeId] ASC)
);



