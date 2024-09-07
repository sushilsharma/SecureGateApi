CREATE TABLE [dbo].[EventMaster] (
    [EventMasterId]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [EventCode]               NVARCHAR (250) NULL,
    [EventDescription]        NVARCHAR (250) NULL,
    [IsRequiredForOnboarding] BIT            NULL,
    [CompanyId]               BIGINT         NULL,
    [CompanyCode]             NVARCHAR (250) NULL,
    [IsActive]                BIT            NOT NULL,
    [CreatedBy]               BIGINT         NOT NULL,
    [CreatedDate]             DATETIME       CONSTRAINT [DF_EventMaster_CreatedDate] DEFAULT ([dbo].[GetDateLocal]()) NOT NULL,
    [UpdatedBy]               BIGINT         NULL,
    [UpdatedDate]             DATETIME       NULL,
    CONSTRAINT [PK_EventMaster] PRIMARY KEY CLUSTERED ([EventMasterId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date when the event was last updated.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventMaster', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User who last updated the event.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventMaster', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date when the event was created.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventMaster', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User who created the event.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventMaster', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag indicating whether the event is active.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventMaster', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Description of the event.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventMaster', @level2type = N'COLUMN', @level2name = N'EventDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Code of the event.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventMaster', @level2type = N'COLUMN', @level2name = N'EventCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for the event master.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventMaster', @level2type = N'COLUMN', @level2name = N'EventMasterId';

