CREATE TABLE [dbo].[PaymentDetails] (
    [PaymentId]     BIGINT          NOT NULL,
    [BillId]        BIGINT          NULL,
    [PaymentDate]   DATE            NULL,
    [AmountPaid]    DECIMAL (10, 2) NULL,
    [ReceiptNumber] NVARCHAR (50)   NULL,
    [PaymentMethod] NVARCHAR (50)   NULL,
    PRIMARY KEY CLUSTERED ([PaymentId] ASC),
    FOREIGN KEY ([BillId]) REFERENCES [dbo].[Bills] ([BillId])
);

