CREATE TABLE [dbo].[email_validation_status] (
    [EmailValidationStatusId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [StatusDescription]       NVARCHAR (20)  NOT NULL,
    [TanentCode]              NVARCHAR (250) NULL,
    [IsActive]                BIT            NULL,
    [CreatedDate]             DATETIME       NULL,
    [CreatedBy]               BIGINT         NULL,
    [UpdatedDate]             DATETIME       NULL,
    [UpdatedBy]               BIGINT         NULL,
    PRIMARY KEY CLUSTERED ([EmailValidationStatusId] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Description of the email validation status', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'email_validation_status', @level2type = N'COLUMN', @level2name = N'StatusDescription';


GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Unique identifier for the email validation status', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'email_validation_status', @level2type = N'COLUMN', @level2name = N'EmailValidationStatusId';

