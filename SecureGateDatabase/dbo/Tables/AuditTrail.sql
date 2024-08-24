CREATE TABLE [dbo].[AuditTrail] (
    [AuditId]    BIGINT        IDENTITY (1, 1) NOT NULL,
    [TableName]  NVARCHAR (50) NULL,
    [Action]     NVARCHAR (20) NULL,
    [RecordId]   BIGINT        NULL,
    [ActionBy]   NVARCHAR (50) NULL,
    [ActionDate] DATETIME      CONSTRAINT [DF__AuditTrai__Actio__72C60C4A] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK__AuditTra__A17F23980C215D85] PRIMARY KEY CLUSTERED ([AuditId] ASC)
);

