CREATE TABLE [dbo].[FCMEventMasterMapping] (
    [FCMEventMasterMappingId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [FCMServerDetailsId]      BIGINT         NULL,
    [EventCode]               NVARCHAR (100) NULL,
    [IsActive]                BIT            NOT NULL,
    [CreatedBy]               BIGINT         NOT NULL,
    [CreatedDate]             DATETIME       CONSTRAINT [DF_FCMEventMasterMapping_CreatedDate] DEFAULT ([dbo].[GetDateLocal]()) NOT NULL,
    [UpdatedBy]               BIGINT         NULL,
    [UpdatedDate]             DATETIME       NULL,
    CONSTRAINT [PK_FCMEventMasterMapping] PRIMARY KEY CLUSTERED ([FCMEventMasterMappingId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time when the record was updated (datetime).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FCMEventMasterMapping', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the user who updated the record (bigint).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FCMEventMasterMapping', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time when the record was created (datetime).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FCMEventMasterMapping', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the creator (bigint).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FCMEventMasterMapping', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicator for the active status (bit).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FCMEventMasterMapping', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Code of the event (nvarchar).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FCMEventMasterMapping', @level2type = N'COLUMN', @level2name = N'EventCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier for FCM server details (bigint).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FCMEventMasterMapping', @level2type = N'COLUMN', @level2name = N'FCMServerDetailsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for FCM event mapping (bigint).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FCMEventMasterMapping', @level2type = N'COLUMN', @level2name = N'FCMEventMasterMappingId';

