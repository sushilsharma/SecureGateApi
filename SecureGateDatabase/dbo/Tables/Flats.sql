CREATE TABLE [dbo].[Flats] (
    [FlatId]      BIGINT         NOT NULL,
    [FlatNo]      NVARCHAR (10)  NOT NULL,
    [Wing]        NVARCHAR (10)  NOT NULL,
    [TanentCode]  NVARCHAR (250) NULL,
    [IsActive]    BIT            NULL,
    [CreatedDate] DATETIME       NULL,
    [CreatedBy]   BIGINT         NULL,
    [UpdatedDate] DATETIME       NULL,
    [UpdatedBy]   BIGINT         NULL,
    PRIMARY KEY CLUSTERED ([FlatId] ASC)
);



