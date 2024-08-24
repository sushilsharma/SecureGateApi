CREATE TABLE [dbo].[BillCharges] (
    [ChargeId]     BIGINT          IDENTITY (1, 1) NOT NULL,
    [BillId]       BIGINT          NOT NULL,
    [ChargeTypeId] BIGINT          NOT NULL,
    [Amount]       DECIMAL (10, 2) NOT NULL,
    CONSTRAINT [PK__BillChar__17FC361B29D963FB] PRIMARY KEY CLUSTERED ([ChargeId] ASC),
    CONSTRAINT [FK__BillCharg__BillI__619B8048] FOREIGN KEY ([BillId]) REFERENCES [dbo].[Bills] ([BillId]),
    CONSTRAINT [FK__BillCharg__Charg__628FA481] FOREIGN KEY ([ChargeTypeId]) REFERENCES [dbo].[ChargeTypes] ([ChargeTypeId])
);

