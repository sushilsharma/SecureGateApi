CREATE TABLE [dbo].[Members] (
    [MemberId]      BIGINT          NOT NULL,
    [Name]          NVARCHAR (100)  NOT NULL,
    [ContactNumber] NVARCHAR (100)  NOT NULL,
    [Email]         NVARCHAR (100)  NOT NULL,
    [RoomTypeId]    BIGINT          NOT NULL,
    [TotalArea]     DECIMAL (10, 2) NOT NULL,
    [FlatId]        BIGINT          NULL,
    [TanentCode]    NVARCHAR (250)  NULL,
    [IsActive]      BIT             NULL,
    [CreatedDate]   DATETIME        NULL,
    [CreatedBy]     BIGINT          NULL,
    [UpdatedDate]   DATETIME        NULL,
    [UpdatedBy]     BIGINT          NULL,
    PRIMARY KEY CLUSTERED ([MemberId] ASC),
    FOREIGN KEY ([FlatId]) REFERENCES [dbo].[Flats] ([FlatId])
);




GO
CREATE NONCLUSTERED INDEX [idx_Members_FlatId]
    ON [dbo].[Members]([FlatId] ASC);

