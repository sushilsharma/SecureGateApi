CREATE TABLE [dbo].[EventObjectPropertiesMapping] (
    [EventObjectPropertiesMappingId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [EventMasterId]                  BIGINT         NULL,
    [ObjectId]                       BIGINT         NULL,
    [ObjectPropertyIds]              NVARCHAR (MAX) NULL,
    [IsActive]                       BIGINT         NOT NULL,
    [CreatedBy]                      BIGINT         NOT NULL,
    [CreatedDate]                    DATETIME       CONSTRAINT [DF_EventObjectPropertiesMapping_CreatedDate] DEFAULT ([dbo].[GetDateLocal]()) NOT NULL,
    [UpdatedBy]                      BIGINT         NULL,
    [UpdatedDate]                    DATETIME       NULL,
    CONSTRAINT [PK_EventPropertiesMaping] PRIMARY KEY CLUSTERED ([EventObjectPropertiesMappingId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date when the event object properties mapping was last updated.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventObjectPropertiesMapping', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User who last updated the event object properties mapping.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventObjectPropertiesMapping', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date when the event object properties mapping was created.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventObjectPropertiesMapping', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User who created the event object properties mapping.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventObjectPropertiesMapping', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag indicating whether the event object properties mapping is active.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventObjectPropertiesMapping', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IDs of the properties associated with the object.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventObjectPropertiesMapping', @level2type = N'COLUMN', @level2name = N'ObjectPropertyIds';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the object.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventObjectPropertiesMapping', @level2type = N'COLUMN', @level2name = N'ObjectId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the associated event master.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventObjectPropertiesMapping', @level2type = N'COLUMN', @level2name = N'EventMasterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for the event object properties mapping.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventObjectPropertiesMapping', @level2type = N'COLUMN', @level2name = N'EventObjectPropertiesMappingId';

