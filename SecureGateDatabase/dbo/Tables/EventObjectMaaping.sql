CREATE TABLE [dbo].[EventObjectMaaping] (
    [EventObjectMappingId] BIGINT   IDENTITY (1, 1) NOT NULL,
    [EmailEventId]         BIGINT   NULL,
    [ObjectId]             BIGINT   NULL,
    [IsActive]             BIT      NOT NULL,
    [CreatedBy]            BIGINT   NOT NULL,
    [CreatedDate]          DATETIME CONSTRAINT [DF_EventObjectMaaping_CreatedDate] DEFAULT ([dbo].[GetDateLocal]()) NOT NULL,
    [ModifiedBy]           BIGINT   NULL,
    [ModifiedDate]         DATETIME CONSTRAINT [DF_EventObjectMaaping_ModifiedDate] DEFAULT ([dbo].[GetDateLocal]()) NULL,
    CONSTRAINT [PK_EventObjectMaaping] PRIMARY KEY CLUSTERED ([EventObjectMappingId] ASC) WITH (FILLFACTOR = 80)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date when the event object mapping was last modified.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventObjectMaaping', @level2type = N'COLUMN', @level2name = N'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User who last modified the event object mapping.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventObjectMaaping', @level2type = N'COLUMN', @level2name = N'ModifiedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date when the event object mapping was created.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventObjectMaaping', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User who created the event object mapping.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventObjectMaaping', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag indicating whether the event object mapping is active.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventObjectMaaping', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the object.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventObjectMaaping', @level2type = N'COLUMN', @level2name = N'ObjectId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the email event.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventObjectMaaping', @level2type = N'COLUMN', @level2name = N'EmailEventId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for the event object mapping.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventObjectMaaping', @level2type = N'COLUMN', @level2name = N'EventObjectMappingId';

