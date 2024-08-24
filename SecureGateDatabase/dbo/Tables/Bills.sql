CREATE TABLE [dbo].[Bills] (
    [BillId]            BIGINT          IDENTITY (1, 1) NOT NULL,
    [MemberId]          BIGINT          NOT NULL,
    [UniqueId]          BIGINT          NOT NULL,
    [BillNo]            BIGINT          NOT NULL,
    [BillDate]          DATE            NOT NULL,
    [PeriodStart]       DATE            NOT NULL,
    [PeriodEnd]         DATE            NOT NULL,
    [DueDate]           DATE            NOT NULL,
    [TotalPayable]      DECIMAL (10, 2) NOT NULL,
    [PaymentStatus]     NVARCHAR (20)   NOT NULL,
    [PaymentDate]       DATE            NULL,
    [InterestAmount]    DECIMAL (10, 2) NULL,
    [TotalWithInterest] DECIMAL (10, 2) NULL,
    [FlatId]            BIGINT          NULL,
    CONSTRAINT [PK__Bills__11F2FC6A3950F34E] PRIMARY KEY CLUSTERED ([BillId] ASC),
    CONSTRAINT [FK__Bills__FlatId__778AC167] FOREIGN KEY ([FlatId]) REFERENCES [dbo].[Flats] ([FlatId])
);


GO
CREATE NONCLUSTERED INDEX [idx_Bills_PaymentStatus]
    ON [dbo].[Bills]([PaymentStatus] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_Bills_DueDate]
    ON [dbo].[Bills]([DueDate] ASC);


GO
CREATE TRIGGER trg_Bills_Audit
ON dbo.Bills
AFTER UPDATE, DELETE
AS
BEGIN
    DECLARE @Action NVARCHAR(20), @BillId BIGINT;

    IF EXISTS (SELECT * FROM DELETED)
    BEGIN
        SET @Action = 'DELETE';
        SET @BillId = (SELECT BillId FROM DELETED);
    END
    ELSE
    BEGIN
        SET @Action = 'UPDATE';
        SET @BillId = (SELECT BillId FROM INSERTED);
    END

    INSERT INTO AuditTrail (TableName, Action, RecordId, ActionBy)
    VALUES ('Bills', @Action, @BillId, SYSTEM_USER);
END;